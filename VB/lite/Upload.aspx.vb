Option Explicit On
Option Strict On

Imports RadPdf.Lite

Partial Class lite_Upload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim settings As PdfLiteSettings = New PdfLiteSettings()
            settings.MaxPdfPages = 1000
            settings.MaxPdfPageWidth = 100 * settings.RenderDpi
            settings.MaxPdfPageHeight = 100 * settings.RenderDpi

            ' Create empty document to upload into
            Me.PdfWebControl1.CreateEmptyDocument("RadPdfDemo", settings)

        End If
    End Sub
End Class
