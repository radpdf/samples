using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RadPdf.Web.UI;

namespace CS_React.Server.Controllers
{
    [ApiController]
    [Route("radpdf")]
    public class RadPdfController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<RadPdfController> _logger;

        public RadPdfController(IWebHostEnvironment environment, ILogger<RadPdfController> logger)
        {
            _hostEnvironment = environment;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public string Get(string id)
        {
            // Get PDF from drive (we could use the id to specify exactly which PDF we should load)
            string path = Path.Combine(_hostEnvironment.ContentRootPath, "PDFs", "RadPdfSampleForm.pdf");
            byte[] pdf = System.IO.File.ReadAllBytes(path);

            // Load PDF into RAD PDF
            PdfWebControlLite radpdf = new PdfWebControlLite(HttpContext);
            radpdf.CreateDocument("SamplePdf", pdf);
            radpdf.Height = "600px";
            radpdf.Width = "100%";

            return PdfWebControl.RenderHead() + radpdf.RenderControl();
        }
    }
}
