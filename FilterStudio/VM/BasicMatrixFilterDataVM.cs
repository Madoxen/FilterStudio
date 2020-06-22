using FilterStudio.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FilterStudio.VM
{
    public class BasicMatrixFilterDataVM : FilterDataVM
    {
        private ObservableCollection<ObservableCollection<double>> matrix;
        public ObservableCollection<ObservableCollection<double>> Matrix
        {
            get { return matrix; }
            set { SetProperty(ref matrix, value); }
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

        /// <summary>
        /// Creates 3x3 matrix data vm
        /// </summary>
        /// <param name="filter"></param>
        public BasicMatrixFilterDataVM(FilterVM filter) : base(filter)
        {
            if (!(filter.UnderlayingFilter is BasicMatrixFilter))
                throw new ArgumentException("BasicMatrixFilterDataVM can only handle BasicMatrixFilter type of underlaying filter (is: " + filter.UnderlayingFilter.GetType() + " | must be: BasicMatrixFilter");

            concreteFilter = (BasicMatrixFilter)filter.UnderlayingFilter;

            Matrix = new ObservableCollection<ObservableCollection<double>>();

            this.PropertyChanged += OnDimensionsChanged;
        }



        public BasicMatrixFilterDataVM(FilterVM filter, double[,] data) : this(filter)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                { 
                
                }
            }
            
        }


        public override void SetData()
        {
            concreteFilter.FilterData = new double[matrix.Count, matrix[0].Count];
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    concreteFilter.FilterData[i, j] = Matrix[i][j];
                }
            }
            concreteFilter.OnFilterDataChanged();
        }


        private void OnDimensionsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Height")
            {
                if (Height > Matrix.Count)
                {
                    for (int i = Matrix.Count; i < Height; i++)
                    {
                        Matrix.Add(new ObservableCollection<double>());
                    }
                }
                else if (Height < Matrix.Count)
                {
                    for (int i = Matrix.Count; i > Height; i--)
                    {
                        Matrix.RemoveAt(Matrix.Count - 1);
                    }
                }
            }
            else if (e.PropertyName == "Width")
            {
                foreach (ObservableCollection<double> oc in Matrix)
                {
                    if (Width > oc.Count)
                    {
                        for (int i = oc.Count; i < Width; i++)
                        {
                            oc.Add(0.0);
                        }
                    }
                    else if (Width < oc.Count)
                    {
                        for (int i = oc.Count; i > Width; i--)
                        {
                            oc.RemoveAt(oc.Count - 1);
                        }
                    }
                }
            }
        }


    }
}
