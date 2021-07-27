using System;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using RadPdf.Web.UI;

namespace CS_MVC4.Controllers
{
    public class BasicController : Controller
    {
        //
        // GET: /Basic/

        public ActionResult Index()
        {
            // Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            byte[] pdfData = System.IO.File.ReadAllBytes(Server.MapPath("~/Data/RadPdfSampleForm.pdf"));

            // Create RAD PDF control
            PdfWebControl pdfWebControl1 = new PdfWebControl();

            // Setup pdfWebControl1 with any properties which must be called before CreateDocument (optional)
            // e.g. pdfWebControl1.RenderDpi = 144;

            // Create document from PDF data
            pdfWebControl1.CreateDocument("Document Name", pdfData);

            // Put control in ViewBag
            ViewBag.PdfWebControl1 = pdfWebControl1;

            return View();
        }

    }
}
