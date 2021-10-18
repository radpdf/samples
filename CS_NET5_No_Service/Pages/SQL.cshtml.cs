using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;

using RadPdf.Web.UI;

namespace RadPdfCoreDemoNoService.Pages
{
    public class SqlModel : PageModel
    {
        private IHostingEnvironment _env;

        public SqlModel(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {
            string path = System.IO.Path.Combine(_env.WebRootPath, "pdfs", "RadPdfSampleForm.pdf");

            // Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            byte[] pdfData = System.IO.File.ReadAllBytes(path);

            // Create RAD PDF control
            PdfWebControl pdfWebControl1 = new PdfWebControl(HttpContext);

            // Create document from PDF data
            pdfWebControl1.CreateDocument("Document Name", pdfData);

            // Put control in ViewBag
            ViewData["PdfWebControl1"] = pdfWebControl1;
        }
    }
}