using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace RZLab.Clipper.Core
{
    public static class PdfUtils
    {
        /// <summary>
        /// Extract each page text as a list. Index 0 => page 1.
        /// Requires UglyToad.PdfPig nuget.
        /// </summary>
        public static List<string> ExtractPagesText(string filePath)
        {
            var pages = new List<string>();
            using (var doc = PdfDocument.Open(filePath))
            {
                foreach (Page p in doc.GetPages())
                {
                    var text = p.Text;
                    pages.Add(text ?? string.Empty);
                }
            }

            return pages;
        }

        public static (int pageCount, int sizeKb) GetBasicMetadata(string filePath)
        {
            int pageCount = 0;
            long size = 0;
            if (File.Exists(filePath))
            {
                using (var doc = PdfDocument.Open(filePath))
                {
                    pageCount = doc.NumberOfPages;
                }
                var fi = new FileInfo(filePath);
                size = fi.Length;
            }
            return (pageCount, (int)(size / 1024));
        }
    }
}
