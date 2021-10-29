using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Web;

using RadPdf.Data.Document.Objects.Shapes;
using RadPdf.Integration;

public class NoServicePdfIntegrationProvider : PdfIntegrationProvider
{
    public NoServicePdfIntegrationProvider() : base()
    {
        // Set that we do not want to use the RAD PDF System Service
        this.AdvancedSettings.UseService = false;

        // Assign our resources file to be used by the PdfWebControl
        this.PdfWebControlResources = Resources.PdfWebControlResources.ResourceManager;

        // Setup a custom storage provider for Lite Documents because Service is usually the default
        this.LiteStorageProvider = new SessionLiteStorageProvider();
    }
}
