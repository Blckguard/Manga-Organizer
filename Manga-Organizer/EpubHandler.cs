using System;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.CodeDom;
using Manga_Organizer.View.UserControls;
using System.Text.RegularExpressions;

namespace Manga_Organizer
{
    internal class EpubHandler
    {
        public static string BookPath { get; set; }

        public static void ExtractMetadata(string bookPath)
        {
            BookPath = bookPath;
            var zip = ZipFile.OpenRead(bookPath);
            var zipEntry = zip.GetEntry(@"OEBPS/content.opf");

            if (zipEntry == null)
            {
                zipEntry = zip.GetEntry(@"content.opf");
            }
            if (zipEntry == null)
            {
                throw new ArgumentNullException("file-format not supported");
            }

            zipEntry.ExtractToFile(@"Temp\content.opf", true);
            string[] lines = File.ReadAllLines(@"Temp\content.opf");
            File.Delete(@"Temp\content.opf");
            var metadata = File.AppendText(@"Temp\metadata.opf");

            foreach (string line in lines)
            {
                metadata.WriteLine(line.Trim());

                if (line.Contains("</metadata>"))
                {
                    break;
                }
            }
            metadata.Close();
        }

        public static Book CreateBookInstance(string metadata)
        {
            string[] lines = File.ReadAllLines(metadata);
            string title = string.Empty;
            string author = string.Empty;

            foreach (string line in lines)
            {
                if (line.Contains("<dc:title"))
                {
                    title = Regex.Replace(line, "<.*?>", String.Empty).Trim();
                }
                else if (line.Contains("<dc:creator>"))
                {
                    author = Regex.Replace(line, "<.*?>", String.Empty).Trim();
                    break;
                }
                if (line.Contains("</metadata>"))
                {
                    break;
                }
            }

            var bookInstance = new Book(title, author, BookPath);
            return bookInstance;
        }

        //public static void CreateBookFolder(Book book, string initialPath)
        //{
        //    if (!Directory.Exists(book.Path))
        //    {
        //         Directory.CreateDirectory(book.Path);
        //        File.Copy(@"Temp\metadata.opf", @$"{book.Path}\metadata.opf");
        //        //File.Copy(initialPath, @$"{book.Path}\{book.Title}.epub");
        //    }
        //    else
        //    {
        //        Debug.WriteLine("book already in library");
        //    }
        //    File.Delete(@"Temp\metadata.opf");
        //}
    }
}
