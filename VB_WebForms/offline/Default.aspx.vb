Option Explicit On
Option Strict On

Imports RadPdf.Exceptions

Partial Class offline_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim documentKey As String = Request("dk")

        If Not String.IsNullOrEmpty(documentKey) Then

            ' Attempt to use old document key
            Try

                Me.PdfWebControl1.DocumentKey = documentKey

            Catch ex As RadPdfDocumentKeyNotFoundException

                documentKey = Nothing

            End Try
        End If

        ' If a document key was used to load the document
        If String.IsNullOrEmpty(documentKey) Then

            ' Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            Dim pdfData() As Byte = System.IO.File.ReadAllBytes(Server.MapPath("~/pdfs/RadPdfSampleForm.pdf"))

            ' Load PDF byte array into RAD PDF
            Me.PdfWebControl1.CreateDocument("Document Name", pdfData)

            ' Add dk parameter to query string for proper offline use and reload page
            Response.Redirect("Default.aspx?dk=" + Me.PdfWebControl1.DocumentKey, True)
            Return
        End If
    End Sub
End Class