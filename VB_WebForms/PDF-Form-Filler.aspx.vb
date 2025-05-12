Option Explicit On
Option Strict On

Imports RadPdf.Data.Document

Partial Class PDF_Form_Filler
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            Dim pdfData As Byte() = System.IO.File.ReadAllBytes(Server.MapPath("~/pdfs/RadPdfSampleForm.pdf"))

            'Load PDF byte array into RAD PDF
            Me.PdfWebControl1.CreateDocument("Document Name", pdfData, _
              PdfDocumentSettings.IsReadOnlyExceptFormFields Or _
              PdfDocumentSettings.DisablePrint _
              )

        End If
    End Sub
End Class
