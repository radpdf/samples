using System;

using RadPdf.Lite;

partial class lite_Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PdfLiteSettings settings = new PdfLiteSettings();
        settings.MaxPdfPages = 1000;
        settings.MaxPdfPageWidth = 100 * settings.RenderDpi;
        settings.MaxPdfPageHeight = 100 * settings.RenderDpi;

        // Create empty document to upload into
        this.PdfWebControl1.CreateEmptyDocument(settings);
    }
}