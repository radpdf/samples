using System;
using System.Collections.Generic;

using RadPdf.Integration;

namespace RadPdfCoreDemoNoService.CustomProviders
{
    public class CustomIntegrationProvider : PdfIntegrationProvider
    {
        public CustomIntegrationProvider() : base()
        {
            this.AdvancedSettings.UseService = false;

            // When using Lite Documents and no service, we must provide a custom storage
            // provider implementation because the default storage provider uses the System Service!
            // Here we have implemented a basic in-memory storage provider for demonstration purposes.
            this.LiteStorageProvider = new MemoryLiteStorageProvider();

            // It could better be implemented using something like Azure Blob Storage. (comment out the line above and uncomment below)
            //this.LiteStorageProvider = new BlobLiteStorageProvider();

            // It could also be implemented using a directory with read and write access on the local machine. (comment out the line above and uncomment below)
            //this.LiteStorageProvider = new FileLiteStorageProvider(@"C:\Windows\Temp\RadPdfTemp\");
        }
    }
}
