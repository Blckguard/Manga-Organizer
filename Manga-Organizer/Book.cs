using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Manga_Organizer
{
    internal class Book
    {
        //properties
        public string? title;
        public string? author;
        public string Path;

        // constructor
        public Book(string title, string author, string initialPath)
        {
            Title = title;
            Author = author;
            Path = @$"Books\{Title}";

            if (!Directory.Exists(Path))
            {
                CreateBookFolder(Path);
            }
        }

        // get setters
        public string Title
        {
            get 
            {
                if (title != null)
                {
                    return title;
                }
                return "no title";
            }
            set
            {
                title = value;
            }
        }
        public string Author
        {
            get
            {
                if (author != null)
                {
                    return author;
                }
                return "no author";
            }
            set
            {
                author = value;
            }
        }

        // methods
        public void CreateBookFolder(string initialPath)
        {
            Directory.CreateDirectory(Path);
            File.Copy(@"Temp\metadata.opf", @$"{Path}\metadata.opf");
            //File.Copy(initialPath, @$"{book.Path}\{book.Title}.epub");
            File.Delete(@"Temp\metadata.opf");
        }
    }
}
