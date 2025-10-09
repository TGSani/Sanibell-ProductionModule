using Sanibell_ProductionModule.Services.Interfaces;
using Sanibell_ProductionModule.Services;
using Microsoft.Extensions.Options;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Sanibell_ProductionModule.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// DI for user service
builder.Services.AddScoped<IUsersRepository, MockUserRepository>(); //  switch between "MockUserRepository" and "OdbcUserRepository" here
builder.Services.AddScoped<IMenuTileService, MenuTileService>();
builder.Services.AddScoped<IOrderRepository, MockOrderRepository>(); // switch between "MockOrderRepository" and "OdbcOrderRepository" here


builder.Services.AddAuthorization();

// Cookie authentication
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Index";
        options.AccessDeniedPath = "/Index";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
