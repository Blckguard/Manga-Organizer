﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Manga_Organizer.View.UserControls
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

        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                BookPath = files[0];

                EpubHandler.ExtractMetadata(BookPath);
                EpubHandler.CreateBookInstance(@"Temp\metadata.opf");
            }
        }
    }
}
