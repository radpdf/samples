using System;
#if NET40
using System.Collections.Concurrent;
#else
using System.Collections.Generic;
#endif
using System.Web;

using RadPdf.Lite;

// Not in use by default
// Uncomment the approprate line in CustomPdfIntegrationProvider.cs
public class CustomPdfLiteSessionProvider : PdfLiteSessionProvider
{
    // This example uses an in memory dictionary, which won't have  
    // persistent storage, but a database or other key /value store 
    // can easily be substituted. 
#if NET40
    private readonly ConcurrentDictionary<string, byte[]> _dict;
#else
    private readonly Dictionary<string, byte[]> _dict;
#endif

    public CustomPdfLiteSessionProvider()
        : base()
    {
#if NET40
        _dict = new ConcurrentDictionary<string, byte[]>();
#else
        _dict = new Dictionary<string, byte[]>();
#endif
    }

    public override string AddSession(PdfLiteSession session)
    {
        string key = GenerateKey();

#if !NET40
        lock (_dict)
        {
#endif
            _dict[key] = session.Serialize();
#if !NET40
        }
#endif
        return key;
    }

    public override PdfLiteSession GetSession(string key)
    {
        byte[] data;

#if !NET40
        lock (_dict)
        {
#endif
            data = _dict[key];
#if !NET40
        }
#endif

        if (null == data)
        {
            return null;
        }

        return PdfLiteSession.Deserialize(data);
    }
}
