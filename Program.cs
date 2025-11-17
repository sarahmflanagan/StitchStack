using Microsoft.EntityFrameworkCore;
using StitchStack.Data;
using StitchStack.Data.InMemory;
using StitchStack.Data.Repositories;
using StitchStack.Data.Services;
using StitchStack.Data.SqlDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddControllers();

// db
builder.Services.AddDbContext<InMemoryDBContext>(options =>
      options.UseInMemoryDatabase("InMemoryDBContext"));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SqlDBContext>(options => {
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// repositories
builder.Services.AddScoped<IPatternRepository, PatternRepository>();
builder.Services.AddScoped<IFabricRepository, FabricRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

// services
builder.Services.AddScoped<IProjectService, ProjectService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InMemoryDBContext>();
    await DbSeeder.SeedAsync(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StitchStack API");
        c.RoutePrefix = "api/docs";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map Razor Pages before the catch-all route
app.MapRazorPages();
app.MapControllers();

app.Run();
