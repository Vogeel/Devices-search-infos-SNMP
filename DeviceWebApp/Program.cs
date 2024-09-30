var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configure HttpClient with a base address
builder.Services.AddHttpClient<DeviceService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7055/"); // Set the base address for the API
});

builder.Services.AddScoped<DeviceService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
