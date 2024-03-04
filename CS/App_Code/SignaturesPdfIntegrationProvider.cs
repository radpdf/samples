using System;
using System.Drawing;
using System.Web;

using RadPdf.Data.Document;
using RadPdf.Data.Document.Common;
using RadPdf.Data.Document.Objects;
using RadPdf.Data.Document.Objects.Shapes;
using RadPdf.Data.Document.Pages;
using RadPdf.Integration;

public class SignaturesPdfIntegrationProvider : PdfIntegrationProvider
{
    public SignaturesPdfIntegrationProvider()
        : base()
    {
        // Add the signature font of our choice.
        // This example uses Herr Von Muellerhoff which is an open source font (SIL Open Font License, Version 1.1).
        // Herr Von Muellerhoff must be installed on your server. The TrueType file is provided by Google here:
        // https://fonts.google.com/specimen/Herr+Von+Muellerhoff
        // https://github.com/google/fonts/tree/master/ofl/herrvonmuellerhoff
        this.FontResources.Add(new PdfFontResource("Signature", "Herr Von Muellerhoff, cursive, serif", "Herr Von Muellerhoff"));
    }

    private static void ApplyTimestamp(PdfDocument document, string timestampText)
    {
        const string TimestampCustomData = "timestamp";

        int dpi = Convert.ToInt32(document.Dpi);

        // Add our watermark on each page
        foreach (PdfPage p in document.Pages)
        {
            PdfTextShape timestamp = FindOnPage(p, TimestampCustomData) as PdfTextShape;

            // If not found
            if (null == timestamp)
            {
                timestamp = p.CreateObject(PdfObjectCreatable.ShapeText) as PdfTextShape;

                // Set custom data so we can find it next save and re-use it
                timestamp.CustomData = TimestampCustomData;
            }

            // Set position
            timestamp.Left = 0;
            timestamp.Top = dpi * 3;
            timestamp.Height = dpi / 2;
            timestamp.Width = p.Width;
            timestamp.Rotation = PdfRotation.Rotation0;

            // Set properties
            timestamp.Changeable = false;
            timestamp.Deletable = false;
            timestamp.Duplicatable = false;
            timestamp.HideFocusOutline = true;
            timestamp.Moveable = false;
            timestamp.Resizable = false;
            timestamp.Stylable = false;
            timestamp.Wrappable = true;

            // Set font
            timestamp.Font.Alignment = PdfHorizontalAlignment.AlignCenter;
            timestamp.Font.Color = new PdfColor(Color.Red);
            timestamp.Font.Size = dpi / 3;

            // Set text
            timestamp.Text = timestampText;
        }
    }

    private static PdfObject FindOnPage(PdfPage page, string searchForCustomData)
    {
        foreach (PdfObject o in page.Objects)
        {
            if (o.CustomData == searchForCustomData)
            {
                return o;
            }
        }

        return null;
    }

    public override void OnDocumentInit(DocumentInitEventArgs e)
    {
        base.OnDocumentInit(e);

        //Add web font for client side fall back if Herr Von Muellerhoff isn't installed on clients.
        e.ExternalStyle = "https://fonts.googleapis.com/css?family=Herr+Von+Muellerhoff";
    }

    public override void OnDocumentPrinting(DocumentPrintingEventArgs e)
    {
        base.OnDocumentPrinting(e);

        string timestampText = "Last Printed " + DateTime.UtcNow.ToString();

        ApplyTimestamp(e.Document, timestampText);
    }

    public override void OnDocumentSaving(DocumentSavingEventArgs e)
    {
        base.OnDocumentSaving(e);

        string timestampText = "Last Saved " + DateTime.UtcNow.ToString();

        ApplyTimestamp(e.Document, timestampText);
    }

    public override void OnDocumentSaved(DocumentSavedEventArgs e)
    {
        base.OnDocumentSaved(e);

        // Reload the document client side after saving
        e.ThenReloadDocument = true;
    }

    public override void ProcessObjectDataRequest(PdfDataContext context)
    {
        switch (context.Request.DataKey)
        {
            case "MyImageSignature":
                // Write a file to the response
                // Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                context.Response.WriteFile(HttpContext.Current.Server.MapPath(@"~/signatures/images/signature.gif"));
                break;
        }
    }
}
