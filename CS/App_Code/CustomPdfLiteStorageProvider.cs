using System;
using System.IO;
using System.Web;

using RadPdf.Lite;

// Not in use by default
// Uncomment the approprate line in CustomPdfIntegrationProvider.cs
public class CustomPdfLiteStorageProvider : PdfLiteStorageProvider
{
    private readonly DirectoryInfo _dir;

    public CustomPdfLiteStorageProvider(string path)
        : base()
    {
        _dir = new DirectoryInfo(path);

        if (!_dir.Exists)
        {
            _dir.Create();
        }
    }

    private string GetPath(PdfLiteSession session, int subtype)
    {
        return Path.Combine(_dir.FullName, session.ID.ToString("N") + "-" + subtype.ToString() + ".dat");
    }

    public override void DeleteData(PdfLiteSession session)
    {
        FileInfo[] files = _dir.GetFiles(session.ID.ToString("N") + "*");

        foreach (FileInfo file in files)
        {
            lock (string.Intern(file.FullName))
            {
                file.Delete();
            }
        }
    }

    public override byte[] GetData(PdfLiteSession session, int subtype)
    {
        string path = GetPath(session, subtype);

        lock (string.Intern(path))
        {
            if (!File.Exists(path))
            {
                return null;
            }

            return File.ReadAllBytes(path);
        }
    }

    public override void SetData(PdfLiteSession session, int subtype, byte[] value)
    {
        string path = GetPath(session, subtype);

        lock (string.Intern(path))
        {
            File.WriteAllBytes(path, value);
        }
    }
}
