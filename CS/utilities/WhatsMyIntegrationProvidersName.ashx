<%@ WebHandler Language="C#" Class="WhatsMyIntegrationProvidersName" %>

using System;
using System.Web;

public class WhatsMyIntegrationProvidersName : IHttpHandler
{
    public void ProcessRequest (HttpContext context)
    {
        // Change "CustomPdfIntegrationProvider" to the name (including any namespace changes) of your class
        Type myType = typeof(CustomPdfIntegrationProvider);

        context.Response.ContentType = "text/plain";
        context.Response.Write("The fully qualified name of my custom PdfIntegrationProvider class (" + myType.Name + ") is:\n");
        context.Response.Write(myType.AssemblyQualifiedName + "\n");
        context.Response.Write("\n");
        context.Response.Write("This value can be used in your web.config file in the <appSettings> section like this:\n");
        context.Response.Write("<?xml version=\"1.0\"?>\n");
        context.Response.Write("<configuration>\n");
        context.Response.Write("    <appSettings>\n");
        context.Response.Write("        <add key=\"RadPdfIntegrationProvider\" value=\"" + myType.AssemblyQualifiedName + "\"/>\n");
        context.Response.Write("        ...\n");
        context.Response.Write("    </appSettings>\n");
        context.Response.Write("    ...\n");
        context.Response.Write("</configuration>\n");
    }
 
    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
