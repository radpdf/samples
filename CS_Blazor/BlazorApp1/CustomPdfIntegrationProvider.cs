using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Web;

using RadPdf.Data.Document.Objects.Shapes;
using RadPdf.Integration;

public class CustomPdfIntegrationProvider : PdfIntegrationProvider
{
    public CustomPdfIntegrationProvider() : base()
    {
        // Replace this session provide with your own
        this.LiteSessionProvider = new CustomPdfLiteSessionProvider();
    }
}
