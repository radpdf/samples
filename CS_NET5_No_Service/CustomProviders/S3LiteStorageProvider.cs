using System;
using System.IO;

using Amazon.S3;
using Amazon.S3.Model;

using RadPdf.Lite;
using RadPdf.Integration;

namespace RadPdfCoreDemoNoService.CustomProviders
{
    // This example uses the Amazon Simple Storage Service (S3) API, see:
    // https://www.nuget.org/packages/AWSSDK.S3/
    // Using a key-value / NoSQL store like this, RAD PDF can easily scale with even the largest deployments.
    // Cosmos or Mongo DB could also be used, but each have relatively low (2 MB and 16 MB, respectively) document size limits.

    public class S3LiteStorageProvider : PdfLiteStorageProvider
    {
        private readonly string _bucketName;

        private readonly AmazonS3Client _client;

        const string Seperator = "/";

        public S3LiteStorageProvider() : base()
        {
            AppSettings settings = AppSettings.LoadAppSettings();

            // Get bucket name used by this code
            _bucketName = settings.S3BucketName;

            // Get client for our container (load credentials from default location) 
            _client = new AmazonS3Client();
        }

        public override void DeleteData(PdfLiteSession session)
        {
            string continuationToken = null;

            for ( ; ; )
            {
                var listRequest = new ListObjectsV2Request();
                listRequest.BucketName = _bucketName;
                listRequest.ContinuationToken = continuationToken;
                listRequest.Prefix = session.ID + Seperator;

                // List all objects
                var listResponse = _client.ListObjectsV2Async(listRequest);
                listResponse.Wait();

                // Init DeleteObjects request
                var delRequest = new DeleteObjectsRequest();
                foreach (S3Object obj in listResponse.Result.S3Objects)
                {
                    // Add key
                    delRequest.AddKey(obj.Key);
                }

                // Delete any objects we have added
                if (delRequest.Objects.Count > 0)
                {
                    var delResponse = _client.DeleteObjectsAsync(delRequest);
                    delResponse.Wait();
                }

                // If done
                if (!listResponse.Result.IsTruncated)
                {
                    return;
                }

                // Set continuation token for next loop
                continuationToken = listResponse.Result.NextContinuationToken;
            }
        }

        public override byte[] GetData(PdfLiteSession session, int subtype)
        {
            try
            {
                string key = CreateStorageKey(session, subtype);

                var getRequest = new GetObjectRequest();
                getRequest.BucketName = _bucketName;
                getRequest.Key = key;

                // Get object
                var getResponse = _client.GetObjectAsync(getRequest);
                getResponse.Wait();

                // Read stream and return
                using (var stream = getResponse.Result.ResponseStream)
                {
                    byte[] ret = new byte[getResponse.Result.ContentLength];

                    stream.Read(ret);
                    
                    return ret;
                }
            }
            catch (AmazonS3Exception ex)
            {
                // If not found, return null
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw ex;
            }
        }

        public override void SetData(PdfLiteSession session, int subtype, byte[] value)
        {
            string key = CreateStorageKey(session, subtype);

            var putRequest = new PutObjectRequest();
            putRequest.BucketName = _bucketName;
            putRequest.Key = key;
            putRequest.InputStream = new MemoryStream(value);

            // Put object
            var putResponse = _client.PutObjectAsync(putRequest);
            putResponse.Wait();
        }

        private static string CreateStorageKey(PdfLiteSession session, int subtype)
        {
            return session.ID.ToString("N") + Seperator + subtype.ToString();
        }
    }
}
