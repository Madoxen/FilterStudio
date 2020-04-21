using FilterStudio.Interfaces;
using System;
using System.Collections.Generic;
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

        public double[][] FilterData { get; set; }


        public BasicMatrixFilter()
        {

        }

        public BasicMatrixFilter(double[][] FilterData)
        {
            FilterData.CopyTo(this.FilterData, 0);
        }


        public void Operate()
        {
            int filterWidth = FilterData.Length;
            int filterHeight = FilterData[0].Length;
            Bitmap map = new Bitmap(Input);

            Rectangle rect = new Rectangle(0, 0, map.Width, map.Height);
            BitmapData data = map.LockBits(rect, ImageLockMode.ReadWrite, map.PixelFormat);
            int depth = Bitmap.GetPixelFormatSize(data.PixelFormat) / 8; //bytes per pixel
            byte[] buffer = new byte[data.Width * data.Height * depth];

            //copy pixels to buffer
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            Process(buffer, 0, 0, data.Width, data.Height, filterWidth, filterHeight, data.Width, depth);

            //Copy the buffer back to image
            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);
            map.UnlockBits(data);
            Output = map;
        }


        private void Process(byte[] data, int x, int y, int endx, int endy, int filterWidth, int filterHeight, int stride, int depth)
        {

            filterWidth /= 2;
            filterHeight /= 2;

            for (int i = x; i < endx; i++)
            {
                for (int j = y; j < endy; j++)
                {
                    double maskSum = 0;
                    int offset = ((j * stride) + i) * depth;
                    double R = 0;
                    double G = 0;
                    double B = 0;


                    for (int k = -filterWidth; k < filterWidth + 1; k++)
                    {
                        for (int g = -filterHeight; g < filterHeight + 1; g++)
                        {
                            //Check for overflow
                            if (k + i < 0 || k + i >= endx || g + j < 0 || g + j >= endy)
                                continue;

                            int filterPixelIndex = (((j + g) * stride) + k+i)*depth;
                            maskSum += FilterData[k + filterWidth][g + filterHeight];
                            R += data[filterPixelIndex] * FilterData[k + filterWidth][g + filterHeight];
                            G += data[filterPixelIndex + 1] * FilterData[k + filterWidth][g + filterHeight];
                            B += data[filterPixelIndex + 2] * FilterData[k + filterWidth][g + filterHeight];
                        }
                    }


                    if (maskSum == 0)
                        maskSum = 1;

                    R /= maskSum;
                    B /= maskSum;
                    G /= maskSum;



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



                    data[offset] = Convert.ToByte(R);
                    data[offset + 1] = Convert.ToByte(G);
                    data[offset + 2] = Convert.ToByte(B);
                }
            }
        }
    }
}
