using System;
using System.Drawing;

namespace Image_Processing
{
    internal class BitmapFilter
    {
        public static void GrayScale(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (int)((c.R + c.G + c.B) / 3.0);
                    bmp.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }
        }

        public static void Invert(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
        }

        public static void Sepia(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);

                    int tr = (int)(0.393 * c.R + 0.769 * c.G + 0.189 * c.B);
                    int tg = (int)(0.349 * c.R + 0.686 * c.G + 0.168 * c.B);
                    int tb = (int)(0.272 * c.R + 0.534 * c.G + 0.131 * c.B);

                    tr = Math.Min(255, tr);
                    tg = Math.Min(255, tg);
                    tb = Math.Min(255, tb);

                    bmp.SetPixel(x, y, Color.FromArgb(tr, tg, tb));
                }
            }
        }

        public static int[][] GetRGBHistogram(Bitmap bmp)
        {
            int[][] hist = new int[3][]; 
            hist[0] = new int[256]; 
            hist[1] = new int[256]; 
            hist[2] = new int[256]; 

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    hist[0][c.R]++;
                    hist[1][c.G]++;
                    hist[2][c.B]++;
                }
            }
            return hist;
        }

        //public static Bitmap DrawRGBHistogram(int[][] hist)
        //{
        //    int width = 512;   
        //    int height = 200;
        //    Bitmap histImage = new Bitmap(width, height);

        //    using (Graphics g = Graphics.FromImage(histImage))
        //    {
        //        g.Clear(Color.White);

        //        int max = 0;
        //        for (int i = 0; i < 3; i++)
        //            for (int j = 0; j < 256; j++)
        //                if (hist[i][j] > max) max = hist[i][j];

        //        for (int i = 0; i < 256; i++)
        //        {
        //            float rIntensity = (float)hist[0][i] / max;
        //            float gIntensity = (float)hist[1][i] / max;
        //            float bIntensity = (float)hist[2][i] / max;

        //            int rHeight = (int)(rIntensity * height);
        //            int gHeight = (int)(gIntensity * height);
        //            int bHeight = (int)(bIntensity * height);

        //            g.DrawLine(new Pen(Color.FromArgb(180, Color.Red)), i * 2, height, i * 2, height - rHeight);
        //            g.DrawLine(new Pen(Color.FromArgb(180, Color.Green)), i * 2 + 1, height, i * 2 + 1, height - gHeight);
        //            g.DrawLine(new Pen(Color.FromArgb(180, Color.Blue)), i * 2, height, i * 2, height - bHeight);
        //        }
        //    }
        //    return histImage;
        //}

        public static Bitmap DrawRGBHistogram(int[][] hist, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);

                int max = hist.Max(h => h.Max());
                if (max == 0) max = 1;

                Pen redPen = new Pen(Color.Red);
                Pen greenPen = new Pen(Color.Green);
                Pen bluePen = new Pen(Color.Blue);

                for (int i = 0; i < 256; i++)
                {
                    int rHeight = (int)(hist[0][i] * (height - 1) / max);
                    int gHeight = (int)(hist[1][i] * (height - 1) / max);
                    int bHeight = (int)(hist[2][i] * (height - 1) / max);

                    // Scale X across the width
                    int x = i * width / 256;

                    g.DrawLine(redPen, x, height - 1, x, height - 1 - rHeight);
                    g.DrawLine(greenPen, x, height - 1, x, height - 1 - gHeight);
                    g.DrawLine(bluePen, x, height - 1, x, height - 1 - bHeight);
                }
            }
            return bmp;
        }

        public static Bitmap GreenScreen(Bitmap greenscreenImage, Bitmap background)
        {
            Bitmap result = new Bitmap(greenscreenImage.Width, greenscreenImage.Height);

            for (int x = 0; x < greenscreenImage.Width; x++)
            {
                for (int y = 0; y < greenscreenImage.Height; y++)
                {
                    Color pixel = greenscreenImage.GetPixel(x, y);

                    // Better green detection
                    bool isGreen = pixel.G > 100 &&
                                   pixel.G > pixel.R + 40 &&
                                   pixel.G > pixel.B + 40;

                    if (isGreen && x < background.Width && y < background.Height)
                    {
                        result.SetPixel(x, y, background.GetPixel(x, y));
                    }
                    else
                    {
                        result.SetPixel(x, y, pixel);
                    }
                }
            }
            return result;
        }

    }
}
