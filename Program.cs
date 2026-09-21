var builder = WebApplication.CreateBuilder(args);

// services (ASP.NET components) for handling controllers and views to dependency injection container 
// sets up the MVC pattern for handling HTTP requests
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // dev env error page for unhandled exceptions - provides detailed info for debugging
    app.UseDeveloperExceptionPage();
}

// map default controller route - standard URL pattern (also adds middleware - handles routing of incoming requests to appropriate controller)
app.MapDefaultControllerRoute();

// adds middleware, final pipeline step - handles requests and generates response
app.Run();
