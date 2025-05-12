Option Explicit On
Option Strict On

Imports RadPdf.Data.Document

Partial Class Custom_Interface
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            Dim pdfData As Byte() = System.IO.File.ReadAllBytes(Server.MapPath("~/pdfs/RadPdfSampleForm.pdf"))

            'Load PDF byte array into RAD PDF
            Me.PdfWebControl1.CreateDocument("Document Name", pdfData)

        Else

            Me.PdfSize.Text = "PDF size on last save: " & Me.PdfWebControl1.GetPdf().Length.ToString() & " bytes"
        End If
    End Sub
End Class

