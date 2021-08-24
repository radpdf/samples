using System;

using Microsoft.Extensions.Configuration;

namespace RadPdfCoreDemoNoService
{
    public class AppSettings
    {
        public string AzureStorageConnectionString { get; set; }
        public string AzureStorageContainerName { get; set; }

        public string S3BucketName { get; set; }

        public static AppSettings LoadAppSettings()
        {
            IConfigurationRoot configRoot = new ConfigurationBuilder()
                .AddJsonFile("Settings.json")
                .Build();
            AppSettings appSettings = configRoot.Get<AppSettings>();
            return appSettings;
        }
    }
}
