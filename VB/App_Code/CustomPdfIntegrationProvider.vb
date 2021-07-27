Option Explicit On
Option Strict On

Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Web

Imports RadPdf.Data.Document.Objects.Shapes
Imports RadPdf.Integration

Public Class CustomPdfIntegrationProvider
    Inherits PdfIntegrationProvider

    Public Sub New()
        MyBase.New()

        ' Assign our resources file to be used by the PdfWebControl
        Me.PdfWebControlResources = Resources.PdfWebControlResources.ResourceManager

        ' Uncomment this line to use a custom PdfLiteSessionProvider
        'Me.LiteSessionProvider = New CustomPdfLiteSessionProvider()

        ' Uncomment this line to use a custom PdfLiteStorageProvider
        ' The directory used must allow the web application user to read and write
        'Me.LiteStorageProvider = New CustomPdfLiteStorageProvider("C:\RadPdfLiteStorageProvider")

    End Sub

    Public Overrides Sub ProcessObjectDataRequest(ByVal context As PdfDataContext)

        Select Case context.Request.DataKey

            Case "dynamic"
                ' Create a dynamic image showing the date (200px by 50px)
                Using bmp As New Bitmap(200, 50, PixelFormat.Format32bppArgb)

                    ' Create graphics object
                    Using gr As Graphics = Graphics.FromImage(bmp)

                        ' Set smoothing mode
                        gr.SmoothingMode = SmoothingMode.AntiAlias

                        ' Get the rect for the bitmap
                        Dim rect As RectangleF = gr.VisibleClipBounds

                        ' Create a new brush to draw background with
                        Using br As Brush = New SolidBrush(Color.Yellow)

                            ' Draw background
                            gr.FillRectangle(br, rect)

                        End Using

                        ' Create a new brush to draw text with
                        Using br As Brush = New SolidBrush(Color.Black)

                            ' Create a new font to draw text with
                            Using ft As New Font("Arial", 20.0F, FontStyle.Regular, GraphicsUnit.Pixel)

                                ' Create string format to draw text with
                                Using sf As New StringFormat()

                                    ' Set format properties
                                    sf.Alignment = StringAlignment.Center
                                    sf.LineAlignment = StringAlignment.Center

                                    ' Draw current date to bitmap
                                    gr.DrawString(DateTime.Now.ToString("yyyy-MM-dd" & vbLf & "hh:mm tt"), ft, br, rect, sf)

                                End Using
                            End Using
                        End Using
                    End Using

                    ' Create output strea
                    Using ms As New MemoryStream()

                        ' Save image to stream
                        bmp.Save(ms, ImageFormat.Gif)

                        ' Write bytes to the response
                        context.Response.Write(ms.ToArray())
                    End Using
                End Using

            Case "signature"
                ' Write a file to the response
                ' Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                context.Response.WriteFile(HttpContext.Current.Server.MapPath("~/images/signature.gif"))

        End Select

    End Sub

    Public Overrides Sub OnObjectDataAdding(ByVal e As ObjectDataAddingEventArgs)
        MyBase.OnObjectDataAdding(e)

        ' If data is added to an image
        If e.PdfObjectType Is GetType(PdfImageShape) Then

            ' Check image size (if larger than 1 MB)
            If e.Data.Length > &H100000 Then

                ' Cancel object data adding and display a message
                e.Cancel = True
                e.CancelMessage = "Maximum image size is 1 MB."
            End If

        Else
            Throw New ArgumentException("PdfObjectType unsupported.")
        End If

    End Sub

End Class
