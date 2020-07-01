using FilterStudio.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.VM
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GaussianFilterDataProviderVM : FilterDataProviderVM
    {
        [JsonProperty]
        private double sigma = 1;
        public double Sigma
        {
            get { return sigma; }
            set { SetProperty(ref sigma, value); SetData(); }
        }

        [JsonProperty]
        private int width;
        public int Width
        {
            get { return width; }
            set { SetProperty(ref width, value); SetData(); }
        }

        [JsonProperty]
        private int height;
        public int Height
        {
            get { return height; }
            set { SetProperty(ref height, value); SetData(); }
        }

        private BasicMatrixFilter concreteFilter;


        [JsonConstructor]
        private GaussianFilterDataProviderVM() { } 

        public GaussianFilterDataProviderVM(FilterVM filter) : base(filter)
        {
            if (!(filter.UnderlayingFilter is BasicMatrixFilter))
                throw new ArgumentException("GaussianFilterDataVM can only handle BasicMatrixFilter type of underlaying filter");

            width = 3; 
            height = 3;
            concreteFilter = (BasicMatrixFilter)filter.UnderlayingFilter;
        }

        public override void SetData()
        {
            concreteFilter.FilterData = new double[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    int relativeX = i - Width / 2;
                    int relativeY = j - Height / 2;


                    concreteFilter.FilterData[i, j] = 1.0 / (2.0 * Math.PI * Sigma * Sigma) * Math.Exp((relativeX * relativeX + relativeY * relativeY) / (2.0 * Sigma * Sigma));
                }
          
            }
            concreteFilter.RecalculateMask();
        }

        public override void CopySettings(FilterDataProviderVM provider)
        {
            if (!(provider is GaussianFilterDataProviderVM donor))
                throw new ArgumentException("Donor provider must be of a type GaussianFilterDataProviderVM, was of type:" + provider.GetType().ToString());

            width = donor.Width;
            height = donor.Height;
            sigma = donor.Sigma;


            SetData();
        }
    }
}
