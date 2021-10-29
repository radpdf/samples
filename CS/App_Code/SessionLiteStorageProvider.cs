using System;
using System.Web;

using RadPdf.Lite;
using RadPdf.Integration;

public class SessionLiteStorageProvider : PdfLiteStorageProvider
{
    public SessionLiteStorageProvider() : base()
    {
        // IMPORTANT!
        // This storage mechanism is for demonstration purposes and not recommended in production.
        // It uses the session store (server memory) to store all document data, which will quickly require large amounts of memory.
    }

    public override void DeleteData(PdfLiteSession session)
    {
        throw new NotImplementedException();
    }

    public override byte[] GetData(PdfLiteSession session, int subtype)
    {
        string key = CreateStorageKey(session, subtype);


        return HttpContext.Current.Session[CreateSessionKey(session, subtype)] as byte[];
    }

    public override void SetData(PdfLiteSession session, int subtype, byte[] value)
    {
        string key = CreateStorageKey(session, subtype);

        HttpContext.Current.Session[CreateSessionKey(session, subtype)] = value;
    }

    private static string CreateStorageKey(PdfLiteSession session, int subtype)
    {
        return session.ID.ToString("N") + "-" + subtype.ToString();
    }

    private static string CreateSessionKey(PdfLiteSession session, int subtype)
    {
        return "RadPdf-" + session.ID.ToString("N") + "-" + subtype.ToString("X");
    }
}
