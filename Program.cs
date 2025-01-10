using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Route untuk menerima Spotify callback
app.MapGet("/callback", (HttpContext ctx) =>
{
    var query = ctx.Request.Query["code"];
    if (!string.IsNullOrEmpty(query))
    {
        // Kode otorisasi diterima dengan benar
        return $"Authorization code received: {query}. You can close this tab.";
    }
    else
    {
        // Tidak ada kode otorisasi yang diterima, untuk debugging
        return "No authorization code received. Check your redirect URI or server setup.";
    }
});


// Route default untuk cek server
app.MapGet("/", () => "Spotify Server is running!");

app.Run();
