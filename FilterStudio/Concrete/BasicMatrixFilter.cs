using FilterStudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FilterStudio.Concrete
{

    /// <summary>
    /// This filter assumes that we are using square arrays
    /// </summary>
    public class BasicMatrixFilter : IFilter
    {

        public Bitmap Output { get; private set; }

        public Bitmap Input { get; set; }


        public double[,] FilterData { get; set; }



        private double maskSum = 1;



        private void OnFilterDataChanged()
        {
            maskSum = 0;
            for (int g = 0; g < FilterData.GetLength(0); g++)
            {
                for (int k = 0; k < FilterData.GetLength(1); k++)
                {
                    maskSum += FilterData[k, g];
                }
            }

            if (maskSum == 0)
                maskSum = 1;
        }


        public BasicMatrixFilter()
        {

        }

        public BasicMatrixFilter(double[,] FilterData)
        {
            FilterData.CopyTo(this.FilterData, 0);
            OnFilterDataChanged();
        }


        public void Operate()
        {
            int filterWidth = FilterData.GetLength(0) / 2;
            int filterHeight = FilterData.GetLength(1) / 2;
            Bitmap map = new Bitmap(Input);

            Rectangle rect = new Rectangle(0, 0, map.Width, map.Height);
            BitmapData data = map.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte[] buffer = new byte[Math.Abs(data.Stride) * data.Height];
            byte[] image = new byte[Math.Abs(data.Stride) * data.Height];

            //copy pixels to buffer
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);


            int Height = data.Height;
            int Width = data.Width;
            int Stride = Math.Abs(data.Stride);

            for (int y = 0; y < Height; y++) //operate on RGB bitmap values
            {
                for (int x = 0; x < Width; x++) //in-stride position
                {
                    int centralPixel = (Stride * y) + (x * 3);
                    double R = 0;
                    double G = 0;
                    double B = 0;

                    for (int g = -filterHeight; g < filterHeight + 1; g++)
                    {
                        for (int k = -filterWidth; k < filterWidth + 1; k++)
                        {

                            //Check for overflow
                            if (k + x < 0 || k + x >= Width || g + y < 0 || g + y >= Height)
                                continue;

                            int filterPixelIndex = centralPixel + (g * Stride) + (k * 3);
                            R += buffer[filterPixelIndex] * FilterData[k + filterWidth, g + filterHeight];
                            G += buffer[filterPixelIndex + 1] * FilterData[k + filterWidth, g + filterHeight];
                            B += buffer[filterPixelIndex + 2] * FilterData[k + filterWidth, g + filterHeight];
                        }
                    }


                    R /= maskSum;
                    G /= maskSum;
                    B /= maskSum;



                    if (R < 0)
                        R = 0;

                    if (R > 255)
                        R = 255;

                    if (G < 0)
                        G = 0;

                    if (G > 255)
                        G = 255;

                    if (B < 0)
                        B = 0;

                    if (B > 255)
                        B = 255;



                    image[centralPixel] = Convert.ToByte(R);
                    image[centralPixel + 1] = Convert.ToByte(G);
                    image[centralPixel + 2] = Convert.ToByte(B);
                }
            }


            //Copy the buffer back to image
            Marshal.Copy(image, 0, data.Scan0, image.Length);
            map.UnlockBits(data);
            Output = map;
        }
    }
}
