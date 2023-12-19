﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RadPdf;

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

// Create middleware settings
RadPdfCoreMiddlewareSettings settings = new RadPdfCoreMiddlewareSettings()
{
    // Add SQL Server Connection String, if not using Lite Documents
    // Sample connection string below connects to a SQL Server Express instance on localhost
    // Encrypt=False is set to avoid trust exception (e.g. "The certificate chain was issued by an authority that is not trusted.")
    ConnectionString = @"Server=.\SQLExpress;Database=RadPdf;Encrypt=False;Trusted_Connection=Yes;",

    // Add License Key
    LicenseKey = "DEMO"

    // To run RAD PDF without the System Service, add UseService = false
    // If using Lite Documents without the System Service, a LiteStorageProvider must also be implemented
};

// Add RAD PDF's middleware to app
app.UseRadPdf(settings);

app.Run();