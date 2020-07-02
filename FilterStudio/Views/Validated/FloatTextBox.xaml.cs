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

        private static string GetNumbers(string input)
        {
            char sep = ',';

            string old = new string(input.Where(c => char.IsDigit(c) || c == sep).ToArray());

            if (old[0] == '.')
            {
                old = '0' + old;
            }
            
            string neww = "";
            int numdots = 0;
            for (int i = 0; i < old.Length; i++)
            {
                if (old[i] == sep)
                {
                    if (numdots == 0)
                    {
                        neww += old[i];
                    }
                    numdots++;
                }
                else
                {
                    neww += old[i];
                }

            }
            return neww;
        }

        public object Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FloatTextBox));

        private void CheckForInvalidState(object sender, RoutedEventArgs e)
        {

            if (!(sender is TextBox))
                return;
            double n;

            if (!double.TryParse(InputTextBox.Text, out n) && InputTextBox.Text.Length>0)
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
