using Microsoft.Web.WebView2.Core;
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
    public partial class FrmDocumentLegalDetail : Form
    {
        private ModernSpinner spinner;
        private readonly DocumentDataModel _documentData;
        public FrmDocumentLegalDetail(DocumentDataModel documentData)
        {
            InitializeComponent();

            _documentData = documentData;

            this.BackColor = Color.FromArgb(25, 25, 25);
            this.Text = "Document Viewer";

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
            pnlLoader.Visible = isShow;
            spinner.Visible = isShow;
            webView2.Visible = !isShow;
        }
    }
}
