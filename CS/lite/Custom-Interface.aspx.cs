using System;
using RadPdf.Data.Document;

partial class Custom_Interface : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            //Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            byte[] pdfData = System.IO.File.ReadAllBytes(Server.MapPath("~/pdfs/RadPdfSampleForm.pdf"));

            //Load PDF byte array into RAD PDF
            this.PdfWebControl1.CreateDocument("Document Name", pdfData);
        }
        else
        {
            this.PdfSize.Text = "PDF size on last save: " + this.PdfWebControl1.GetPdf().Length.ToString() + " bytes";
        }
    }
}

