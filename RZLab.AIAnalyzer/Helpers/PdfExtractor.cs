using RZLab.Clipper.Core.DocumentLegal;
using System.Text;
using UglyToad.PdfPig;

public class PdfExtractor
{
    public PDFMetadata? Extract(string path)
    {
        try
        {
            using var pdf = PdfDocument.Open(path);
            var sb = new StringBuilder();

            var pageCount = pdf.GetPages().Count();

            foreach (var page in pdf.GetPages())
                sb.AppendLine(page.Text);

            var result = new PDFMetadata
            {
                text_raw = sb.ToString(),
                page_count = pageCount,
            };

            return result;
        }
        catch
        {
            return null;
        }
    }
}
