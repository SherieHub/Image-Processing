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

        public static int[] GetHistogram(Bitmap bmp)
        {
            int[] histogram = new int[256];

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (int)((c.R + c.G + c.B) / 3.0);
                    histogram[gray]++;
                }
            }

            return histogram;
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
