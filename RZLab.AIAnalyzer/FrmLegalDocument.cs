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
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using UglyToad.PdfPig.Graphics;

namespace RZLab.AIAnalyzer
{
    public partial class FrmLegalDocument : Form
    {
        private ModernSpinner spinner;
        private AppSettingModel _appSetting = new();
        private bool _isDragging = false;
        private Point _offset;

        private DocumentDataModel? _selectedDocument = null;

        private readonly DocumentLegalStorageService _storageService;
        private readonly PdfExtractor _pdfExtractor;
        private readonly AnalysisViewerControl _aiSummaryViewer;
        private LegalDocumentSummaryCardControl _analysisViewer;
        public FrmLegalDocument()
        {
            InitializeComponent();

            _storageService = new DocumentLegalStorageService();
            _pdfExtractor = new PdfExtractor();
            _aiSummaryViewer = new AnalysisViewerControl();
            //tabPage2.Controls.Clear();
            //tabPage2.Controls.Add(_aiSummaryViewer);

            flowDocument.Padding = new Padding(5, 5, 5, 5);
            flowDocument.Margin = new Padding(0);

            // Anti flicker
            flowDocument.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance)!
                .SetValue(flowDocument, true, null);

            AttachSmoothAutoResize(flowDocument);

            InitializeLoader();
            LoadAppSetting();
            Initialize();
        }
        void LoadAppSetting()
        {
            var config = ConfigLoader.Load();
            _appSetting.OpenAI = new();
            _appSetting.Paths = new();

            _appSetting.OpenAI.ApiKey = config["OpenAI:ApiKey"];
            _appSetting.OpenAI.Model = config["OpenAI:Model"];
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
        void Initialize()
        {
            pnlHeader.MouseDown += Header_MouseDown;
            pnlHeader.MouseMove += Header_MouseMove;
            pnlHeader.MouseUp += Header_MouseUp;

            // Event untuk memindahkan form dari body (opsional)
            this.MouseDown += Form_MouseDown;
            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;

            this.flowDocument.Resize += flowCards_Resize;

            btnAnalyze.Visible = _selectedDocument != null;
            btnRefresh.Visible = _selectedDocument != null;
            lblStatus.Visible = _selectedDocument != null;
            //darkTabControl1.Visible = _selectedDocument != null;

            //DocumentLegalAnalyzerGridHelper.Setup(lstView);
            pnlHeaderDocument.Visible = false;
            pnlHeaderDocumentType.Visible = false;

            InitializeDocumentType();
            LoadDataGrid();
        }
        void InitializeDocumentType()
        {
            cmbDocumentType.Items.Clear();
            cmbDocumentType.Items.Add("Perjanjian Kontrak");
            cmbDocumentType.Items.Add("S&K Vendor");
            cmbDocumentType.Items.Add("Sewa Rumah");
            cmbDocumentType.Items.Add("Tagihan Invoice");
            cmbDocumentType.Items.Add("NDA MoU");
            cmbDocumentType.Items.Add("Perjanjian Kerja");

            cmbDocumentType.SelectedIndex = 0;
        }
        private void btnPackagePath_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "PDF Files|*.pdf;";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtJsonFilePath.Text = dlg.FileName;
                }
            }
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            var documentName = txtJsonFilePath.Text;

            // Copy file PDF ke folder pdf.js virtual host
            string workspacePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "pdfjs");
            string fileName = Path.GetFileName(documentName);
            string destFileName = Path.Combine(workspacePath, fileName);

            if (!File.Exists(destFileName))
                File.Copy(documentName, destFileName, true);

            // Extract
            var pdfMetadata = _pdfExtractor.Extract(destFileName);

            // AI processing
            //var result = await ai.Analyze(text);

            // Build document data
            var doc = new DocumentDataModel
            {
                document_id = Guid.NewGuid().ToString(),
                file_name = Path.GetFileName(documentName),
                file_path = documentName,
                uploaded_at = DateTime.Now,
                document_type = cmbDocumentType.Text,
                metadata = new MetadataModel
                {
                    page_count = pdfMetadata!.page_count,
                    file_size_kb = new FileInfo(documentName).Length / 1024
                },
                raw_text = pdfMetadata!.text_raw,
                analysis_result = new AnalysisResultModel()
            };

            _storageService.Save(doc);

            LoadDataGrid();
        }

        void LoadDataGrid()
        {
            var data = _storageService.LoadAll();
            DocumentLegalAnalyzerGridHelper.LoadData(flowDocument, data, _appSetting, (s) =>
            {
                OnViewDetailDocument(s);
            }, (s) => { });

            flowDocument.Controls.Add(new Panel { Height = 10 });
        }
        void OnViewDetailDocument(DocumentDataModel document)
        {
            _selectedDocument = document;
            btnAnalyze.Visible = _selectedDocument != null;
            btnRefresh.Visible = _selectedDocument != null;
            lblStatus.Visible = _selectedDocument != null;
            UpdateStatus(_selectedDocument!);
            //darkTabControl1.Visible = _selectedDocument != null;

            //PreviewDocument();
            SetAISummaryResult(document);
        }
        void SetAISummaryResult(DocumentDataModel model)
        {
            var pageCount = model.metadata.page_count;
            var analysResult = model.analysis_result;

            pnlAISummary.Controls.Clear();

            if (analysResult != null)
            {
                var summary = new LegalDocumentSummaryCardControl(analysResult.summary, pageCount, analysResult.risk_level, analysResult.recommendations);
                var clause = new LegalDocumentClauseCardControl(analysResult.clauses);

                pnlAISummary.Controls.Add(clause);
                pnlAISummary.Controls.Add(summary);
            }

        }
        void ViewDetail(object? sender)
        {
            if (sender == null) return;

            var model = sender as DocumentDataModel;
            if (model == null) return;

            var detail = new FrmDocumentLegalDetail(model, _appSetting);
            detail.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void Header_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _offset = e.Location;
            }
        }
        private void Header_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                this.Location = new Point(
                    this.Left + e.X - _offset.X,
                    this.Top + e.Y - _offset.Y);
            }
        }

        private void Header_MouseUp(object? sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void Form_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Y > 40) // 40 adalah tinggi header
            {
                _isDragging = true;
                _offset = e.Location;
            }
        }

        private void Form_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                this.Location = new Point(
                    this.Left + e.X - _offset.X,
                    this.Top + e.Y - _offset.Y);
            }
        }

        private void Form_MouseUp(object? sender, MouseEventArgs e)
        {
            _isDragging = false;
        }
        private void flowCards_Resize(object sender, EventArgs e)
        {
            foreach (Panel card in flowDocument.Controls)
            {

                card.Width = flowDocument.ClientSize.Width - 30;
            }
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
            //webView2.Invoke(new Action(() =>
            //{
            //    webView2.Visible = !isShow;
            //}));
            pnlLeftSide.Invoke(new Action(() =>
            {
                pnlLeftSide.Visible = !isShow;
            }));
            pnlRightSide.Invoke(new Action(() =>
            {
                pnlRightSide.Visible = !isShow;
            }));
        }

        private System.Windows.Forms.Timer resizeTimer;
        public void AttachSmoothAutoResize(FlowLayoutPanel flow)
        {
            resizeTimer = new System.Windows.Forms.Timer();
            resizeTimer.Interval = 8; // smooth debounce
            resizeTimer.Tick += (s, e) =>
            {
                resizeTimer.Stop();
                ApplyResize(flow);
            };

            flow.SizeChanged += (s, e) => resizeTimer.Start();
        }

        private void ApplyResize(FlowLayoutPanel flow)
        {
            flow.SuspendLayout();

            int targetWidth = flow.ClientSize.Width - 15;
            //if (targetWidth < 200) targetWidth = 200;

            foreach (Control ctrl in flow.Controls)
                ctrl.Width = targetWidth;

            flow.ResumeLayout(true);
        }
        private void WebView2_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            ShowLoader(false);
        }

        void UpdateStatus(DocumentDataModel documentData)
        {
            if (documentData.analysis_result != null)
            {
                lblStatus.Text = "ANALYZED";
                lblStatus.BackColor = Color.FromArgb(0, 140, 60); // hijau
            }
            else
            {
                lblStatus.Text = "NOT ANALYZED";
                lblStatus.BackColor = Color.FromArgb(180, 90, 0); // oranye
            }

            lblDocumentName.Text = documentData.file_name;
            lblDocumentType.Text = documentData.document_type;
            pnlHeaderDocument.Visible = true;
            pnlHeaderDocumentType.Visible = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (_selectedDocument != null) OnViewDetailDocument(_selectedDocument);
        }

        private async void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (_selectedDocument != null)
            {
                ShowLoader(true);
                await AnalyzeAsync();
            }
        }
        async Task AnalyzeAsync()
        {
            var pipeline = new ContractAnalysisPipeline();

            // 2. get keywords for this doc type
            var keywords = DocumentLegalKeywordProvider.GetKeywords(_selectedDocument!.document_type);

            // 3. run pipeline (async)
            try
            {
                var output = await pipeline.AnalyzeDocumentAsync(_selectedDocument!, keywords);
                var analysis = JsonSerializer.Deserialize<AnalysisResultModel>(output.summary,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                _storageService.SaveAnalysis(_selectedDocument!.document_id, analysis!);
                _selectedDocument!.analysis_result = analysis!;

                OnViewDetailDocument(_selectedDocument!);
                ShowLoader(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ShowLoader(false);
            }
        }
        void PreviewAISummary()
        {
            ShowLoader(true);
            _aiSummaryViewer.SetAnalysis(_selectedDocument!.analysis_result);
            ShowLoader(false);
        }

        private void btnViewDocument_Click(object sender, EventArgs e)
        {
            if (_selectedDocument == null) return;

            var documentDetail = new FrmDocumentLegalDetail(_selectedDocument, _appSetting);
            documentDetail.ShowDialog();
        }
    }
}
