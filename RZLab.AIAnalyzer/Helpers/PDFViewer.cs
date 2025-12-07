using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RZLab.AIAnalyzer;

public class PdfViewer : UserControl
{
    private readonly WebView2 webView;
    private bool viewerReady = false;
    private string pendingHighlightJson = null;
    private string _pendingPdfVirtualUrl = null;

    public event Action ViewerReady;

    public PdfViewer()
    {
        webView = new WebView2
        {
            Dock = DockStyle.Fill
        };
        Controls.Add(webView);

        this.Load += PdfViewerWithHighlight_Load;
    }

    private async void PdfViewerWithHighlight_Load(object sender, EventArgs e)
    {
        await InitializeAsync();
    }

    // ---------------------------
    // INIT WEBVIEW + VIRTUAL HOST
    // ---------------------------
    private async Task InitializeAsync()
    {
        await webView.EnsureCoreWebView2Async();

        string pdfjsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "pdfjs");

        webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
            "pdf.local",
            pdfjsPath,
            CoreWebView2HostResourceAccessKind.Allow);

        webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
        webView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
    }

    private async void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        string url = webView.Source?.ToString() ?? "";

        // viewer.html loaded
        if (url.Contains("viewer.html"))
        {
            await Task.Delay(500); // berikan waktu viewer load
            //string pdfUrl = _pendingPdfVirtualUrl;
            //string js = $"window.loadPdf('{pdfUrl}');";

            //await webView.CoreWebView2.ExecuteScriptAsync(js);

            // Jika ada highlight yang tertunda → jalankan
            if (pendingHighlightJson != null)
            {
                await HighlightKeywordsAsync(pendingHighlightJson);
                pendingHighlightJson = null;
            }
        }
    }

    // menerima pesan dari JS
    private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        var json = e.TryGetWebMessageAsString();
        if (json.Contains("viewer_ready"))
        {
            viewerReady = true;
            ViewerReady?.Invoke();

            if (pendingHighlightJson != null)
            {
                _ = HighlightKeywordsAsync(pendingHighlightJson);
                pendingHighlightJson = null;
            }
        }
    }

    // ---------------------------
    // LOAD VIEWER.HTML
    // ---------------------------
    public async Task LoadViewerAsync()
    {
        if (webView?.CoreWebView2 == null)
            await InitializeAsync();

        string viewerUrl = "https://pdf.local/viewer.html";
        webView.CoreWebView2.Navigate(viewerUrl);
    }

    // ---------------------------
    // LOAD PDF
    // ---------------------------
    public async Task LoadPdfAsync(string pdfPath, string highlightJson)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException(pdfPath);

        if (!viewerReady)
            viewerReady = false;

        // Pastikan viewer sudah load
        if (!webView.Source?.ToString().Contains("viewer.html") ?? true)
            await LoadViewerAsync();

        await Task.Delay(300);

        // Copy file PDF ke folder pdf.js virtual host
        string pdfjsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "pdfjs");
        string fileName = Path.GetFileName(pdfPath);
        string dest = Path.Combine(pdfjsPath, fileName);

        if (!File.Exists(dest))
            File.Copy(pdfPath, dest, true);

        _pendingPdfVirtualUrl = $"https://pdf.local/{Uri.EscapeDataString(fileName)}";
        string viewerUrl = $"https://pdf.local/viewer.html?file={Uri.EscapeDataString(_pendingPdfVirtualUrl)}";
        webView.CoreWebView2.Navigate(viewerUrl);
        // ensure viewer loaded (navigate if needed)
        //if (webView.Source == null || !webView.Source.AbsoluteUri.Contains("viewer.html"))
        //{
        //    //string viewerUrl = $"https://pdf.local/viewer.html?file={Uri.EscapeDataString(_pendingPdfVirtualUrl)}";
        //    string viewerUrl = $"https://pdf.local/viewer.html";
        //    webView.CoreWebView2.Navigate(viewerUrl);
        //}
        //else
        //{
        //    string js = $"window.loadPdf('{_pendingPdfVirtualUrl}');";
        //    await Task.Delay(300);
        //    await webView.CoreWebView2.ExecuteScriptAsync(js);
        //}
        //string js = $"window.loadPdf('{_pendingPdfVirtualUrl}');";
        //await webView.CoreWebView2.ExecuteScriptAsync(js);
    }

    // ---------------------------
    // PUBLIC API: HIGHLIGHT
    // ---------------------------
    public async Task HighlightKeywordsAsync(string jsonKeywords)
    {
        // Simpan dulu jika viewer belum siap
        if (!viewerReady)
        {
            pendingHighlightJson = jsonKeywords;
            return;
        }

        string js = $"highlightMultiple({jsonKeywords});";

        try
        {
            await webView.CoreWebView2.ExecuteScriptAsync(js);
        }
        catch
        {
            // Jika viewer belum render
            await Task.Delay(600);
            await webView.CoreWebView2.ExecuteScriptAsync(js);
        }
    }

    // ---------------------------
    // CLEAR HIGHLIGHT
    // ---------------------------
    public async Task ClearHighlightsAsync()
    {
        await webView.CoreWebView2.ExecuteScriptAsync("clearAllHighlights();");
    }

    // ---------------------------
    // SCROLL TO FIRST HIGHLIGHT
    // ---------------------------
    public async Task ScrollToFirstHighlightAsync()
    {
        await webView.CoreWebView2.ExecuteScriptAsync("scrollToFirstHighlight();");
    }

    // ---------------------------
    // HELPER API: highlight C# array
    // ---------------------------
    public async Task HighlightKeywordsAsync(object arrayObject)
    {
        string json = JsonSerializer.Serialize(arrayObject);
        await HighlightKeywordsAsync(json);
    }
}