using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.RateLimiting;
using WebApplication.Configuration;
using WebApplication.Data;
using WebApplication.Data.Data;
using WebApplication.Data.Interfaces;
using WebApplication.Data.Models;
using WebApplication.Data.Repositories;
using WebApplication.Authorization;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);


// Configure secure configuration sources
builder.Configuration.Sources.Clear();

builder.Configuration
    .AddJsonFile("sharedsettings.json", optional: true, reloadOnChange: true);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Configuration
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Configuration.AddEnvironmentVariables();

// Bind configuration sections to strongly typed classes
var webAppConfiguration = builder.Configuration.Get<WebAppConfiguration>();

if (webAppConfiguration == null)
{
    throw new InvalidOperationException("WebAppConfiguration section is missing or empty.");
}

builder.Services.AddSingleton(webAppConfiguration);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("WebApplication")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IWebAppRepository, WebAppRepository>();

// Configure Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.OnRejected = (context, _) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        return new ValueTask();
    };

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        if (httpContext.User.Identity?.IsAuthenticated == true)
        {
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return RateLimitPartition.GetFixedWindowLimiter(userId, _ =>
                new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 1000,
                    Window = TimeSpan.FromMinutes(1),
                    AutoReplenishment = true // maybe set explicitly
                });
        }
        else
        {
            var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown_ip";
            return RateLimitPartition.GetFixedWindowLimiter(ip, _ =>
                new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 100,
                    Window = TimeSpan.FromMinutes(1),
                    AutoReplenishment = true
                });
        }
    });
});


// Configure Authorization
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("ArchiveAccessPolicy", policy =>
    {
        policy.RequireClaim("IsVerifiedClient", "true");
    });

    options.AddPolicy("CanManageMaterial", policy =>
    {
        policy.AddRequirements(new IsAuthorRequirement());
    });

    options.AddPolicy("PremiumAccess", policy =>
        policy.AddRequirements(new MinimumWorkingHoursRequirement(100)));

    options.AddPolicy("ForumAccess", policy =>
        policy.AddRequirements(new ForumAccessRequirement()));
});

// Register the authorization handler
builder.Services.AddScoped<IAuthorizationHandler, IsAuthorHandler>();

// Register the MinimumWorkingHoursHandler
builder.Services.AddScoped<IAuthorizationHandler, MinimumWorkingHoursHandler>();

// Register the ForumAccessHandler
builder.Services.AddScoped<IAuthorizationHandler, ForumAccessHandler>();

// Configure Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// define the list of supported cultures
var supportedCultures = new[] { "en-US", "uk-UA", "fr-FR" };

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture("en-US");

    options.AddSupportedCultures(supportedCultures);

    options.AddSupportedUICultures(supportedCultures);
});

//set up MVC with localization
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
