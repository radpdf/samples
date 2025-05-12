Option Explicit On
Option Strict On

Imports System.Diagnostics
Imports RadPdf.Integration

Partial Class Easy_Integration
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            Dim pdfData As Byte() = System.IO.File.ReadAllBytes(Server.MapPath("~/pdfs/RadPdfSampleForm.pdf"))

            'Load PDF byte array into RAD PDF
            Me.PdfWebControl1.CreateDocument("Document Name", pdfData)

        End If
    End Sub

    Protected Sub Saved(ByVal sender As Object, ByVal e As DocumentSavedEventArgs)

        'Check what raised the Saved event
        Select Case e.SaveType

            'When we are saving or downloading
            Case DocumentSaveType.Save, DocumentSaveType.Download

                'Get saved PDF
                Dim pdfData As Byte() = e.DocumentData

                'If desired, we could save the modified PDF to a file, database, send it via email, etc.
                'For example:
                'System.IO.File.WriteAllBytes("C:\output.pdf", pdfData)

                'Get its size
                Dim pdfSize As Integer = pdfData.Length

                'Create our message
                Dim message As String = String.Format("A PDF file of {0} bytes was just saved or downloaded!", pdfSize)

                'Add event to the Windows Application Log
                EventLog.WriteEntry("RAD PDF", message, EventLogEntryType.Information)


            Case Else

                'Ignore all other save types (Print, etc)

        End Select
    End Sub
End Class
