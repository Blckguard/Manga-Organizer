﻿using System;
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

namespace Manga_Organizer.View.UserControls
{
    /// <summary>
    /// Interaction logic for Sidebar.xaml
    /// </summary>
    public partial class Sidebar : UserControl
    {
        public static string testboxContent = "test";
        public Sidebar()
        {
            InitializeComponent();
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            var library = new Library(@"Books");

            libraryList.Items.Clear();

            foreach (Book book in library.GetBooks())
            {
                libraryList.Items.Add(book.title);

                
            }
        }
    }
}
