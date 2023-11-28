using System;
using System.Collections.Concurrent;

using RadPdf.Lite;
using RadPdf.Integration;

namespace RadPdfDemoNoService.CustomProviders
{
    public class MemoryLiteStorageProvider : PdfLiteStorageProvider
    {
        private ConcurrentDictionary<string, byte[]> _storage;

        public MemoryLiteStorageProvider() : base()
        {
            // This storage mechanism is for demonstration purposes and not recommended in production.
            // It uses server memory as the storage location, which will require large amounts of memory.
            _storage = new ConcurrentDictionary<string, byte[]>();
        }

        public override void DeleteData(PdfLiteSession session)
        {
            throw new NotImplementedException();
        }

        public override byte[] GetData(PdfLiteSession session, int subtype)
        {
            string key = CreateStorageKey(session, subtype);

            byte[] data;
            if (_storage.TryGetValue(key, out data))
            {
                return data;
            }

            return null;
        }

        public override void SetData(PdfLiteSession session, int subtype, byte[] value)
        {
            string key = CreateStorageKey(session, subtype);

            _storage[key] = value;
        }

        private static string CreateStorageKey(PdfLiteSession session, int subtype)
        {
            return session.ID.ToString("N") + "-" + subtype.ToString();
        }
    }
}
