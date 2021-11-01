﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;

using RadPdf.Lite;
using RadPdf.Web.UI;

namespace RadPdfDemoNoService.Pages
{
    public class RenderAtClientModel : PageModel
    {
        private IHostingEnvironment _env;

        public RenderAtClientModel(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {
            string path = System.IO.Path.Combine(_env.WebRootPath, "pdfs", "RadPdfSampleForm.pdf");

            // Get PDF as byte array from file (or database, browser upload, remote storage, etc)
            byte[] pdfData = System.IO.File.ReadAllBytes(path);

            // Create RAD PDF control
            PdfWebControlLite pdfWebControl1 = new PdfWebControlLite(HttpContext);

            // Create document from PDF data
            pdfWebControl1.CreateDocument("Document Name", pdfData);

            // Put control in ViewBag
            ViewData["PdfWebControl1"] = pdfWebControl1;
        }
    }
}