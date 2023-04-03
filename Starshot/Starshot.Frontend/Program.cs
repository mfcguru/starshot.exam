using Starshot.Frontend;
using Starshot.Frontend.Services.Api;
using Starshot.Frontend.Services.Session;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<AppSettings>(options => builder.Configuration
        .GetSection("AppSettings")
        .Bind(options));
builder.Services.AddTransient<IApiService, ApiService>();
builder.Services.AddTransient<ISessionManager, SessionManager>();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
