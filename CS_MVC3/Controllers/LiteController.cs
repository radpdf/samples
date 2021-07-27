using System;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using RadPdf.Lite;
using RadPdf.Web.UI;

namespace CS_MVC3.Controllers
{
    public class LiteController : Controller
    {
        //
        // GET: /Lite/

        public ActionResult Index()
        {
            // Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            byte[] pdfData = System.IO.File.ReadAllBytes(Server.MapPath("~/Data/RadPdfSampleForm.pdf"));

            // Create RAD PDF control
            PdfWebControlLite pdfWebControlLite1 = new PdfWebControlLite();

            // Create settings
            PdfLiteSettings settings = new PdfLiteSettings();

            // Setup pdfWebControl1 with any properties which must be called before CreateDocument (optional)
            // e.g. settings.RenderDpi = 144;

            // Create document from PDF data
            pdfWebControlLite1.CreateDocument("Document Name", pdfData, settings);

            // Put control in ViewBag
            ViewBag.PdfWebControlLite1 = pdfWebControlLite1;

            return View();
        }

    }
}
