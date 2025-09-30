using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Image_Processing
{
    internal class ConvolutionFilter
    {
        public static Bitmap ApplyKernel(Bitmap sourceBitmap, double[,] kernel, int bias = 0)
        {
            int width = sourceBitmap.Width;
            int height = sourceBitmap.Height;
            int kWidth = kernel.GetLength(0);
            int kHeight = kernel.GetLength(1);
            int kHalfWidth = kWidth / 2;
            int kHalfHeight = kHeight / 2;

            Bitmap resultBitmap = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double r = 0, g = 0, b = 0;

                    for (int ky = -kHalfHeight; ky <= kHalfHeight; ky++)
                    {
                        for (int kx = -kHalfWidth; kx <= kHalfWidth; kx++)
                        {
                            int px = Math.Min(Math.Max(x + kx, 0), width - 1);
                            int py = Math.Min(Math.Max(y + ky, 0), height - 1);

                            Color pixel = sourceBitmap.GetPixel(px, py);
                            double weight = kernel[ky + kHalfHeight, kx + kHalfWidth];

                            r += pixel.R * weight;
                            g += pixel.G * weight;
                            b += pixel.B * weight;
                        }
                    }

                    int rVal = Math.Min(Math.Max((int)Math.Round(r) + bias, 0), 255);
                    int gVal = Math.Min(Math.Max((int)Math.Round(g) + bias, 0), 255);
                    int bVal = Math.Min(Math.Max((int)Math.Round(b) + bias, 0), 255);

                    resultBitmap.SetPixel(x, y, Color.FromArgb(rVal, gVal, bVal));
                }
            }

            return resultBitmap;
        }


        public static double[,] GetSmoothKernel()
        {
            int size = 3;
            double[,] kernel = new double[size, size];
            double value = 1.0 / (size * size); 

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    kernel[y, x] = value;
                }
            }
            return kernel;
        }

        public static double[,] GetGaussianKernel()
        {
            double[,] kernel = new double[3, 3]
            {
                { 1, 2, 1 },
                { 2, 4, 2 },
                { 1, 2, 1 }
            };

            double sum = 0;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    sum += kernel[y, x];
                }
            }

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    kernel[y, x] /= sum; 
                }
            }

            return kernel;
        }

        public static double[,] GetSharpenKernel()
        {
            double[,] kernel = new double[3, 3]
            {
                { 0, -2, 0 },
                { -2, 11, -2 },
                { 0, -2, 0 }
            };

            return kernel;
        }

        public static double[,] GetMeanRemovalKernel()
        {
            double[,] kernel = new double[3, 3]
            {
                { -1, -1, -1 },
                { -1,  9, -1 },
                { -1, -1, -1 }
            };

            return kernel;
        }
        public static double[,] GetEmbossLaplacianKernel()
        {
            double[,] kernel = new double[3, 3]
            {
                { -1,  0, -1 },
                {  0,  4,  0 },
                { -1,  0, -1 }
            };

            return kernel;
        }

        public static double[,] GetEmbossHVKernel()
        {
            return new double[3, 3]
            {
                { 0, -1, 0 },
                { -1, 4, -1 },
                { 0, -1, 0 }
            };
        }

        public static double[,] GetEmbossAllKernel()
        {
            return new double[3, 3]
            {
                { -1, -1, -1 },
                { -1, 8, -1 },
                { -1, -1, -1 }
            };
        }

        public static double[,] GetEmbossLossyKernel()
        {
            return new double[3, 3]
            {
                { 1, -2, 1 },
                { -2, 4, -2 },
                { -2, 1, -2 }
            };
        }

        public static double[,] GetEmbossHorizontalKernel()
        {
            return new double[3, 3]
            {
                { 0, 0, 0 },
                { -1, 2, -1 },
                { 0, 0, 0 }
            };
        }

        public static double[,] GetEmbossVerticalKernel()
        {
            return new double[3, 3]
            {
                { 0, -1, 0 },
                { 0,  0, 0 },
                { 0,  1, 0 }
            };
        }


    }
}
