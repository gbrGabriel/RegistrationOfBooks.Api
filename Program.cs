using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RegistrationOfBooks.Api.Persistence;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    Serilog.Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.MSSqlServer(connectionString, "Logs", autoCreateSqlTable: true)
        .WriteTo.Console()
        .CreateLogger();
}).UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<RegistrationBooksDbContext>();
builder.Services.AddDbContext<RegistrationBooksDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api para cadastro de livros",
        Version = "v1",
        Description = "Cadastrar livros, realizar busca e atualização de livros cadastrados.",
        Contact = new OpenApiContact
        {
            Name = "Gabriel Silva",
            Email = "gabrielgbr.contato@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/gbrgabriel/")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    opt.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
