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

        // Run without the RAD PDF System Service? (uncomment the next two lines)
        // this.AdvancedSettings.UseService = false;
        // this.LiteStorageProvider = new MemoryLiteStorageProvider(); // NOTE: This storage mechanism is for demonstration purposes and not recommended in production
    }
}
