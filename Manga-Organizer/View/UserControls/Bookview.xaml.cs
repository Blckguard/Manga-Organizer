using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelloWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for Bookview.xaml
    /// </summary>
    public partial class Bookview : UserControl
    {
        public string BookPath { get; set; }
        public Bookview()
        {
            InitializeComponent();
            BookPath = string.Empty;
        }

        private void buttonExtract_Click(object sender, RoutedEventArgs e)
        {
            BookHandler.GetBookTitle();
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Get the path of the dropped file
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                BookPath = files[0]; // Assuming only one file was dropped

            }
        }
    }
}
