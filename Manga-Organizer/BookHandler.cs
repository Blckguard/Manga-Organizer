using System;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.CodeDom;

namespace Manga_Organizer
{
    internal class BookHandler
    {
        public static void ExtractMetadata(string bookPath)
        {
            var zip = ZipFile.OpenRead(bookPath);
            var zipEntry = zip.GetEntry(@"OEBPS/content.opf");

            if (zipEntry == null)
            {
                throw new ArgumentNullException("file not found");
            }

            zipEntry.ExtractToFile(@"Temp\metadata.opf", true);
        }

        public static Book CreateBookInstance(string metadata)
        {
            string[] lines = File.ReadAllLines(metadata);
            string title = string.Empty;
            string author = string.Empty;

            foreach (string line in lines)
            {
                if (line.StartsWith("<dc:title>"))
                {
                    title = line.Substring(10, line.Length - 21);
                }
                else if (line.StartsWith("<dc:creator>"))
                {
                    author = line.Substring(12, line.Length - 23);
                    break;
                }
            }

            var bookInstance = new Book(title, author);
            return bookInstance;
        }

        public static void CreateBookFolder(Book book, string initialPath)
        {
            Directory.CreateDirectory(book.Path);
            File.Copy(@"Temp\metadata.opf", @$"{book.Path}\metadata.opf");
            File.Copy(initialPath, @$"{book.Path}\{book.Title}.epub");
            File.Delete(@"Temp\metadata.opf");
        }
    }
}
