Option Explicit On
Option Strict On

Partial Class _Upload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack And Me.UploadControl.HasFile Then

            'Get PDF as byte array from upload
            Dim pdfData As Byte() = Me.UploadControl.FileBytes

            'Check file size (up to 1 MB)
            If pdfData.Length <= 1048576 Then

                Try

                    'Load PDF byte array into RAD PDF
                    Me.PdfWebControl1.CreateDocument(Me.UploadControl.FileName, pdfData)

                Catch

                    Me.UploadMessage.Text = "PDF file could not be loaded."

                End Try

            Else

                Me.UploadMessage.Text = "File is too large. Please choose a file no larger than 1 MB."

            End If

        End If

        'Check if PDF is loaded into RAD PDF
        If Me.PdfWebControl1.DocumentLoaded Then

            'File is loaded, hide the upload panel
            Me.UploadPanel.Visible = False

        Else

            'No file was uploaded, do not show RAD PDF
            Me.PdfWebControl1.Visible = False

        End If
    End Sub
End Class