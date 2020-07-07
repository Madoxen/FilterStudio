using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Drawing;

namespace FilterStudio.Views.Validated
{
    /// <summary>
    /// Interaction logic for FloatTextBox.xaml
    /// </summary>
    public partial class FloatTextBox : UserControl
    {
        public FloatTextBox()
        {
            InitializeComponent();
        }

        public object Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FloatTextBox));

    }
}
