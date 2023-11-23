using API.InputModels;
using API.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IValidator<PostHeroiInputModel>, PostHeroiInputModelValidator>();
builder.Services.AddScoped<IValidator<PutHeroiInputModel>, PuttHeroiInputModelValidator>();

builder.Services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("dbHerois"));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = $"API Super Heróis ( {env} )",
        Description = "Heróis",
        Version = "v1",

        Contact = new OpenApiContact
        {
            Name = "Viceri TI",
            Email = "ti.todos@viceri.com.br"
        },

        License = new OpenApiLicense
        {
            Name = "Viceri TI"
        },
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


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
