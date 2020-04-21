using FilterStudio.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FilterStudio.Views.Converters
{
    [ValueConversion(typeof(Bitmap), typeof(BitmapSource))]
    public class BitmapToBitmapSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Bitmap b))
            {
                Debug.WriteLine("value parameter is of type " + value?.GetType().ToString() + " not of expected Bitmap type");
                return null;
            }
            return b.ToBitmapSource();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Conversion back from BitmapSource to Bitmap is not yet supported");
        }
    }
}
