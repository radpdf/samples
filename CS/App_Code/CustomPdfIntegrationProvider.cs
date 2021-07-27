using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Web;

using RadPdf.Data.Document.Objects.Shapes;
using RadPdf.Integration;

public class CustomPdfIntegrationProvider : PdfIntegrationProvider
{
    public CustomPdfIntegrationProvider() : base()
    {
        // Assign our resources file to be used by the PdfWebControl
        this.PdfWebControlResources = Resources.PdfWebControlResources.ResourceManager;

        // Uncomment this line to use a custom PdfLiteSessionProvider
        //this.LiteSessionProvider = new CustomPdfLiteSessionProvider();

        // Uncomment this line to use a custom PdfLiteStorageProvider
        // The directory used must allow the web application user to read and write
        //this.LiteStorageProvider = new CustomPdfLiteStorageProvider(@"C:\RadPdfLiteStorageProvider");

        // OR

        // Uncomment this line to use a custom SQL Server database as the session provider
        //this.LiteSessionProvider = new SqlServerPdfLiteSessionProvider(@"Server=.;Database=RadPdf;Trusted_Connection=Yes");

        // Uncomment this line to use a custom SQL Server database as the storage provider
        //this.LiteStorageProvider = new SqlServerPdfLiteStorageProvider(@"Server=.;Database=RadPdf;Trusted_Connection=Yes");
    }

    public override void ProcessObjectDataRequest(PdfDataContext context)
    {
        switch(context.Request.DataKey)
        {
            case "dynamic":
                // Create a dynamic image showing the date (200px by 50px)
                using (Bitmap bmp = new Bitmap(200, 50, PixelFormat.Format32bppArgb))
                {
                    // Create graphics object
                    using (Graphics gr = Graphics.FromImage(bmp))
                    {
                        // Set smoothing mode
                        gr.SmoothingMode = SmoothingMode.AntiAlias;

                        // Get the rect for the bitmap
                        RectangleF rect = gr.VisibleClipBounds;

                        // Create a new brush to draw background with
                        using (Brush br = new SolidBrush(Color.Yellow))
                        {
                            //Draw background
                            gr.FillRectangle(br, rect);
                        }

                        // Create a new brush to draw text with
                        using (Brush br = new SolidBrush(Color.Black))
                        {
                            // Create a new font to draw text with
                            using(Font ft = new Font("Arial", 20.0f, FontStyle.Regular, GraphicsUnit.Pixel))
                            {
                                // Create string format to draw text with
                                using (StringFormat sf = new StringFormat())
                                {
                                    // Set format properties
                                    sf.Alignment = StringAlignment.Center;
                                    sf.LineAlignment = StringAlignment.Center;

                                    // Draw current date to bitmap
                                    gr.DrawString(DateTime.Now.ToString("yyyy-MM-dd\nhh:mm tt"), ft, br, rect, sf);
                                }
                            }
                        }
                    }
                    
                    // Create output strea
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Save image to stream
                        bmp.Save(ms, ImageFormat.Gif);

                        // Write bytes to the response
                        context.Response.Write(ms.ToArray());
                    }
                }
                break;

            case "signature":
                // Write a file to the response
                // Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                context.Response.WriteFile(HttpContext.Current.Server.MapPath(@"~/images/signature.gif"));
                break;
        }
    }

    public override void OnObjectDataAdding(ObjectDataAddingEventArgs e)
    {
        base.OnObjectDataAdding(e);

        // If data is added to an image
        if (e.PdfObjectType == typeof(PdfImageShape))
        {
            // Check image size (if larger than 1 MB)
            if (e.Data.Length > 0x100000)
            {
                // Cancel object data adding and display a message
                e.Cancel = true;
                e.CancelMessage = "Maximum image size is 1 MB.";
            }
        }
        else
        {
            throw new ArgumentException("PdfObjectType unsupported.");
        }
    }
}
