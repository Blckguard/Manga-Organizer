using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Manga_Organizer
{
    internal class Book
    {
        public string? title;
        public string? author;
        public string Path;

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
            Path = @$"Books\{Title}";

        }

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
    }
}
