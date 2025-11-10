using Microsoft.EntityFrameworkCore;
using StitchStack.Data.InMemory;
using StitchStack.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// db
builder.Services.AddDbContext<InMemoryDBContext>(options =>
      options.UseInMemoryDatabase("InMemoryDBContext"));

// repositories
builder.Services.AddScoped<IPatternRepository, PatternRepository>();
builder.Services.AddScoped<IFabricRepository, FabricRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
