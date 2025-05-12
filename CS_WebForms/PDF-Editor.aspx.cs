using System;
using System.Drawing;
using RadPdf.Data.Document;
using RadPdf.Data.Document.Common;
using RadPdf.Data.Document.Objects;
using RadPdf.Data.Document.Objects.FormFields;
using RadPdf.Data.Document.Objects.Shapes;

partial class PDF_Editor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            byte[] pdfData = System.IO.File.ReadAllBytes(Server.MapPath("~/pdfs/RadPdfSampleForm.pdf"));

            //Load PDF byte array into RAD PDF
            this.PdfWebControl1.CreateDocument("Document Name", pdfData);

            //Create PdfDocumentEditor object
            PdfDocumentEditor DocumentEditor1 =
                this.PdfWebControl1.EditDocument();

            //Fill out PDF field using field names
            ((PdfTextField)
              DocumentEditor1.Fields.Find("First Name"))
              .Value = "John";
            ((PdfTextField)
              DocumentEditor1.Fields.Find("Last Name"))
              .Value = "Smith";
            ((PdfCheckField)
              DocumentEditor1.Fields.Find("Product Support"))
              .Checked = true;

            //Add arrow object
            PdfArrowShape a =
              (PdfArrowShape)
              DocumentEditor1.Pages[0].CreateObject(PdfObjectCreatable.ShapeArrow);
            a.LineColor = new PdfColor(Color.Blue);
            a.LineWidth = 2;
            a.SetLine(200, 220, 40, 160);
            a.Moveable = false;
            a.Resizable = false;
            a.Stylable = false;
            a.Deletable = false;
            a.Duplicatable = false;

            //Add text form field object
            PdfTextField f =
              (PdfTextField)
              DocumentEditor1.Pages[0].CreateObject(PdfObjectCreatable.FormFieldText);
            f.Resizable = false;
            f.Stylable = false;
            f.Deletable = false;
            f.Duplicatable = false;

            //Commit PdfDocumentEditor changes
            DocumentEditor1.Save();
        }
    }
}
