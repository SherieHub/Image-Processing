
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using WebCamLib;

namespace Image_Processing
{
    public partial class Form1 : Form
    {
        Bitmap greenscreenImage, backgroundImage, loadedImage, resultingImage1, resultingImage2;
        private Bitmap resultingWebcamFrame; 
        private Bitmap capturedFrame;

        private Device[] devices;
        private Device activeDevice;

        private System.Windows.Forms.Timer processingTimer;
        private enum WebcamFilter
        {
            None,
            GrayScale,
            Invert,
            Sepia,
            Histogram
        }

        private WebcamFilter currentFilter = WebcamFilter.None;

        public Form1()
        {
            InitializeComponent();

            processingTimer = new System.Windows.Forms.Timer();
            processingTimer.Interval = 100; 
            processingTimer.Tick += ProcessingTimer_Tick;
            processingTimer.Start();
        }

        private void ProcessingTimer_Tick(object sender, EventArgs e)
        {
            if (activeDevice == null) return;

            try
            {
                activeDevice.Sendmessage();
                IDataObject data = Clipboard.GetDataObject();

                if (data == null || !data.GetDataPresent(DataFormats.Bitmap)) return;

                capturedFrame?.Dispose();
                capturedFrame = (Bitmap)data.GetData(DataFormats.Bitmap);

                if (capturedFrame != null)
                {
                    panel1.BackgroundImage?.Dispose();
                    panel1.BackgroundImage = (Bitmap)capturedFrame.Clone();
                    panel1.BackgroundImageLayout = ImageLayout.Stretch;
                }

                if (currentFilter == WebcamFilter.None) return;

                Bitmap processedFrame = null;

                if (capturedFrame != null)
                {
                    switch (currentFilter)
                    {
                        case WebcamFilter.GrayScale:
                            processedFrame = (Bitmap)capturedFrame.Clone();
                            BitmapFilter.GrayScale(processedFrame);
                            break;

                        case WebcamFilter.Invert:
                            processedFrame = (Bitmap)capturedFrame.Clone();
                            BitmapFilter.Invert(processedFrame);
                            break;

                        case WebcamFilter.Sepia:
                            processedFrame = (Bitmap)capturedFrame.Clone();
                            BitmapFilter.Sepia(processedFrame);
                            break;

                        case WebcamFilter.Histogram:
                            int[][] hist = BitmapFilter.GetRGBHistogram(capturedFrame);
                            processedFrame = BitmapFilter.DrawRGBHistogram(hist, panel2.Width, panel2.Height);
                            panel2.BackColor = Color.Black; 
                            break;
                    }

                    panel2.BackgroundImage?.Dispose();
                    panel2.BackgroundImage = processedFrame;
                    panel2.BackgroundImageLayout = ImageLayout.Stretch;

                    resultingWebcamFrame?.Dispose();
                    resultingWebcamFrame = (Bitmap)processedFrame.Clone();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProcessingTimer error: " + ex.Message);
            }
        }




        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadedImage = LoadImage(pictureBox1, pictureBox2);
        }

        private void loadGreenScreenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            greenscreenImage = LoadImage(pictureBox3, pictureBox5);
        }

        private void loadBackgroundToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            backgroundImage = LoadImage(pictureBox4, pictureBox5);
        }

        private Bitmap LoadImage(PictureBox targetBox, PictureBox clearBox)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(openFileDialog1.FileName);

                targetBox.Image = bmp;

                if (clearBox != null)
                    clearBox.Image = null;

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;

                return bmp;
            }
            return null;
        }

        private void saveToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (resultingImage1 == null)
            {
                MessageBox.Show("No processed image to save!");
                return;
            }

            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "PNG|*.png|JPEG|*.jpg|Bitmap|*.bmp";
                saveFileDialog1.Title = "Save Processed Image";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string ext = System.IO.Path.GetExtension(saveFileDialog1.FileName).ToLower();
                    ImageFormat format = ImageFormat.Png;

                    switch (ext)
                    {
                        case ".jpg":
                        case ".jpeg":
                            format = ImageFormat.Jpeg; break;
                        case ".bmp":
                            format = ImageFormat.Bmp; break;
                    }

                    try
                    {
                        resultingImage1.Save(saveFileDialog1.FileName, format);
                        MessageBox.Show("Processed image saved successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving image: " + ex.Message);
                    }
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loadedImage != null)
            {
                resultingImage1 = (Bitmap)loadedImage.Clone();
                pictureBox2.Image = resultingImage1;
            }
            else
            {
                MessageBox.Show("No image loaded to copy!");
            }
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loadedImage != null)
            {
                resultingImage1 = (Bitmap)loadedImage.Clone();
                BitmapFilter.GrayScale(resultingImage1);
                pictureBox2.Image = resultingImage1;
            }
            else
            {
                MessageBox.Show("Load an image before applying GrayScale!");
            }
        }

        private void invertcolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loadedImage != null)
            {
                resultingImage1 = (Bitmap)loadedImage.Clone();
                BitmapFilter.Invert(resultingImage1);
                pictureBox2.Image = resultingImage1;
            }
            else
            {
                MessageBox.Show("Load an image before applying Invert!");
            }
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loadedImage != null)
            {
                int[][] hist = BitmapFilter.GetRGBHistogram(loadedImage);
                resultingImage1 = BitmapFilter.DrawRGBHistogram(hist, pictureBox2.Width, pictureBox2.Height);
                pictureBox2.Image = resultingImage1;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if (capturedFrame != null)
            {
                currentFilter = WebcamFilter.Histogram; 
            }
            else
            {
                MessageBox.Show("No image or webcam frame available for Histogram!");
            }
        }




        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loadedImage != null)
            {
                resultingImage1 = (Bitmap)loadedImage.Clone();
                BitmapFilter.Sepia(resultingImage1);
                pictureBox2.Image = resultingImage1;
            }
            else
            {
                MessageBox.Show("Load an image before applying Sepia!");
            }
        }

        private void saveGreenScreenImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resultingImage2 == null)
            {
                MessageBox.Show("No processed image to save!");
                return;
            }

            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "PNG|*.png|JPEG|*.jpg|Bitmap|*.bmp";
                saveFileDialog1.Title = "Save Processed Image";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string ext = System.IO.Path.GetExtension(saveFileDialog1.FileName).ToLower();
                    ImageFormat format = ImageFormat.Png;

                    switch (ext)
                    {
                        case ".jpg":
                        case ".jpeg":
                            format = ImageFormat.Jpeg; break;
                        case ".bmp":
                            format = ImageFormat.Bmp; break;
                    }

                    try
                    {
                        resultingImage2.Save(saveFileDialog1.FileName, format);
                        MessageBox.Show("Processed image saved successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving image: " + ex.Message);
                    }
                }
            }
        }

        private void applySubtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (backgroundImage == null)
            {
                MessageBox.Show("Load a background image first!");
                return;
            }

            if (pictureBox3.Image == null)
            {
                MessageBox.Show("No image available!");
                return;
            }

            Bitmap fg = new Bitmap(pictureBox3.Image);

            try
            {
                Bitmap newResult = BitmapFilter.GreenScreen(fg, backgroundImage);

                if (resultingImage2 != null)
                {
                    resultingImage2.Dispose();
                    resultingImage2 = null;
                }

                if (pictureBox5.Image != null)
                {
                    pictureBox5.Image.Dispose();
                    pictureBox5.Image = null;
                }

                resultingImage2 = newResult;
                pictureBox5.Image = resultingImage2;
            }
            finally
            {
                fg.Dispose();
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            exit();
        }

        private void exitToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            exit();
        }

        private void exit()
        {
            DialogResult result = MessageBox.Show(
            "Are you sure you want to exit?",
            "Exit Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void startWebCamToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (activeDevice != null)
            {
                MessageBox.Show("Webcam already running!");
                return;
            }

            devices = DeviceManager.GetAllDevices();
            if (devices.Length == 0)
            {
                MessageBox.Show("No webcam detected!");
                return;
            }

            activeDevice = Array.Find(devices, d => d.Name.Contains("ManyCam")) ?? devices[0];

            try
            {
                activeDevice.ShowWindow(panel1); 
                panel1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start webcam: " + ex.Message);
                activeDevice = null;
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (activeDevice == null) return;

            try
            {
                activeDevice.Stop();
                panel1.BackgroundImage?.Dispose();
                panel1.BackgroundImage = null;

                panel2.BackgroundImage?.Dispose();
                panel2.BackgroundImage = null;

                activeDevice = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to stop webcam: " + ex.Message);
            }
        }

        private void captureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resultingWebcamFrame == null)
            {
                MessageBox.Show("No processed frame available to capture.");
                return;
            }

            pictureBox6.Image?.Dispose();

            Bitmap resizedCapture = new Bitmap(resultingWebcamFrame, panel2.Width, panel2.Height);

            pictureBox6.Image = resizedCapture;
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void grayScaleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            currentFilter = WebcamFilter.GrayScale;
        }

        private void invertColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentFilter = WebcamFilter.Invert;
        }

        private void sepiaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            currentFilter = WebcamFilter.Sepia;
        }

        // Optional: Reset to original
        private void noFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentFilter = WebcamFilter.None;
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (pictureBox6.Image == null)
            {
                MessageBox.Show("No captured image to save!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save Captured Image";
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                saveFileDialog.DefaultExt = "png";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                        System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;

                        switch (ext)
                        {
                            case ".jpg":
                            case ".jpeg":
                                format = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                            case ".bmp":
                                format = System.Drawing.Imaging.ImageFormat.Bmp;
                                break;
                        }

                        pictureBox6.Image.Save(saveFileDialog.FileName, format);
                        MessageBox.Show("Captured image saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
