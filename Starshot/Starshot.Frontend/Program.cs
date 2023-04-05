using MassTransit;
using Starshot.Frontend;
using Starshot.Frontend.Services.Api;
using Starshot.Frontend.Services.Command;
using Starshot.Frontend.Services.Session;
using System.Configuration;

// add services to the container
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<AppSettings>(options => builder.Configuration
        .GetSection("AppSettings")
        .Bind(options));
builder.Services.AddTransient<IApiService, ApiService>();
builder.Services.AddTransient<ISessionManager, SessionManager>();
builder.Services.AddScoped<CommandServiceFactory>();
builder.Services.AddMassTransit(configuration =>
{
    configuration.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});
// configure pipeline
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}",
    defaults: new { controller = "Home", action = "Index" });

app.Run();
