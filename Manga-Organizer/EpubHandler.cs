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
        public static ZipArchive Zip { get; set; }

        public EpubHandler(string bookPath)
        {
            BookPath = bookPath;
            Zip = ZipFile.OpenRead(BookPath);
        }

        public void ExtractMetadata()
        {
            var zipEntry = Zip.GetEntry(@"OEBPS/content.opf");

            if (zipEntry == null)
            {
                zipEntry = Zip.GetEntry(@"content.opf");
            }
            if (zipEntry == null)
            {
                throw new ArgumentNullException("metadata not found");
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

        public void ExtractCover()
        {
            var zipEntry = Zip.GetEntry(@"OEBPS/Images/cover.jpg");

            if (zipEntry == null)
            {
                zipEntry = Zip.GetEntry(@"cover.jpg");
            }
            if (zipEntry == null)
            {
                throw new ArgumentNullException("cover not found");
            }

            zipEntry.ExtractToFile(@"Temp\cover.jpg");
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
    }
}
