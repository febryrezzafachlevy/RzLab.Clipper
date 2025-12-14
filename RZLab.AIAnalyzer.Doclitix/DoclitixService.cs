using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RZLab.AIAnalyzer.Doclitix
{
    public class DoclitixService : IDoclitixService
    {
        private readonly DocumentLegalStorageService _storageService;
        public DoclitixService(DocumentLegalStorageService storageService)
        {
            _storageService = storageService;
        }
        public void Process(string documentName, string documentType)
        {
            try
            {
                // Copy file PDF ke folder pdf.js virtual host
                string workspacePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "pdfjs");
                string fileName = Path.GetFileName(documentName);
                string destFileName = Path.Combine(workspacePath, fileName);

                if (!File.Exists(destFileName))
                    File.Copy(documentName, destFileName, true);

                // Extract
                var pdfMetadata = PDFUtility.ExtractPagesText(destFileName);

                // AI processing
                //var result = await ai.Analyze(text);
                var rawText = string.Join(Environment.NewLine, pdfMetadata!.Contents.Select(x => x.Content));
                // Build document data
                var doc = new DocumentDataModel
                {
                    document_id = Guid.NewGuid().ToString(),
                    file_name = Path.GetFileName(documentName),
                    file_path = documentName,
                    uploaded_at = DateTime.Now,
                    document_type = documentType,
                    metadata = new MetadataModel
                    {
                        page_count = pdfMetadata!.TotalPage,
                        file_size_kb = new FileInfo(documentName).Length / 1024
                    },
                    raw_text = rawText,
                    analysis_result = new AnalysisResultModel()
                };

                _storageService.Save(doc);
            }
            catch
            {
                throw;
            }
        }
    }
}
