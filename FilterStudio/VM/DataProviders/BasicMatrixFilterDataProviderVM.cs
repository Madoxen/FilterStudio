using FilterStudio.Base;
using FilterStudio.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace FilterStudio.VM
{
    public class BasicMatrixFilterDataProviderVM : FilterDataProviderVM
    {
        private ObservableCollection<ObservableCollection<PrimitiveWrapper<double>>> matrix;
        public ObservableCollection<ObservableCollection<PrimitiveWrapper<double>>> Matrix
        {
            get { return matrix; }
            set
            {
                if (matrix != null)
                    matrix.CollectionChanged -= TopChanged;

                SetProperty(ref matrix, value);

                if (matrix != null)
                    matrix.CollectionChanged += TopChanged;
            }
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


        public RelayCommand IncrementHeight { get; set; }
        public RelayCommand DecrementHeight { get; set; }
        public RelayCommand IncrementWidth { get; set; }
        public RelayCommand DecrementWidth { get; set; }


        private readonly BasicMatrixFilter concreteFilter;
        public BasicMatrixFilterDataProviderVM(FilterVM filter) : base(filter)
        {
            if (!(filter.UnderlayingFilter is BasicMatrixFilter))
                throw new ArgumentException("BasicMatrixFilterDataVM can only handle BasicMatrixFilter type of underlaying filter (is: " + filter.UnderlayingFilter.GetType() + " | must be: BasicMatrixFilter)");

            concreteFilter = (BasicMatrixFilter)filter.UnderlayingFilter;

            Matrix = new ObservableCollection<ObservableCollection<PrimitiveWrapper<double>>>();

            IncrementHeight = new RelayCommand(() => { Height++; });
            DecrementHeight = new RelayCommand(() => { Height--; }, (_) => { return Height > 1; });
            IncrementWidth = new RelayCommand(() => { Width++; });
            DecrementWidth = new RelayCommand(() => { Width--; }, (_) => { return Width > 1; });


            for (int i = 0; i < concreteFilter.FilterData.GetLength(0); i++)
            {
                Matrix.Add(new ObservableCollection<PrimitiveWrapper<double>>());
                for (int j = 0; j < concreteFilter.FilterData.GetLength(1); j++)
                {
                    Matrix[i].Add(new PrimitiveWrapper<double>(concreteFilter.FilterData[i, j]));
                }
            }
            Width = Matrix.Count;
            Height = Matrix[0].Count;


            this.PropertyChanged += OnDimensionsChanged;
        }


        public BasicMatrixFilterDataProviderVM(FilterVM filter, double[,] data) : this(filter)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Matrix.Add(new ObservableCollection<PrimitiveWrapper<double>>());
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Matrix[i].Add(new PrimitiveWrapper<double>(data[i, j]));
                }
            }
            Width = Matrix.Count;
            Height = Matrix[0].Count;

            SetData();
        }

        //Called when top level matrix changes
        private void TopChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //If we remove items from this collection, make sure that we unsubscribe from events so that we wont have memory leak
            if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (ObservableCollection<PrimitiveWrapper<double>> oc in e.OldItems)
                {
                    oc.CollectionChanged -= MidChanged;
                    foreach (PrimitiveWrapper<double> pw in oc)
                    {
                        pw.PropertyChanged -= BotChanged;
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ObservableCollection<PrimitiveWrapper<double>> oc in e.NewItems)
                {
                    oc.CollectionChanged += MidChanged;
                }
            }

        }

        //Called when middle level matrixes change
        private void MidChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //If we remove items from this collection, make sure that we unsubscribe from events so that we wont have memory leak
            if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (PrimitiveWrapper<double> pw in e.OldItems)
                {
                    pw.PropertyChanged -= BotChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add) //if we add any items, make sure that we will subscribe to event
            {
                foreach (PrimitiveWrapper<double> pw in e.NewItems)
                {
                    pw.PropertyChanged += BotChanged;
                }
            }
        }

        private void BotChanged(object sender, PropertyChangedEventArgs e)
        {
            SetData();
        }






        public override void SetData()
        {
            concreteFilter.FilterData = new double[matrix.Count, matrix[0].Count];
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    concreteFilter.FilterData[i, j] = Matrix[i][j].Value;
                }
            }
            concreteFilter.RecalculateMask();
        }

        private void OnDimensionsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Height")
            {
                if (Height > Matrix.Count)
                {
                    for (int i = Matrix.Count; i < Height; i++)
                    {
                        Matrix.Add(new ObservableCollection<PrimitiveWrapper<double>>());
                        for (int j = 0; j < Width; j++)
                        {
                            Matrix.Last().Add(new PrimitiveWrapper<double>(0.0));
                        }
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
                foreach (ObservableCollection<PrimitiveWrapper<double>> oc in Matrix)
                {
                    if (Width > oc.Count)
                    {
                        for (int i = oc.Count; i < Width; i++)
                        {
                            oc.Add(new PrimitiveWrapper<double>(0.0));
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

            SetData();
        }
    }
}
