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

        private static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
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

            if (!(sender is TextBox))
                return;
            int n;

            if (!int.TryParse(InputTextBox.Text, out n))
            {
                int t = InputTextBox.CaretIndex;
                InputTextBox.Text = GetNumbers(InputTextBox.Text);
                if (t > 0) t--;
                InputTextBox.CaretIndex = t;
            }
            if (InputTextBox.Text != "")
            {
                InputTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }
    }
}
