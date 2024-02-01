using RadPdf;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDistributedMemoryCache();

// Use session cookies for RAD PDF session storage (other options can be used)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

// Use session cookies for RAD PDF session storage (other options can be used)
app.UseSession();

// Create middleware settings
RadPdfCoreMiddlewareSettings settings = new RadPdfCoreMiddlewareSettings()
{
    // Add SQL Server Connection String, if not using Lite Documents
    // Sample connection string below connects to a SQL Server Express instance on localhost
    // TrustServerCertificate=True is set to avoid a trust exception (e.g. "The certificate chain was issued by an authority that is not trusted.")
    ConnectionString = @"Server=.\SQLExpress;Database=RadPdf;Trusted_Connection=Yes;TrustServerCertificate=True;",

    // Add License Key
    LicenseKey = "DEMO",

    // In this sample, we are using the System Service
    UseService = true
};

// Add RAD PDF's middleware to app
// Remember to also update the client's vite.config.js to forward all /RadPdf.axd and /radpdf requests here
app.UseRadPdf(settings);

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
