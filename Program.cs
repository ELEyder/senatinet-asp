using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using senatinet_asp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<FirebaseService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Duración de la sesión
    options.Cookie.HttpOnly = true; // Asegura que las cookies solo sean accesibles a través de HTTP(S)
    options.Cookie.IsEssential = true; // Asegura que la cookie no sea eliminada por consentimiento de cookies de GDPR
});

// Initialize FirebaseApp
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("Config/fs_credencials.json")
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
