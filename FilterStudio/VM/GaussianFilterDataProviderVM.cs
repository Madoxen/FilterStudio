using FilterStudio.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.VM
{
    public class GaussianFilterDataProviderVM : FilterDataProviderVM
    {
        private double sigma = 1;
        public double Sigma
        {
            get { return sigma; }
            set { SetProperty(ref sigma, value); }
        }


        private int width;
        public int Width
        {
            get { return width; }
            set { SetProperty(ref width, value); }
        }


        private int height;
        public int Height
        {
            get { return height; }
            set { SetProperty(ref height, value); }
        }

        private BasicMatrixFilter concreteFilter;


        public GaussianFilterDataProviderVM(FilterVM filter) : base(filter)
        {
            if (!(filter.UnderlayingFilter is BasicMatrixFilterDataProviderVM))
                throw new ArgumentException("GaussianFilterDataVM can only handle BasicMatrixFilter type of underlaying filter");


            concreteFilter = (BasicMatrixFilter)filter.UnderlayingFilter;
        }

        public override void SetData()
        {
            
        }
    }
}
