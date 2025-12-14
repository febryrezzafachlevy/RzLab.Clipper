using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace RZLab.AIAnalyzer.Doclitix
{
    public static class PDFUtility
    {
        public static PDFMetadata ExtractPagesText(string filePath)
        {
            var fi = new FileInfo(filePath);

            var metadata = new PDFMetadata();
            metadata.Contents = new List<PDFContent>();
            metadata.TotalPage = 0;
            metadata.FileSize = fi.Length;

            using (var doc = PdfDocument.Open(filePath))
            {
                foreach (Page p in doc.GetPages())
                {
                    var content = new PDFContent();
                    content.Content = p.Text;
                    content.PageNumber = p.Number;
                    metadata.Contents.Add(content);
                }

                metadata.TotalPage = doc.NumberOfPages;
            }

            return metadata;
        }
    }
}
