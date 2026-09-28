using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MyShop.DAL;
using Serilog;
using Serilog.Events;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

// services (ASP.NET components) for handling controllers and views to dependency injection container 
// sets up the MVC pattern for handling HTTP requests
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ItemDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ItemDbContextConnection"]
    );
});

// DAL
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// logger
builder.Services.AddSerilog((services, loggerConfiguration) =>
{
    loggerConfiguration
    .MinimumLevel.Information()
    //.WriteTo.Console()
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log")
    // Filtering out info-level EF db execution logs
    .Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) && 
                e.Level == LogEventLevel.Information && 
                e.MessageTemplate.Text.Contains("Executed DbCommend"));
});

/* Adjusting lifetime of services
builder.Services.AddScoped<IService,Service>();
builder.Services.AddTransient<IService,Service>();
builder.Services.AddSingleton<IService,Service>();
*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // dev env error page for unhandled exceptions - provides detailed info for debugging
    app.UseDeveloperExceptionPage();
    DBInit.Seed(app);
}

// middleware to allow using files in the wwwwroot-folder
app.UseStaticFiles();

// map default controller route - standard URL pattern (also adds middleware - handles routing of incoming requests to appropriate controller)
app.MapDefaultControllerRoute();

// adds middleware, final pipeline step - handles requests and generates response
app.Run();
