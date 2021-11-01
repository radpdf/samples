using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RadPdf;

using RadPdfDemoNoService.CustomProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

// Create middleware settings (add UseService = false to run RAD PDF without the System Service)
RadPdfCoreMiddlewareSettings settings = new RadPdfCoreMiddlewareSettings() { IntegrationProvider = new CustomIntegrationProvider(), LicenseKey = "DEMO", UseService = false };

// Add RAD PDF's middleware to app
app.UseRadPdf(settings);

app.Run();