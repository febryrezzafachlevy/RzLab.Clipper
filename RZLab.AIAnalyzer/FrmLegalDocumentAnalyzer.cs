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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RZLab.AIAnalyzer
{
    public partial class FrmLegalDocumentAnalyzer : Form
    {
        private ModernSpinner spinner;
        private AppSettingModel _appSetting = new();
        private bool _isDragging = false;
        private Point _offset;

        private readonly DocumentLegalStorageService _storageService;
        private readonly PdfExtractor _pdfExtractor;

        public FrmLegalDocumentAnalyzer()
        {
            InitializeComponent();

            _storageService = new DocumentLegalStorageService();
            _pdfExtractor = new PdfExtractor();

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

            //DocumentLegalAnalyzerGridHelper.Setup(lstView);

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

            // Extract
            var pdfMetadata = _pdfExtractor.Extract(txtJsonFilePath.Text);

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
        async void PreviewDocument(DocumentDataModel document)
        {
            try
            {
                await webView2.EnsureCoreWebView2Async(null);

                ShowLoader(true);

                // Handle event after PDF load
                webView2.NavigationCompleted += WebView2_NavigationCompleted;
                // Load PDF
                webView2.CoreWebView2.Navigate(document.file_path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka PDF: " + ex.Message);
            }
        }

        void LoadDataGrid()
        {
            var data = _storageService.LoadAll();
            DocumentLegalAnalyzerGridHelper.LoadData(flowDocument, data, _appSetting, (s) => PreviewDocument(s), (s) => { });

            flowDocument.Controls.Add(new Panel { Height = 10 });
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
            webView2.Invoke(new Action(() =>
            {
                webView2.Visible = !isShow;
            }));
            pnlBodyContainer.Invoke(new Action(() =>
            {
                pnlBodyContainer.Visible = !isShow;
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
        private void WebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            ShowLoader(false);
        }
    }
}
