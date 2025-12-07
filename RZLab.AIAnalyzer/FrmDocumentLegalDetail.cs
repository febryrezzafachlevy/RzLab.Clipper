using Microsoft.VisualBasic.Devices;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using RzLab.Clipper.ControlsLib;
using RZLab.AIAnalyzer.Helpers;
using RZLab.Clipper.Core;
using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Graphics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RZLab.AIAnalyzer
{
    public partial class FrmDocumentLegalDetail : Form
    {
        private ModernSpinner spinner;
        private readonly AppSettingModel _appSetting;
        private DocumentDataModel _documentData;
        private readonly DocumentLegalStorageService _storageService;
        private List<PDFHighlight>? jsonClauses = null;
        private readonly PdfViewer _pdfViewer;
        public FrmDocumentLegalDetail(DocumentDataModel documentData, AppSettingModel appSetting)
        {
            InitializeComponent();

            _storageService = new DocumentLegalStorageService();
            _appSetting = appSetting;
            _documentData = documentData;

            _pdfViewer = new PdfViewer();
            _pdfViewer.Dock = DockStyle.Fill;
            _pdfViewer.BringToFront();
            pnlBody.Controls.Add(_pdfViewer);
            pnlBody.BringToFront();

            this.BackColor = Color.FromArgb(25, 25, 25);
            this.Text = "Document Viewer";
            //this.Shown += MainForm_Shown;
            InitializeLoader();
            this.Shown += PdfForm_Shown;
            //Initialize();
        }
        void InitializeLoader()
        {

            spinner = new ModernSpinner()
            {
                SpinnerColor = Color.White,
                Radius = 40,     // bigger spinner
                Thickness = 4,
            };
            pnlLoader.Controls.Add(spinner);
            spinner.Location = new Point(
                (pnlLoader.Width - spinner.Width) / 2,
                (pnlLoader.Height - spinner.Height) / 2
            );

            pnlLoader.Resize += (s, e) =>
            {
                spinner.Location = new Point(
                    (pnlLoader.Width - spinner.Width) / 2,
                    (pnlLoader.Height - spinner.Height) / 2
                );
            };
            pnlLoader.BringToFront();
            pnlLoader.Visible = false;
        }
        void ShowLoader(bool isShow)
        {
            pnlLoader.Invoke(new Action(() =>
            {
                pnlLoader.Visible = isShow;
            }));
            spinner.Invoke(new Action(() =>
            {
                spinner.Visible = isShow;
            }));
            _pdfViewer.Invoke(new Action(() =>
            {
                _pdfViewer.Visible = !isShow;
            }));
        }
        private async void PdfForm_Shown(object sender, EventArgs e)
        {
            string? highlightKeyword = null;

            BuildJsonClause();
            if (jsonClauses != null)
            {
                var items = jsonClauses;
                highlightKeyword = System.Text.Json.JsonSerializer.Serialize(items);
            }

            await _pdfViewer.LoadPdfAsync(_documentData.file_path, highlightKeyword);
        }
        //private async Task InitPdfViewerAsync()
        //{
        //    ShowLoader(true);
        //    await webView2.EnsureCoreWebView2Async();

        //    // Map the physical pdfjs folder to virtual host
        //    string pdfJsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "pdfjs");
        //    webView2.CoreWebView2.SetVirtualHostNameToFolderMapping(
        //        "pdf.local",
        //        pdfJsFolder,
        //        CoreWebView2HostResourceAccessKind.Allow
        //    );

        //    webView2.CoreWebView2.NavigationCompleted += WebView_NavigationCompleted;

        //    // optional devtools
        //    webView2.CoreWebView2.Settings.AreDevToolsEnabled = true;

        //    // listen to messages from JS
        //    webView2.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
        //}

        //private async void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        //{
        //    try
        //    {
        //        var msg = e.TryGetWebMessageAsString();
        //        if (jsonClauses != null)
        //        {
        //            var items = jsonClauses;
        //            string json = System.Text.Json.JsonSerializer.Serialize(items);
        //            if (msg.Contains("viewer_ready"))
        //            {
        //                await Task.Delay(200);
        //                await HighlightKeywordsAsync(json);
        //            }
        //            Console.WriteLine("Ready: " + msg);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Not Ready: " + msg);
        //        }
        //    }
        //    catch { }
        //}

        //public async Task LoadPdfWithPdfJsAsync(string pdfPath)
        //{
        //    if (!File.Exists(pdfPath)) throw new FileNotFoundException(pdfPath);

        //    if (webView2?.CoreWebView2 == null) await InitPdfViewerAsync();

        //    // Ensure PDF file is located inside mapped folder. If not, copy or map its folder.
        //    // For simplicity assume PDF is inside Resources/pdfjs folder; if not, copy it there first.
        //    string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "pdfjs");
        //    string fileName = Path.GetFileName(pdfPath);
        //    string dest = Path.Combine(folder, fileName);
        //    if (!File.Exists(dest))
        //    {
        //        File.Copy(pdfPath, dest, true);
        //    }

        //    // build viewer url
        //    string pdfVirtualUrl = $"https://pdf.local/{Uri.EscapeDataString(fileName)}";
        //    string viewerUrl = $"https://pdf.local/viewer.html?file={Uri.EscapeDataString(pdfVirtualUrl)}";

        //    webView2.CoreWebView2.Navigate(viewerUrl);

        //    // optional: wait a bit for the viewer to initialize
        //    await Task.Delay(500);


        //}
        void BuildJsonClause()
        {
            if (_documentData.analysis_result != null)
            {
                jsonClauses = new();

                foreach (var clause in _documentData.analysis_result.clauses)
                {
                    Color c = clause.risk.ToUpper() switch
                    {
                        "HIGH" => Color.FromArgb(180, 40, 40),
                        "MEDIUM" => Color.FromArgb(230, 140, 10),
                        "LOW" => Color.FromArgb(40, 150, 60),
                        _ => Color.Gray
                    };

                    var item = new PDFHighlight()
                    {
                        text = clause.content,
                        color = "#" + c.Name.Substring(0, 6),
                    };
                    jsonClauses.Add(item);
                }
            }
        }
        //private async void WebView_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        //{
        //    // Cek apakah viewer sudah load
        //    string url = webView2.Source?.ToString() ?? "";

        //    if (url.Contains("viewer.html"))
        //    {
        //        // Tunggu textLayer render
        //        await Task.Delay(600);
        //        if (jsonClauses != null)
        //        {
        //            var items = jsonClauses;
        //            string json = System.Text.Json.JsonSerializer.Serialize(items);
        //            await HighlightKeywordsAsync(json);
        //        }
        //    }

        //    ShowLoader(false);
        //}

        // Highlight array of (text,color)
        //public async Task HighlightKeywordsAsync(string json)
        //{
        //    // pastikan webView.CoreWebView2 sudah inisialisasi dan viewer telah load PDF
        //    await webView2.CoreWebView2.ExecuteScriptAsync($"window.highlightMultiple({json});");
        //}
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
