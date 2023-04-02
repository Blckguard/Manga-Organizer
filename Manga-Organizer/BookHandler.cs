using System;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace HelloWPF
{
    internal class BookHandler
    {
        public static void GetBookTitle()
        {
            string bookPath = @"C:\Users\candy\repos\Manga-Organizer\Manga-Organizer\Books\Abara_ Complete Deluxe Edition - Tsutomu Nihei.epub";

            string extractPath = ".\\Temp";
            string metadataPath = ".\\Temp\\OEBPS\\content.opf";
            Directory.CreateDirectory(".\\Temp");
            ZipFile.ExtractToDirectory(bookPath, extractPath);

            string[] lines = File.ReadAllLines(metadataPath);
            Trace.WriteLine("successfully extracted");
            foreach (string line in lines)
            {
                if (line.Contains("<dc:title>"))
                {
                    string title = line.Substring(10, line.Length - 11);
                    Trace.WriteLine(title);
                    break;
                }
            }
            Directory.Delete(extractPath, true);
        }

        public static void ExtractBook(string bookPath)
        {
            string extractPath = "C:\\Users\\candy\\repos\\C#\\HelloWPF\\Books\\Abara";
            ZipFile.ExtractToDirectory(bookPath, extractPath);
        }
    }
}
