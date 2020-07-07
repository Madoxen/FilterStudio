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

namespace FilterStudio.Views.Validated
{
    /// <summary>
    /// Interaction logic for IntTextBox.xaml
    /// </summary>
    public partial class IntTextBox : UserControl
    {
        public IntTextBox()
        {
            InitializeComponent();
        }


        public object Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IntTextBox));

        private void CheckForInvalidState(object sender, RoutedEventArgs e)
        {
            return;
        }
    }
    }
