using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Drawing.Imaging;

namespace CameraRFID
{
    public partial class MainForm : Form
    {
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevice)
            {
                comboDevices.Items.Add(Device.Name);
            }
            comboDevices.SelectedIndex = 0; // default to first camera
            FinalFrame = new VideoCaptureDevice();
            streamStart();

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[comboDevices.SelectedIndex].MonikerString);

            if (FinalFrame.VideoCapabilities.Length > 0)
            {
                // Set preferred frame size (resolution) to FHD (1920x1080)
                foreach (VideoCapabilities capability in FinalFrame.VideoCapabilities)
                {
                    if (capability.FrameSize.Width == 1920 && capability.FrameSize.Height == 1080)
                    {
                        FinalFrame.VideoResolution = capability;
                        break;
                    }
                }
            }

            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();
        }

        private void streamStart()
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[0].Name);

            if (FinalFrame.VideoCapabilities.Length > 0)
            {
                // Set preferred frame size (resolution) to FHD (1920x1080)
                foreach (VideoCapabilities capability in FinalFrame.VideoCapabilities)
                {
                    if (capability.FrameSize.Width == 1920 && capability.FrameSize.Height == 1080)
                    {
                        FinalFrame.VideoResolution = capability;
                        break;
                    }
                }
            }

            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();
        }

        void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
            }
            try
            {
                pictureBox.Image = (Bitmap)eventArgs.Frame.Clone();
            }
            catch (Exception ex)
            {
                
            }

            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (FinalFrame.IsRunning == true) FinalFrame.SignalToStop();
        }

        private void btnSnapshot_Click(object sender, EventArgs e)
        {
            if (FinalFrame.IsRunning == true)
            {
                string filePath = "Snapshot.bmp";
                if (File.Exists(filePath)) // 파일이 존재하는 경우 memory leak이 발생한다.
                {
                    // 파일을 삭제하여 memory leak을 방지한다.
                    File.Delete(filePath);
                }

                // 이미지를 저장할 경로와 형식을 설정합니다.
                ImageFormat format = ImageFormat.Bmp; // 이미지 형식은 PNG로 설정합니다.
                ImageCodecInfo encoder = GetEncoder(format); // ImageCodecInfo 객체를 가져옵니다.
                EncoderParameters encoderParams = GetEncoderParameters(format); // EncoderParameter 객체 배열을 가져옵니다.


                pictureBox.Image.Save("Snapshot.bmp", encoder, encoderParams);
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }

        private EncoderParameters GetEncoderParameters(ImageFormat format)
        {
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 50L); // 이미지 품질 설정

            return encoderParams;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FinalFrame.IsRunning == true) FinalFrame.SignalToStop();
        }
    }
}