Option Explicit On
Option Strict On

Imports System
Imports System.Drawing
Imports System.Web

Imports RadPdf.Data.Document
Imports RadPdf.Data.Document.Common
Imports RadPdf.Data.Document.Objects
Imports RadPdf.Data.Document.Objects.Shapes
Imports RadPdf.Data.Document.Pages
Imports RadPdf.Integration

Public Class SignaturesPdfIntegrationProvider
    Inherits PdfIntegrationProvider

    Public Sub New()
        MyBase.New()

        ' Add the signature font of our choice.
        ' This example uses Herr Von Muellerhoff which is an open source font (SIL Open Font License, Version 1.1).
        ' Herr Von Muellerhoff must be installed on your server. The TrueType file is provided by Google here:
        ' https://fonts.google.com/specimen/Herr+Von+Muellerhoff
        ' https://github.com/google/fonts/tree/master/ofl/herrvonmuellerhoff
        Me.FontResources.Add(New PdfFontResource("Signature", "Herr Von Muellerhoff, cursive, serif", "Herr Von Muellerhoff"))

    End Sub

    Private Shared Sub ApplyTimestamp(ByVal document As PdfDocument, ByVal timestampText As String)

        Const TimestampCustomData As String = "timestamp"

        Dim dpi As Integer = Convert.ToInt32(document.Dpi)

        ' Add our watermark on each page
        For Each p As PdfPage In document.Pages

            Dim timestamp As PdfTextShape = DirectCast(FindOnPage(p, TimestampCustomData), PdfTextShape)

            ' If not found
            If (timestamp Is Nothing) Then
                timestamp = DirectCast(p.CreateObject(PdfObjectCreatable.ShapeText), PdfTextShape)

                ' Set custom data so we can find it next save and re-use it
                timestamp.CustomData = TimestampCustomData
            End If

            ' Set position
            timestamp.Left = 0
            timestamp.Top = dpi * 3
            timestamp.Height = dpi \ 2
            timestamp.Width = p.Width
            timestamp.Rotation = PdfRotation.Rotation0

            ' Set properties
            timestamp.Changeable = False
            timestamp.Deletable = False
            timestamp.Duplicatable = False
            timestamp.HideFocusOutline = True
            timestamp.Moveable = False
            timestamp.Resizable = False
            timestamp.Stylable = False
            timestamp.Wrappable = True

            ' Set font
            timestamp.Font.Alignment = PdfHorizontalAlignment.AlignCenter
            timestamp.Font.Color = New PdfColor(Color.Red)
            timestamp.Font.Size = dpi \ 3

            ' Set text
            timestamp.Text = timestampText
        Next

    End Sub

    Private Shared Function FindOnPage(ByVal page As PdfPage, ByVal searchForCustomData As String) As PdfObject

        For Each o As PdfObject In page.Objects

            If (o.CustomData = searchForcustomData) Then

                Return o

            End If
        Next

        Return Nothing

    End Function

    Public Overrides Sub OnDocumentInit(ByVal e As DocumentInitEventArgs)
        MyBase.OnDocumentInit(e)

        'Add Web font for client side
        e.ExternalStyle = "https://fonts.googleapis.com/css?family=Herr+Von+Muellerhoff"
    End Sub

    Public Overrides Sub OnDocumentPrinting(ByVal e As DocumentPrintingEventArgs)
        MyBase.OnDocumentPrinting(e)

        Dim timestampText As String = "Last Printed " & DateTime.UtcNow.ToString()

        ApplyTimestamp(e.Document, timestampText)
    End Sub

    Public Overrides Sub OnDocumentSaving(ByVal e As DocumentSavingEventArgs)
        MyBase.OnDocumentSaving(e)

        Dim timestampText As String = "Last Saved " & DateTime.UtcNow.ToString()

        ApplyTimestamp(e.Document, timestampText)
    End Sub

    Public Overrides Sub OnDocumentSaved(ByVal e As DocumentSavedEventArgs)
        MyBase.OnDocumentSaved(e)

        ' Reload the document client side after saving
        e.ThenReloadDocument = True
    End Sub

    Public Overrides Sub ProcessObjectDataRequest(ByVal context As PdfDataContext)

        Select Case context.Request.DataKey

            Case "MyImageSignature"
                ' Write a file to the response
                ' Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                context.Response.WriteFile(HttpContext.Current.Server.MapPath("~/signatures/images/signature.gif"))

        End Select
    End Sub

End Class