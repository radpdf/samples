Option Explicit On
Option Strict On

Imports System.Drawing
Imports RadPdf.Data.Document
Imports RadPdf.Data.Document.Common
Imports RadPdf.Data.Document.Objects
Imports RadPdf.Data.Document.Objects.FormFields
Imports RadPdf.Data.Document.Objects.Shapes

Partial Class lite_PDF_Editor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            Dim pdfData As Byte() = System.IO.File.ReadAllBytes(Server.MapPath("~/pdfs/RadPdfSampleForm.pdf"))

            'Load PDF byte array into RAD PDF
            Me.PdfWebControl1.CreateDocument("Document Name", pdfData)

            'Create PdfDocumentEditor object
            Dim DocumentEditor1 As PdfDocumentEditor = _
                Me.PdfWebControl1.EditDocument()

            'Fill out PDF field using field names
            DirectCast( _
              DocumentEditor1.Fields.Find("First Name"), _
              PdfTextField _
              ).Value = "John"
            DirectCast( _
              DocumentEditor1.Fields.Find("Last Name"), _
              PdfTextField _
              ).Value = "Smith"
            DirectCast( _
              DocumentEditor1.Fields.Find("Product Support"), _
              PdfCheckField _
              ).Checked = True

            'Add arrow object
            Dim a As PdfArrowShape = _
              DirectCast( _
              DocumentEditor1.Pages(0).CreateObject(PdfObjectCreatable.ShapeArrow), _
              PdfArrowShape)
            a.LineColor = New PdfColor(Color.Blue)
            a.LineWidth = 2
            a.SetLine(200, 220, 40, 160)
            a.Moveable = False
            a.Resizable = False
            a.Stylable = False
            a.Deletable = False
            a.Duplicatable = False

            'Add text form field object
            Dim f As PdfTextField = _
              DirectCast( _
              DocumentEditor1.Pages(0).CreateObject(PdfObjectCreatable.FormFieldText), _
              PdfTextField)
            f.Resizable = False
            f.Stylable = False
            f.Deletable = False
            f.Duplicatable = False

            'Commit PdfDocumentEditor changes
            DocumentEditor1.Save()

        End If
    End Sub
End Class
