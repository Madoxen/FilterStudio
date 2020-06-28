using FilterStudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FilterStudio.Concrete
{
    public class GrayscaleFilter : IFilter
    {
        public Bitmap Output { get; private set; }
        public Bitmap Input { get; set; }

        public void Operate()
        {
            
        }
    }
}
