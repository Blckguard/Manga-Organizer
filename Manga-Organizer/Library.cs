using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace Manga_Organizer
{
    internal class Library
    {
        public string? LibraryPath { get; set; }

        public Library(string libraryPath)
        {
            LibraryPath = libraryPath;
        }

        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book> { };
            string[] bookDirectories = Directory.GetDirectories(LibraryPath);
            
            foreach (string directory in bookDirectories)
            {
                string metadata = $@"{directory}\metadata.opf";
                books.Add(EpubHandler.CreateBookInstance(metadata));
                
            }

            return books;
        }
    }
}
