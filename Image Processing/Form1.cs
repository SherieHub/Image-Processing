using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Image_Processing
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;
        private Mat currentFrame;
        private Bitmap resultingWebcamFrame;
        private Bitmap loadedImage, resultingImage1, resultingImage2, greenscreenImage, backgroundImage;
        private Bitmap capturedFrame;

        private enum WebcamFilter
        {
            None,
            GrayScale,
            Invert,
            Sepia,
            Histogram
        }
        private WebcamFilter currentFilter = WebcamFilter.None;
        private System.Windows.Forms.Timer processingTimer;
        private Bitmap panel1Buffer;
        private Bitmap panel2Buffer;

        public Form1()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, panel1, new object[] { true });

            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, panel2, new object[] { true });

            processingTimer = new System.Windows.Forms.Timer();
            processingTimer.Interval = 33;
            processingTimer.Tick += ProcessingTimer_Tick;
        }

        private void InitializeBuffers(int width, int height)
        {
            panel1Buffer?.Dispose();
            panel2Buffer?.Dispose();

            panel1Buffer = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            panel2Buffer = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            panel1.BackgroundImage = panel1Buffer;
            panel2.BackgroundImage = panel2Buffer;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private unsafe void ProcessingTimer_Tick(object sender, EventArgs e)
        {
            if (capture == null) return;
            capture.Read(currentFrame);
            if (currentFrame.IsEmpty) return;

            Bitmap frameBitmap = currentFrame.ToBitmap();

            using (Graphics g = Graphics.FromImage(panel1Buffer))
                g.DrawImage(frameBitmap, 0, 0, panel1Buffer.Width, panel1Buffer.Height);

            if (currentFilter != WebcamFilter.None)
            {
                if (currentFilter == WebcamFilter.Histogram)
                {
                    int[][] hist = BitmapFilter.GetRGBHistogram(frameBitmap);

                    Bitmap histImage = BitmapFilter.DrawRGBHistogram(hist, panel2Buffer.Width, panel2Buffer.Height);

                    using (Graphics g = Graphics.FromImage(panel2Buffer))
                    {
                        g.Clear(Color.Black); 
                        g.DrawImage(histImage, 0, 0, panel2Buffer.Width, panel2Buffer.Height);
                    }

                    histImage.Dispose();

                    resultingWebcamFrame?.Dispose();
                    resultingWebcamFrame = (Bitmap)panel2Buffer.Clone();
                }
                else
                {
                    BitmapData srcData = frameBitmap.LockBits(
                        new Rectangle(0, 0, frameBitmap.Width, frameBitmap.Height),
                        ImageLockMode.ReadOnly, frameBitmap.PixelFormat);

                    BitmapData dstData = panel2Buffer.LockBits(
                        new Rectangle(0, 0, panel2Buffer.Width, panel2Buffer.Height),
                        ImageLockMode.WriteOnly, panel2Buffer.PixelFormat);

                    int bytesPerPixel = Image.GetPixelFormatSize(frameBitmap.PixelFormat) / 8;
                    int height = frameBitmap.Height;
                    int width = frameBitmap.Width;

                    byte* srcPtr = (byte*)srcData.Scan0;
                    byte* dstPtr = (byte*)dstData.Scan0;
                    int srcStride = srcData.Stride;
                    int dstStride = dstData.Stride;

                    for (int y = 0; y < height; y++)
                    {
                        byte* srcRow = srcPtr + (y * srcStride);
                        byte* dstRow = dstPtr + (y * dstStride);

                        for (int x = 0; x < width; x++)
                        {
                            byte b = srcRow[x * bytesPerPixel + 0];
                            byte g = srcRow[x * bytesPerPixel + 1];
                            byte r = srcRow[x * bytesPerPixel + 2];

                            switch (currentFilter)
                            {
                                case WebcamFilter.GrayScale:
                                    byte gray = (byte)((r + g + b) / 3);
                                    dstRow[x * bytesPerPixel + 0] = gray;
                                    dstRow[x * bytesPerPixel + 1] = gray;
                                    dstRow[x * bytesPerPixel + 2] = gray;
                                    break;

                                case WebcamFilter.Invert:
                                    dstRow[x * bytesPerPixel + 0] = (byte)(255 - b);
                                    dstRow[x * bytesPerPixel + 1] = (byte)(255 - g);
                                    dstRow[x * bytesPerPixel + 2] = (byte)(255 - r);
                                    break;

                                case WebcamFilter.Sepia:
                                    int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                                    int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                                    int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);
                                    dstRow[x * bytesPerPixel + 0] = (byte)Math.Min(255, tb);
                                    dstRow[x * bytesPerPixel + 1] = (byte)Math.Min(255, tg);
                                    dstRow[x * bytesPerPixel + 2] = (byte)Math.Min(255, tr);
                                    break;
                            }
                        }
                    }

                    frameBitmap.UnlockBits(srcData);
                    panel2Buffer.UnlockBits(dstData);

                    resultingWebcamFrame?.Dispose();
                    resultingWebcamFrame = (Bitmap)panel2Buffer.Clone();
                }
            }

            panel1.Invalidate();
            panel2.Invalidate();
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
            if (capture != null)
            {
                MessageBox.Show("Webcam already running!");
                return;
            }

            try
            {
                capture = new VideoCapture(0, VideoCapture.API.DShow);
                currentFrame = new Mat();

                int width = capture.Width > 0 ? capture.Width : panel1.Width;
                int height = capture.Height > 0 ? capture.Height : panel1.Height;
                InitializeBuffers(width, height);

                processingTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start webcam: " + ex.Message);
                capture = null;
            }
        }


        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture != null)
            {
                processingTimer.Stop();
                capture.Dispose();
                capture = null;

                panel1.BackgroundImage?.Dispose();
                panel2.BackgroundImage?.Dispose();
                panel1.BackgroundImage = null;
                panel2.BackgroundImage = null;
            }
        }

        private void captureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resultingWebcamFrame == null)
            {
                MessageBox.Show("No processed frame available.");
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

        private void histogramToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            currentFilter = WebcamFilter.Histogram;
        }

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