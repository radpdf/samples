﻿using System;
using System.Collections.Generic;

using RadPdf.Integration;

namespace RadPdfDemoNoService.CustomProviders
{
    public class CustomIntegrationProvider : PdfIntegrationProvider
    {
        public CustomIntegrationProvider() : base()
        {
            this.AdvancedSettings.UseService = false;

            this.PdfWebControlResources = Resources.PdfWebControlResources.ResourceManager; //NOTE: PdfWebControlResources.resx uses BuildAction: EmbeddedResource CustomTool: PublicResXFileCodeGenerator, CustomToolNamespace: Resources

            // When using Lite Documents and no service, we must provide a custom storage
            // provider implementation because the default storage provider uses the System Service!
            // Here we have implemented a basic in-memory storage provider using a IDistributedCache
            // service instance for demonstration purposes.
            this.LiteStorageProvider = new DistributedCacheStorageProvider();

            // It could better be implemented using other key-value store like Amazon Simple Storage Service (S3) (comment out the line above and uncomment below)
            //this.LiteStorageProvider = new S3LiteStorageProvider();

            // Or Azure Blob Storage (comment out the line above and uncomment below)
            //this.LiteStorageProvider = new BlobLiteStorageProvider();

            // It could also be implemented using a directory with read and write access on the local machine. (comment out the line above and uncomment below)
            //this.LiteStorageProvider = new FileLiteStorageProvider(@"C:\Windows\Temp\RadPdfTemp\");
        }
    }
}
