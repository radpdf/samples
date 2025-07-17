using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using RadPdf.Integration;
using RadPdf.Lite;
using System;

namespace RadPdfDemoNoService.CustomProviders
{
    public class DistributedCacheStorageProvider : PdfLiteStorageProvider
    {
        public override void DeleteData(PdfLiteSession session)
        {
            throw new NotImplementedException();
        }

        public override byte[] GetData(PdfLiteSession session, int subtype)
        {
            return session.HttpContext.RequestServices.GetService<IDistributedCache>().Get(session.ID.ToString() + "-" + subtype.ToString());
        }

        public override void SetData(PdfLiteSession session, int subtype, byte[] value)
        {
            session.HttpContext.RequestServices.GetService<IDistributedCache>().Set(session.ID.ToString() + "-" + subtype.ToString(), value);
        }
    }
}
