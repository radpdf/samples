using System;
using RadPdf.Data.Document;
using RadPdf.Lite;

partial class lite_PDF_Form_Filler : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            byte[] pdfData = System.IO.File.ReadAllBytes(Server.MapPath("~/pdfs/RadPdfSampleForm.pdf"));

            //Create settings for Lite Document
            PdfLiteSettings settings = new PdfLiteSettings();
            settings.DocumentSettings = PdfDocumentSettings.IsReadOnlyExceptFormFields | PdfDocumentSettings.DisablePrint;

            //Load PDF byte array into RAD PDF
            this.PdfWebControl1.CreateDocument("Document Name", pdfData, settings);
        }
    }
}
