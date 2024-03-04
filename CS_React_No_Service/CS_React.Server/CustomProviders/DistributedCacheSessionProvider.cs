using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using RadPdf.Integration;
using RadPdf.Lite;

namespace CS_React.Server.CustomProviders
{
    public class DistributedCacheSessionProvider : PdfLiteSessionProvider, IUpdateableLiteSessionProvider
    {
        public override string AddSession(HttpContext context, PdfLiteSession session)
        {
            string key = GenerateKey();

            context.RequestServices.GetService<IDistributedCache>().Set(key, session.Serialize());

            return key;
        }

        public override PdfLiteSession GetSession(HttpContext context, string key)
        {
            byte[] data = context.RequestServices.GetService<IDistributedCache>().Get(key);

            if (null == data)
            {
                return null;
            }

            return PdfLiteSession.Deserialize(data);
        }

        public void UpdateSession(HttpContext context, string key, PdfLiteSession session)
        {
            context.RequestServices.GetService<IDistributedCache>().Set(key, session.Serialize());
        }
    }
}
