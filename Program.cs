using Sanibell_ProductionModule.Services;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Sanibell_ProductionModule.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// DI for user service
builder.Services.AddHttpClient();
builder.Services.AddScoped<IUsersRepository, MockUserRepository>(); //  switch between "MockUserRepository" and "OdbcUserRepository" here
builder.Services.AddScoped<MenuTileService>();
builder.Services.AddScoped<PlannerErpService>();
builder.Services.AddScoped<IOrderRepository, OdbcOrderRepository>(); // switch between "MockOrderRepository" and "OdbcOrderRepository" here
builder.Services.AddScoped<IProductionRepository, MockProductionRepository>(); // switch between "MockProductionRepository" and "OdbcProductionRepository" here
builder.Services.AddScoped<IPlannerRepository, MockPlannerRepository>(); // switch between "MockPlannerRepository" and "OdbcPlannerRepository" here



// policy based authorization
builder.Services.AddAuthorization( options =>
{
    options.AddPolicy("RequireProductionRole",
         policy => policy.RequireRole("ProductieMedewerker", "Planner", "Administrator"));
    options.AddPolicy("RequirePlannerRole",
         policy => policy.RequireRole("Planner", "Administrator"));
     options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole("Administrator"));
});

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
