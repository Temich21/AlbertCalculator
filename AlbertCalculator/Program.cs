using Microsoft.EntityFrameworkCore;
using AlbertCalculator.Service;
using AlbertCalculator.Repositories;
using AlbertCalculator.Data;
using AlbertCalculator.Controllers;
using AlbertCalculator.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add controlers, services and repositories to the container.
builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<PurchaseController>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<PurchaseService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

// Add controllers to the dependency container
builder.Services.AddControllers();

// Add authorization and authentication services
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add Middleware CORS before UseAuthentication and UseAuthorization
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

// Add middleware
app.UseMiddleware<AuthCheckMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();