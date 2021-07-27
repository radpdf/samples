using System;
using System.IO;

using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using RadPdf.Lite;
using RadPdf.Integration;

namespace RadPdfCoreDemoNoService.CustomProviders
{
    // This example uses the Azure Storage Blob API, see:
    // https://www.nuget.org/packages/Microsoft.Azure.Storage.Blob/
    // Using a key-value / NoSQL database like this, RAD PDF can easily scale with even the largest deployments.
    // Cosmos or Mongo DB could also be used, but each have relatively low (2 MB and 16 MB, respectively) document size limits.

    public class BlobLiteStorageProvider : PdfLiteStorageProvider
    {
        private readonly BlobContainerClient _client;

        const string Seperator = "-";

        public BlobLiteStorageProvider() : base()
        {
            AppSettings settings = AppSettings.LoadAppSettings();

            string storageConnectionString = settings.AzureStorageConnectionString;

            // Retrieve storage account information from connection string.
            BlobServiceClient service = new BlobServiceClient(storageConnectionString);

            // Get client for our container
            _client = service.GetBlobContainerClient(settings.AzureStorageContainerName);
        }

        public override void DeleteData(PdfLiteSession session)
        {
            // Get blobs
            Pageable<BlobItem> blobs = _client.GetBlobs(BlobTraits.All, BlobStates.None, session.ID + Seperator);

            // Delete each
            foreach (BlobItem blob in blobs)
            {
                BlobClient client = _client.GetBlobClient(blob.Name);

                client.Delete();
            }
        }

        public override byte[] GetData(PdfLiteSession session, int subtype)
        {
            try
            {
                string key = CreateStorageKey(session, subtype);

                BlobClient client = _client.GetBlobClient(key);

                Response<BlobDownloadInfo> res = client.Download();

                using (MemoryStream ret = new MemoryStream())
                {
                    res.Value.Content.CopyTo(ret);

                    // Return our blob
                    return ret.ToArray();
                }
            }
            catch (RequestFailedException ex)
            {
                // If not found
                if (404 == ex.Status)
                {
                    return null;
                }

                throw ex;
            }
        }

        public override void SetData(PdfLiteSession session, int subtype, byte[] value)
        {
            string key = CreateStorageKey(session, subtype);

            BlobClient client = _client.GetBlobClient(key);

            using (MemoryStream stream = new MemoryStream(value))
            {
                // Add blob (and overwrite)
                client.Upload(stream, true);
            }
        }

        private static string CreateStorageKey(PdfLiteSession session, int subtype)
        {
            return session.ID.ToString("N") + Seperator + subtype.ToString();
        }
    }
}
