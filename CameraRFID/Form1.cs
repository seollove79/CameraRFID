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

            if (CaptureDevice.Count == 0)
            {
                // No camera detected. Inform the user and return.
                MessageBox.Show("카메라를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FinalFrame = new VideoCaptureDevice();
            streamStart();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        private void streamStart()
        {
            if (CaptureDevice.Count == 0)
            {
                // No camera detected. Return.
                return;
            }

            FinalFrame = new VideoCaptureDevice(CaptureDevice[0].MonikerString);

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
                // Get the path of the local application data folder
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                // Define the snapshot directory
                string snapshotDirectory = Path.Combine(localAppData, "CAMERA_RFID", "Snapshot");

                // If the directory does not exist, create it
                if (!Directory.Exists(snapshotDirectory))
                {
                    Directory.CreateDirectory(snapshotDirectory);
                }

                // Define the full file path
                string filePath = Path.Combine(snapshotDirectory, "Snapshot.bmp");

                if (File.Exists(filePath)) // If the file exists, prevent a memory leak.
                {
                    // Delete the file to prevent a memory leak.
                    File.Delete(filePath);
                }

                // Set the path and format to save the image.
                ImageFormat format = ImageFormat.Bmp; // Set the image format to PNG.
                ImageCodecInfo encoder = GetEncoder(format); // Get the ImageCodecInfo object.
                EncoderParameters encoderParams = GetEncoderParameters(format); // Get the EncoderParameter object array.

                pictureBox.Image.Save(filePath, encoder, encoderParams);
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