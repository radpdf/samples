using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using RadPdf.Integration;
using RadPdf.Lite;

namespace CS_React.Server.CustomProviders
{
    public class MyIntegrationProvider : PdfIntegrationProvider
    {
        public MyIntegrationProvider()
        {
            LiteSessionProvider = new DistributedCacheSessionProvider();

            LiteStorageProvider = new DistributedCacheStorageProvider();
        }
    }
}
