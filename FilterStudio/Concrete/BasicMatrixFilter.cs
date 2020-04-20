using FilterStudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Controls;

namespace FilterStudio.Concrete
{
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
            Bitmap map = new Bitmap(Input.Width, Input.Height);
            int Length = FilterData.Length / 2;

            for (int i = 0; i < Input.Width; i++)
            {
                for (int j = 0; j < Input.Height; j++)
                {
                    double maskSum = 0;
                    double R = 0;
                    double B = 0;
                    double G = 0;


                    for (int k = -Length; k < Length + 1; k++)
                    {
                        for (int g = -Length; g < Length + 1; g++)
                        {
                            if (k + i < 0 || k + i >= Input.Width || g + j < 0 || g + j >= Input.Height)
                                continue;

                            Color filterPixel = Input.GetPixel(k + i, g + j);
                            maskSum += FilterData[k + Length][g + (Length)];
                            R += filterPixel.R * FilterData[k + (Length)][g + (Length)];
                            B += filterPixel.B * FilterData[k + (Length)][g + (Length)];
                            G += filterPixel.G * FilterData[k + (Length)][g + (Length)];
                        }
                    }

                    if (maskSum == 0)
                        maskSum = 1;

                    R = R / maskSum;
                    B = B / maskSum;
                    G = G / maskSum;



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



                    Color newColor = Color.FromArgb(Convert.ToInt32(R), Convert.ToInt32(G), Convert.ToInt32(B));
                    map.SetPixel(i, j, newColor);
                }
            }
            Output = map;
        }
    }
}
