using System;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;

using RadPdf.Data.Document;
using RadPdf.Data.Document.Common;
using RadPdf.Data.Document.Objects;
using RadPdf.Data.Document.Objects.FormFields;
using RadPdf.Data.Document.Objects.Shapes;
using RadPdf.Data.Document.Pages;

partial class signatures_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Create new document for DEMO
            this.PdfWebControl1.CreateDocument("Document_Name", 1, PdfPageSize.Letter, PdfDocumentSettings.FlattenForm);

            // NOTE: To use PdfWebControlLite instead, change <radPdf:PdfWebControl ... to <radPdf:PdfWebControlLite ... in the .aspx file and replace the above line with:
            // RadPdf.Lite.PdfLiteSettings settings = new RadPdf.Lite.PdfLiteSettings();
            // settings.DocumentSettings = PdfDocumentSettings.FlattenForm;
            // this.PdfWebControl1.CreateDocument("Document_Name", 1, PdfPageSize.Letter, settings);

            // Setup document editor
            PdfDocumentEditor editor = this.PdfWebControl1.EditDocument();

            int dpi = Convert.ToInt32(editor.Document.Dpi);

            // Create custom font form field example (It will be flattened on save. Replace "PdfDocumentSettings.FlattenForm" with "PdfDocumentSettings.None" above to prevent this.)
            PdfTextField field = editor.Pages[0].CreateObject(PdfObjectCreatable.FormFieldText) as PdfTextField;
            field.Top = dpi;
            field.Left = 3 * dpi / 4;
            field.Width = dpi * 2;
            field.Height = dpi / 2;
            field.Font.Name = "Signature";
            field.Font.Size = dpi / 4;
            field.Deletable = false;
            field.Duplicatable = false;
            field.Moveable = false;
            field.Name = "MySignatureField";
            field.Resizable = false;
            field.Stylable = false;

            // Create image example
            PdfImageShape image = editor.Pages[0].CreateObject(PdfObjectCreatable.ShapeImage) as PdfImageShape;
            image.Top = dpi;
            image.Left = 13 * dpi / 4;
            image.Width = dpi * 2;
            image.Height = dpi / 2;
            image.CustomData = "signature";
            image.Deletable = false;
            image.Duplicatable = false;
            image.Moveable = false;
            image.Resizable = false;
            image.Stylable = false;
            image.ImageData = File.ReadAllBytes(Server.MapPath(@"~/signatures/images/empty.gif"));

            // Create signature shape example
            PdfSignatureShape signature = editor.Pages[0].CreateObject(PdfObjectCreatable.ShapeSignature) as PdfSignatureShape;
            signature.Top = dpi;
            signature.Left = 23 * dpi / 4;
            signature.Width = dpi * 2;
            signature.Height = dpi / 2;
            signature.Border = new PdfBorder(2, PdfBorderStyle.Solid, new PdfColor(Color.Black));
            signature.PenColor = new PdfColor(Color.Blue);
            signature.PenWidth = 2;

            // Create signature shape popup example
            PdfSignatureShape signaturePopup = editor.Pages[0].CreateObject(PdfObjectCreatable.ShapeSignature) as PdfSignatureShape;
            signaturePopup.Top = 2 * dpi;
            signaturePopup.Left = 13 * dpi / 4;
            signaturePopup.Width = dpi * 2;
            signaturePopup.Height = dpi / 2;
            signaturePopup.Border = new PdfBorder(2, PdfBorderStyle.Solid, new PdfColor(Color.Black));
            signaturePopup.Font.Name = "Signature";
            signaturePopup.Font.Size = dpi / 4;
            signaturePopup.PenColor = new PdfColor(Color.Blue);
            signaturePopup.PenWidth = 2;
            signaturePopup.PopupInput = true;

            // Create hints
            PdfTextShape fHint = editor.Pages[0].CreateObject(PdfObjectCreatable.ShapeText) as PdfTextShape;
            fHint.Top = 5 + 3 * dpi / 2;
            fHint.Left = 3 * dpi / 4;
            fHint.Width = dpi * 2;
            fHint.Height = dpi / 4;
            fHint.Font.Name = "Arial";
            fHint.Font.Size = dpi / 8;
            fHint.Changeable = false;
            fHint.Deletable = false;
            fHint.Duplicatable = false;
            fHint.HideFocusOutline = true;
            fHint.Moveable = false;
            fHint.OutputToPDF = false;
            fHint.Print = false;
            fHint.Resizable = false;
            fHint.Stylable = false;
            fHint.Text = "type a signature";

            PdfTextShape iHint = editor.Pages[0].CreateObject(PdfObjectCreatable.ShapeText) as PdfTextShape;
            iHint.Top = 5 + 3 * dpi / 2;
            iHint.Left = 13 * dpi / 4;
            iHint.Width = dpi * 2;
            iHint.Height = dpi / 4;
            iHint.Font.Name = "Arial";
            iHint.Font.Size = dpi / 8;
            iHint.Changeable = false;
            iHint.Deletable = false;
            iHint.Duplicatable = false;
            iHint.HideFocusOutline = true;
            iHint.Moveable = false;
            iHint.OutputToPDF = false;
            iHint.Print = false;
            iHint.Resizable = false;
            iHint.Stylable = false;
            iHint.Text = "click above to sign via image";

            PdfTextShape sHint = editor.Pages[0].CreateObject(PdfObjectCreatable.ShapeText) as PdfTextShape;
            sHint.Top = 5 + 3 * dpi / 2;
            sHint.Left = 23 * dpi / 4;
            sHint.Width = dpi * 2;
            sHint.Height = dpi / 4;
            sHint.Font.Name = "Arial";
            sHint.Font.Size = dpi / 8;
            sHint.Changeable = false;
            sHint.Deletable = false;
            sHint.Duplicatable = false;
            sHint.HideFocusOutline = true;
            sHint.Moveable = false;
            sHint.OutputToPDF = false;
            sHint.Print = false;
            sHint.Resizable = false;
            sHint.Stylable = false;
            sHint.Text = "draw a signature with your mouse";

            PdfTextShape pHint = editor.Pages[0].CreateObject(PdfObjectCreatable.ShapeText) as PdfTextShape;
            pHint.Top = 5 + 5 * dpi / 2;
            pHint.Left = 13 * dpi / 4;
            pHint.Width = dpi * 2;
            pHint.Height = dpi / 4;
            pHint.Font.Name = "Arial";
            pHint.Font.Size = dpi / 8;
            pHint.Changeable = false;
            pHint.Deletable = false;
            pHint.Duplicatable = false;
            pHint.HideFocusOutline = true;
            pHint.Moveable = false;
            pHint.OutputToPDF = false;
            pHint.Print = false;
            pHint.Resizable = false;
            pHint.Stylable = false;
            pHint.Text = "click above to sign via popup";

            // Save changes
            editor.Save();
        }
    }
}