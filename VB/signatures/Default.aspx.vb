Option Explicit On
Option Strict On

Imports System
Imports System.Drawing
Imports System.IO
Imports System.Web.UI.HtmlControls

Imports RadPdf.Data.Document
Imports RadPdf.Data.Document.Common
Imports RadPdf.Data.Document.Objects
Imports RadPdf.Data.Document.Objects.FormFields
Imports RadPdf.Data.Document.Objects.Shapes
Imports RadPdf.Data.Document.Pages

Partial Class signatures_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            ' Create new document for DEMO (It will be flattened on save. Replace PdfDocumentSettings.FlattenForm with PdfDocumentSettings.None to prevent this.)
            Me.PdfWebControl1.CreateDocument("Document_Name", 1, PdfPageSize.Letter, PdfDocumentSettings.FlattenForm)

            ' NOTE: To use PdfWebControlLite instead, change <radPdf:PdfWebControl ... to <radPdf:PdfWebControlLite ... in the .aspx file and replace the above line with:
            ' Dim settings As RadPdf.Lite.PdfLiteSettings = New RadPdf.Lite.PdfLiteSettings()
            ' settings.DocumentSettings = PdfDocumentSettings.FlattenForm
            ' Me.PdfWebControl1.CreateDocument("Document_Name", 1, PdfPageSize.Letter, settings)

            ' Setup document editor
            Dim editor As PdfDocumentEditor = Me.PdfWebControl1.EditDocument()

            Dim dpi As Integer = Convert.ToInt32(editor.Document.Dpi)

            ' Create custom font form field example (It will be flattened on save. Replace "PdfDocumentSettings.FlattenForm" with "PdfDocumentSettings.None" above to prevent this.)
            Dim field As PdfTextField = DirectCast(editor.Pages(0).CreateObject(PdfObjectCreatable.FormFieldText), PdfTextField)
            field.Top = dpi
            field.Left = 3 * dpi \ 4
            field.Width = dpi * 2
            field.Height = dpi \ 2
            field.Font.Name = "Signature"
            field.Font.Size = dpi \ 4
            field.Deletable = False
            field.Duplicatable = False
            field.Moveable = False
            field.Name = "MySignatureField"
            field.Resizable = False
            field.Stylable = False

            ' Create image example
            Dim image As PdfImageShape = DirectCast(editor.Pages(0).CreateObject(PdfObjectCreatable.ShapeImage), PdfImageShape)
            image.Top = dpi
            image.Left = 13 * dpi \ 4
            image.Width = dpi * 2
            image.Height = dpi \ 2
            image.CustomData = "signature"
            image.Deletable = False
            image.Duplicatable = False
            image.Moveable = False
            image.Resizable = False
            image.Stylable = False
            image.ImageData = File.ReadAllBytes(Server.MapPath("~/signatures/images/empty.gif"))

            ' Create signature shape example
            Dim signature As PdfSignatureShape = DirectCast(editor.Pages(0).CreateObject(PdfObjectCreatable.ShapeSignature), PdfSignatureShape)
            signature.Top = dpi
            signature.Left = 23 * dpi \ 4
            signature.Width = dpi * 2
            signature.Height = dpi \ 2
            signature.Border = New PdfBorder(2, PdfBorderStyle.Solid, New PdfColor(Color.Black))
            signature.PenColor = New PdfColor(Color.Blue)
            signature.PenWidth = 2

            ' Create signature shape popup example
            Dim signaturePopup As PdfSignatureShape = DirectCast(editor.Pages(0).CreateObject(PdfObjectCreatable.ShapeSignature), PdfSignatureShape)
            signaturePopup.Top = 2 * dpi
            signaturePopup.Left = 13 * dpi \ 4
            signaturePopup.Width = dpi * 2
            signaturePopup.Height = dpi \ 2
            signaturePopup.Border = New PdfBorder(2, PdfBorderStyle.Solid, New PdfColor(Color.Black))
            signaturePopup.Font.Name = "Signature"
            signaturePopup.Font.Size = dpi / 4
            signaturePopup.PenColor = New PdfColor(Color.Blue)
            signaturePopup.PenWidth = 2
            signaturePopup.PopupInput = True

            ' Create hints
            Dim fHint As PdfTextShape = DirectCast(editor.Pages(0).CreateObject(PdfObjectCreatable.ShapeText), PdfTextShape)
            fHint.Top = 3 * dpi \ 2
            fHint.Left = 3 * dpi \ 4
            fHint.Width = dpi * 2
            fHint.Height = dpi \ 4
            fHint.Font.Name = "Arial"
            fHint.Font.Size = dpi \ 8
            fHint.Changeable = False
            fHint.Deletable = False
            fHint.Duplicatable = False
            fHint.HideFocusOutline = True
            fHint.Moveable = False
            fHint.OutputToPDF = False
            fHint.Print = False
            fHint.Resizable = False
            fHint.Stylable = False
            fHint.Text = "type a signature"

            Dim iHint As PdfTextShape = DirectCast(editor.Pages(0).CreateObject(PdfObjectCreatable.ShapeText), PdfTextShape)
            iHint.Top = 3 * dpi \ 2
            iHint.Left = 13 * dpi \ 4
            iHint.Width = dpi * 2
            iHint.Height = dpi \ 4
            iHint.Font.Name = "Arial"
            iHint.Font.Size = dpi \ 8
            iHint.Changeable = False
            iHint.Deletable = False
            iHint.Duplicatable = False
            iHint.HideFocusOutline = True
            iHint.Moveable = False
            iHint.OutputToPDF = False
            iHint.Print = False
            iHint.Resizable = False
            iHint.Stylable = False
            iHint.Text = "click above to sign via image"

            Dim sHint As PdfTextShape = DirectCast(editor.Pages(0).CreateObject(PdfObjectCreatable.ShapeText), PdfTextShape)
            sHint.Top = 3 * dpi \ 2
            sHint.Left = 23 * dpi \ 4
            sHint.Width = dpi * 2
            sHint.Height = dpi \ 4
            sHint.Font.Name = "Arial"
            sHint.Font.Size = dpi \ 8
            sHint.Changeable = False
            sHint.Deletable = False
            sHint.Duplicatable = False
            sHint.HideFocusOutline = True
            sHint.Moveable = False
            sHint.OutputToPDF = False
            sHint.Print = False
            sHint.Resizable = False
            sHint.Stylable = False
            sHint.Text = "draw a signature with your mouse"

            Dim pHint As PdfTextShape = DirectCast(editor.Pages(0).CreateObject(PdfObjectCreatable.ShapeText), PdfTextShape)
            pHint.Top = 5 + 5 * dpi \ 2
            pHint.Left = 13 * dpi \ 4
            pHint.Width = dpi * 2
            pHint.Height = dpi \ 4
            pHint.Font.Name = "Arial"
            pHint.Font.Size = dpi / 8
            pHint.Changeable = False
            pHint.Deletable = False
            pHint.Duplicatable = False
            pHint.HideFocusOutline = True
            pHint.Moveable = False
            pHint.OutputToPDF = False
            pHint.Print = False
            pHint.Resizable = False
            pHint.Stylable = False
            pHint.Text = "click above to sign via popup"

            ' Save changes
            editor.Save()

        End If
    End Sub
End Class