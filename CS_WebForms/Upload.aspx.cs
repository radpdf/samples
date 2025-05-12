using System;

partial class Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack && this.UploadControl.HasFile)
        {
            //Get PDF as byte array from upload
            byte[] pdfData = this.UploadControl.FileBytes;

            //Check file size (up to 10 MB)
            if (pdfData.Length <= 10485760)
            {
                try
                {
                    //Load PDF byte array into RAD PDF
                    this.PdfWebControl1.CreateDocument(this.UploadControl.FileName, pdfData);
                }
                catch
                {
                    this.UploadMessage.Text = "PDF file could not be loaded.";
                }
            }
            else
            {
                this.UploadMessage.Text = "File is too large. Please choose a file no larger than 1 MB.";
            }
        }

        //Check if PDF is loaded into RAD PDF
        if (this.PdfWebControl1.DocumentLoaded)
        {
            //File is loaded, hide the upload panel
            this.UploadPanel.Visible = false;
        }
        else
        {
            //No file was uploaded, do not show RAD PDF
            this.PdfWebControl1.Visible = false;
        }
    }
}