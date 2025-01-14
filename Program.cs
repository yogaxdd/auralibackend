using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan layanan default (opsional, bisa ditambah sesuai kebutuhan)
builder.Services.AddControllers();

var app = builder.Build();

// Middleware: Handle HTTP requests
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Route default untuk cek server
app.MapGet("/", () => "Spotify Server is running!");

// Route untuk menerima Spotify callback
app.MapGet("/callback", async (HttpContext ctx) =>
{
    var query = ctx.Request.Query["code"];
    if (!string.IsNullOrEmpty(query))
    {
        return Results.Ok($"Authorization code received: {query}");
    }
    return Results.BadRequest("No authorization code received!");
});

// Jalankan server
app.Run();
