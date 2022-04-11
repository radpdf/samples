using System;

using RadPdf.Exceptions;

partial class offline_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string documentKey = Request["dk"];

        if (!string.IsNullOrEmpty(documentKey))
        {
            //Attempt to use old document key
            try
            {
                this.PdfWebControl1.DocumentKey = documentKey;
            }
            catch (RadPdfDocumentKeyNotFoundException)
            {
                documentKey = null;
            }
        }

        //If a document key was used to load the document
        if (string.IsNullOrEmpty(documentKey))
        {
            //Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            byte[] pdfData = System.IO.File.ReadAllBytes(Server.MapPath("~/pdfs/RadPdfSampleForm.pdf"));

            //Load PDF byte array into RAD PDF
            this.PdfWebControl1.CreateDocument("Document Name", pdfData);

            //Add dk parameter to query string for proper offline use and reload page
            Response.Redirect("Default.aspx?dk=" + this.PdfWebControl1.DocumentKey, true);
            return;
        }
    }
}