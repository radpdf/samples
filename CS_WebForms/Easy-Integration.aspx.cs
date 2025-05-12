using System;
using System.Diagnostics;
using RadPdf.Integration;

partial class Easy_Integration : System.Web.UI.Page
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
    }

    protected void Saved(object sender, DocumentSavedEventArgs e)
    {
        //Check what raised the Saved event
        switch (e.SaveType)
        {
            //When we are saving or downloading
            case DocumentSaveType.Save:
            case DocumentSaveType.Download:

                //Get saved PDF
                byte[] pdfData = e.DocumentData;

                //If desired, we could save the modified PDF to a file, database, send it via email, etc.
                //For example:
                //System.IO.File.WriteAllBytes(@"C:\output.pdf", pdfData);

                //Get its size
                int pdfSize = pdfData.Length;

                //Create our message
                string message = string.Format("A PDF file of {0} bytes was just saved or downloaded!", pdfSize);

                //Add event to the Windows Application Log
                EventLog.WriteEntry("RAD PDF", message, EventLogEntryType.Information);
                break;

            default:

                //Ignore all other save types (Print, etc)
                break;
        }
    }
}
