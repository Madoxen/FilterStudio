using FilterStudio.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace FilterStudio.Concrete
{

    /// <summary>
    /// Produces grayscale of an image
    /// Grayscale is simp
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class GrayscaleFilter : IFilter
    {
        public Bitmap Output { get; private set; }
        public Bitmap Input { get; set; }

        public void Operate()
        {
            Bitmap map = new Bitmap(Input);
            Rectangle rect = new Rectangle(0, 0, map.Width, map.Height);
            BitmapData data = map.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte[] buffer = new byte[Math.Abs(data.Stride) * data.Height];
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
                    double gray = (buffer[centralPixel] + buffer[centralPixel + 1] + buffer[centralPixel + 2]) / 3.0;
                    buffer[centralPixel] = (byte)Math.Round(gray);
                    buffer[centralPixel+1] = (byte)Math.Round(gray);
                    buffer[centralPixel+2] = (byte)Math.Round(gray);

                }
            }

            //Copy the buffer back to image
            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);
            map.UnlockBits(data);
            Output = map;
        }
    }
}
