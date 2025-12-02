using RZLab.AIAnalyzer.Helpers;
using RZLab.Clipper.Core;
using RZLab.Clipper.Core.DocumentLegal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Formats.Tar;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RZLab.AIAnalyzer
{
    public partial class FrmDocumentLegalAnalyzer : Form
    {
        private ModernSpinner spinner;
        private AppSettingModel _appSetting = new();
        private bool _isDragging = false;
        private Point _offset;

        private readonly DocumentLegalStorageService _storageService;
        private readonly PdfExtractor _pdfExtractor;
        public FrmDocumentLegalAnalyzer()
        {
            InitializeComponent();
            _storageService = new DocumentLegalStorageService();
            _pdfExtractor = new PdfExtractor();

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

            this.flowCards.Resize += flowCards_Resize;

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

            _storageService.Append(doc);

            LoadDataGrid();
        }

        void LoadDataGrid()
        {
            var data = _storageService.LoadAll();
            DocumentLegalAnalyzerGridHelper.LoadData(flowCards, data, _appSetting,
                (s, e) => ViewDetail(s),
                (s, e) => AnalyzeAsync(s)
            );

            flowCards.Controls.Add(new Panel { Height = 20 });
        }
        void ViewDetail(object? sender)
        {
            if (sender == null) return;

            var model = sender as DocumentDataModel;
            if (model == null) return;

            var detail = new FrmDocumentLegalDetail(model);
            detail.ShowDialog();
        }
        async void AnalyzeAsync(object? sender)
        {
            if (sender == null) return;

            var model = sender as DocumentDataModel;
            if (model == null) return;

            var analyzer = new DocumentLegalAnalyzer(_appSetting.OpenAI.ApiKey, _appSetting.OpenAI.Model);

            var output = await analyzer.Analyze(model!.raw_text);

            var analysis = JsonSerializer.Deserialize<AnalysisResultModel>(output.summary,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            _storageService.SaveAnalysis(model.document_id, analysis!);

            LoadDataGrid();
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
            foreach (Panel card in flowCards.Controls)
            {

                card.Width = flowCards.ClientSize.Width - 30;
            }
        }
        void ShowLoader(bool isShow)
        {
            pnlLoader.Visible = isShow;
            spinner.Visible = isShow;
            pnlBodyContainer.Visible = !isShow;
        }
    }
}
