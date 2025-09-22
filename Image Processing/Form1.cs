using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Processing
{
    public partial class Form1 : Form
    {
        Bitmap greenscreenImage, backgroundImage, loadedImage, resultingImage1, resultingImage2;
        private VideoCapture capture;
        private bool capturing = false;

        public Form1()
        {
            InitializeComponent();
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
                resultingImage1 = BitmapFilter.DrawRGBHistogram(hist);
                pictureBox2.Image = resultingImage1;
            }
            else
            {
                MessageBox.Show("Load an image before applying Histogram!");
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

            Bitmap foreground = null;

            if (greenscreenImage != null)
            {
                foreground = (Bitmap)greenscreenImage.Clone();
            }
            else if (pictureBox3.Image != null)
            {
                foreground = new Bitmap(pictureBox3.Image);
            }

            if (foreground != null)
            {
                resultingImage2 = BitmapFilter.GreenScreen(foreground, backgroundImage);
                pictureBox5.Image = resultingImage2;
            }
            else
            {
                MessageBox.Show("No greenscreen image or webcam feed available!");
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

        private void startWebcamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            capture = new VideoCapture(0); 
            if (!capture.IsOpened())
            {
                MessageBox.Show("No webcam detected!");
                return;
            }

            capturing = true;
            Task.Run(() =>
            {
                using (var mat = new Mat())
                {
                    while (capturing)
                    {
                        capture.Read(mat);
                        if (!mat.Empty())
                        {
                            Bitmap frame = BitmapConverter.ToBitmap(mat);

                            pictureBox1.Invoke(new Action(() =>
                            {
                                pictureBox3.Image?.Dispose();
                                pictureBox3.Image = frame;
                            }));
                        }
                    }
                }
            });
        }

        private void captureFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image != null)
            {
                Bitmap snapshot = new Bitmap(pictureBox3.Image);
                pictureBox5.Image = snapshot;

                capturing = false;
                capture?.Release();
                capture?.Dispose();

                MessageBox.Show("Frame captured as static image.");
            }
            else
            {
                MessageBox.Show("No webcam frame available to capture!");
            }
        }

    }
}
