using BallonDor.Data;
using BallonDor.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Register services
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IAwardService, AwardService>();
builder.Services.AddScoped<IVoterService, VoterService>();

// API controllers
builder.Services.AddControllers();

// Swagger 
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure  HTTP request .
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    // Swagger in development
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//  authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// unauthenticated users to the login page
app.Use(async (context, next) =>
{
    var isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;
    var isLoginPage = context.Request.Path.StartsWithSegments("/Identity/Account/Login");
    var isRegisterPage = context.Request.Path.StartsWithSegments("/Identity/Account/Register");

    if (!isAuthenticated && !isLoginPage && !isRegisterPage)
    {
        context.Response.Redirect("/Identity/Account/Login");
        return;
    }

    await next();
});

// API controllers
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();