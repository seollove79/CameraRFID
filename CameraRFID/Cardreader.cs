using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CameraRFID
{
    class Cardreader
    {
        private SerialPort serialPort = new SerialPort();
        private string serialPortName;
        private List<int> receiveData = new List<int>();
        private string cardNumber;
        private Form ownerForm;

        public Cardreader(string strSerialPortName, Form ownerForm)
        {
            serialPortName = strSerialPortName;
            this.ownerForm = ownerForm;
        }

        public void setSerialPort()
        {
            try
            {
                serialPort.PortName = serialPortName;
                serialPort.BaudRate = 9600;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Parity = Parity.None;
                serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                serialPort.Open();
            }
            catch (Exception ex)
            {
                ownerForm.Invoke(new Action(() =>
                {
                    MessageBox.Show(ownerForm, "RFID 리더기 연결에 실패했습니다: " + ex.Message);
                }));
            }
        }


        int count = 0;

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)  //수신 이벤트가 발생하면 이 부분이 실행된다.
        {
            int i_recv_size = serialPort.BytesToRead;
            byte[] b_tmp_buf = new byte[i_recv_size];
            serialPort.Read(b_tmp_buf, 0, i_recv_size);

            foreach (var temp in b_tmp_buf)
            {
                if (temp == 1)
                {
                    receiveData.Clear();
                    count = 0;

                    ownerForm.Invoke(new Action(() =>
                    {
                        MessageBox.Show(ownerForm, "카드를 인식하지 못했습니다.");
                    }));
                    break;
                }

                receiveData.Add(temp);
                count++;

                if (count == 11)
                {
                    for (int i = 3; i < 11; i++)
                    {
                        cardNumber = cardNumber + (char)receiveData[i];
                    }

                    ownerForm.Invoke(new Action(() =>
                    {
                        MessageBox.Show(ownerForm, "카드 인식에 성공하였습니다. 카드 번호: " + cardNumber);
                    }));

                    count = 0;
                    cardNumber = "";
                    receiveData.Clear();
                }
            }
        }

        public string getCardNumber()
        {
            return cardNumber;
        }

        public void read()
        {
            if (!serialPort.IsOpen)
            {
                throw new Exception("카드리더기가 연결되어 있지 않습니다.");
            }

            byte[] readByte = { 0x23, 0x03, 0x02, 0x00, 0x02 };
            serialPort.Write(readByte, 0, readByte.Length);
        }


        public void close()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
    }
}
