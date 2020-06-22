using FilterStudio.Concrete;
using FilterStudio.VM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using FilterStudio.Views.Converters;


namespace FilterStudio.Views
{
    /// <summary>
    /// Interaction logic for MatrixEditorControl.xaml
    /// </summary>
    public partial class MatrixEditorControl : UserControl
    {
        public MatrixEditorControl()
        {
            InitializeComponent();
            DataContextChanged += MatrixEditorControl_DataContextChanged;
        }

        private void MatrixEditorControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is null)
                Debug.WriteLine("Data context of MatrixEditorControl is null!");


            if (!(e.NewValue is BasicMatrixFilterDataVM dt))
                throw new ArgumentException("This control is used only with BasicMatrixFilterDataVM");
            

            MatrixWidth = dt.Matrix.Count;
            MatrixHeight = dt.Matrix[0].Count;

        }
      

        public int MatrixWidth
        {
            get { return (int)GetValue(MatrixWidthProperty); }
            set { SetValue(MatrixWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MatrixWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MatrixWidthProperty =
            DependencyProperty.Register("MatrixWidth", typeof(int), typeof(MatrixEditorControl), new PropertyMetadata(0));


        public int MatrixHeight
        {
            get { return (int)GetValue(MatrixHeightProperty); }
            set { SetValue(MatrixHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MatrixHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MatrixHeightProperty =
            DependencyProperty.Register("MatrixHeight", typeof(int), typeof(MatrixEditorControl), new PropertyMetadata(0));




      
    }
}
