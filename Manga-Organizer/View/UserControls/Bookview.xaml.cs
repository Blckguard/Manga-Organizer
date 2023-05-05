using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using static System.Reflection.Metadata.BlobBuilder;

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
            RefreshList();
        }

        private void buttonExtract_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }
        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                BookPath = files[0];

                var currentEpub = new EpubHandler(BookPath);

                currentEpub.ExtractMetadata();
                currentEpub.ExtractCover();
                EpubHandler.CreateBookInstance(@"Temp\metadata.opf");
            }

            RefreshList();
        }

        public void RefreshList()
        {
            listBookCover.Items.Clear();
            var library = new Library(@"Books");
            List<Book> libraryList = library.GetBooks();

            foreach (Book book in libraryList)
            {
                // getting path to cover and converting to URI
                var fullPath = Path.GetFullPath(book.Cover);
                var imageUri = new Uri(fullPath, UriKind.Absolute);

                // creating image instance
                var image = new Image();
                image.Source = new BitmapImage(imageUri);

                // creating stackpanel instance and adding image to it
                var listEntry = new StackPanel();
                listEntry.Orientation = Orientation.Horizontal;
                listEntry.Children.Add(image);

                // setting properties of list entry
                listEntry.Height = 200;
                listEntry.Margin = new Thickness(5);


                // adding stackpanel to the list of covers
                listBookCover.Items.Add(listEntry);
            }
        }
    }
}
