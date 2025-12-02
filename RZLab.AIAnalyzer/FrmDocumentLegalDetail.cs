using Microsoft.Web.WebView2.Core;
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

        private readonly AnalysisViewerControl _aiSummaryViewer;
        public FrmDocumentLegalDetail(DocumentDataModel documentData, AppSettingModel appSetting)
        {
            InitializeComponent();

            _storageService = new DocumentLegalStorageService();
            _appSetting = appSetting;
            _documentData = documentData;

            this.BackColor = Color.FromArgb(25, 25, 25);
            this.Text = "Document Viewer";

            _aiSummaryViewer = new AnalysisViewerControl();
            _aiSummaryViewer.Dock = DockStyle.Fill;
            tabPage2.Controls.Clear();
            tabPage2.Controls.Add(_aiSummaryViewer);

            InitializeLoader();
            Initialize();
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
        }
        void Initialize()
        {
            if (_documentData.analysis_result.summary != null)
            {
                btnAnalyze.Text = "Re-Analyze";
                lblStatus.Text = "ANALYZED";
                lblStatus.BackColor = Color.FromArgb(0, 140, 60); // hijau
            }
            else
            {
                btnAnalyze.Text = "Analyze";
                lblStatus.Text = "NOT ANALYZED";
                lblStatus.BackColor = Color.FromArgb(180, 90, 0); // oranye
            }

            lblDocumentName.Text = _documentData.file_name;
            lblDocumentType.Text = _documentData.document_type;
            PreviewDocument();
        }

        async void PreviewDocument()
        {
            try
            {
                await webView2.EnsureCoreWebView2Async(null);

                ShowLoader(true);

                // Handle event after PDF load
                webView2.NavigationCompleted += WebView2_NavigationCompleted;
                // Load PDF
                webView2.CoreWebView2.Navigate(_documentData.file_path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka PDF: " + ex.Message);
            }
        }

        private async void btnAnalyze_Click(object sender, EventArgs e)
        {
            ShowLoader(true);
            await AnalyzeAsync(_documentData);
        }

        async Task AnalyzeAsync(DocumentDataModel model)
        {
            var pipeline = new ContractAnalysisPipeline();

            // 2. get keywords for this doc type
            var keywords = DocumentLegalKeywordProvider.GetKeywords(model.document_type);

            // 3. run pipeline (async)
            try
            {
                var output = await pipeline.AnalyzeDocumentAsync(model, keywords);
                var analysis = JsonSerializer.Deserialize<AnalysisResultModel>(output.summary,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                _storageService.SaveAnalysis(model.document_id, analysis!);
                _documentData = _storageService.Get(model.document_id);
                //Console.WriteLine("Analysis summary:");
                //Console.WriteLine(analysis.summary);
                //Console.WriteLine("Risks:");
                //foreach (var r in analysis.risks) Console.WriteLine(" - " + r);
                Initialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            ShowLoader(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void WebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            ShowLoader(false);
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
            webView2.Invoke(new Action(() =>
            {
                webView2.Visible = !isShow;
            }));
            btnAnalyze.Invoke(new Action(() =>
            {
                btnAnalyze.Visible = !isShow;
            }));
        }

        private void darkTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (darkTabControl1.SelectedIndex == 0) PreviewDocument();
            else if (darkTabControl1.SelectedIndex == 1) PreviewAISummary();
        }

        void PreviewAISummary()
        {
            ShowLoader(true);
            var analysisResultViewerModel = new AnalysisResultViewerModel();
            analysisResultViewerModel.risks = _documentData.analysis_result.risks;
            analysisResultViewerModel.summary = _documentData.analysis_result.summary;
            analysisResultViewerModel.risk_level = _documentData.analysis_result.risk_level;
            analysisResultViewerModel.recommendations = _documentData.analysis_result.recommendations;
            analysisResultViewerModel.clauses = _documentData.analysis_result.clauses.Select(x => new ClauseViewerModel
            {
                title = x.title,
                content = x.content,
                risk = x.risk,
            }).ToList();
            _aiSummaryViewer.SetAnalysis(analysisResultViewerModel);
            ShowLoader(false);
        }
    }
}
