using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;



namespace FilterStudio.Views.Converters
{
    [ValueConversion(typeof(double[,]), typeof(IList<IList<double>>))]
    public class Array2DToList2DConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double[,]))
                throw new ArgumentException("value was not of the type double[,]");

            double[,] concrete = (double[,])value;

            IList<IList<double>> matrix = new List<IList<double>>();
            for (int i = 0; i < concrete.GetLength(0); i++)
            {
                matrix.Add(new List<double>());
                for (int j = 0; j < concrete.GetLength(1); j++)
                {
                    matrix[i].Add(  concrete[i, j]);
                }
            }
            return matrix;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IList<IList<double>>))
                throw new ArgumentException("value was not of the type IList<IList<double>>");


            IList<IList<double>> concrete = (IList<IList<double>>)value;

            double[,] matrix = new double[concrete.Count, concrete[0].Count];
            for (int i = 0; i < concrete.Count; i++)
            {
                for (int j = 0; j < concrete[0].Count; j++)
                {
                    matrix[i, j] = concrete[i][j];
                }
            }

            return matrix;
        }
    }
}
