using Microsoft.EntityFrameworkCore;
// brings EF Core extension methods and types into scope (e.g. UseSqlite, DbContext, DbSet, migrations helpers)

using MyMvcApp.Data;
// brings your app's data types into scope — AppDbContext and SeedData are in this namespace

var builder = WebApplication.CreateBuilder(args);
// creates a WebApplicationBuilder:
// - sets up Host, Configuration (from appsettings, env vars, command line), Logging, and Services (DI container).
// - `args` are the command-line args passed into the process.

builder.Services.AddControllersWithViews();
// registers MVC services in the DI container: controller activation, model binding, view rendering (Razor views).
// This enables traditional MVC (controllers + views), not just minimal APIs.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));
// Registers your EF Core DbContext (AppDbContext) with the DI container.
// - AddDbContext registers AppDbContext as a *scoped* service by default (new instance per request).
// - The lambda configures EF Core to use the SQLite provider with the connection string "Data Source=app.db" (a local file).
// - Note: UseSqlite is an EF Core extension method provided by Microsoft.EntityFrameworkCore.Sqlite package.

var app = builder.Build();
// builds the WebApplication (finalizes DI, configuration, and prepares the middleware pipeline).
// After this you configure middleware and routing on `app`.

app.UseStaticFiles();
// adds middleware that serves static files from the wwwroot folder (css, js, images, etc).
// If a request matches a static file, it is served directly (before MVC/Controllers handle it).

app.UseRouting();
// adds routing middleware that matches incoming requests to endpoints (controllers, Razor Pages, etc).
// This must appear before endpoint mapping (Map... calls) and usually before auth/authorization.

app.MapDefaultControllerRoute();
// maps the conventional default controller route: "{controller=Home}/{action=Index}/{id?}"
// This wires up controller endpoints so requests are dispatched to controller actions.


// 🔹 Seed data here
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Initialize(db);
}
// This block runs once at startup to seed initial data.
// - app.Services.CreateScope() creates an IServiceScope so you can resolve *scoped* services (like DbContext) at startup.
// - scope.ServiceProvider.GetRequiredService<AppDbContext>() resolves an AppDbContext instance from DI.
// - SeedData.Initialize(db) is your custom method that typically:
//     - ensures the database exists (e.g., db.Database.EnsureCreated() or db.Database.Migrate()),
//     - checks whether seed rows already exist, and inserts initial records if needed,
//     - calls db.SaveChanges().
// - The using scope ensures the scope (and DbContext) is disposed when done.

app.Run();
// starts the web server (Kestrel by default) and blocks the calling thread until shutdown.
// At this point your app is running and serving requests.
