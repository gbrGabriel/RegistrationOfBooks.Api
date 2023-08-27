using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RegistrationOfBooks.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<RegistrationBooksDbContext>();
builder.Services.AddDbContext<RegistrationBooksDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api para cadastro de livros",
        Version = "v1",
        Description = "Cadastrar livros, realizar busca e atualiza��o de livros cadastrados.",
        Contact = new OpenApiContact
        {
            Name = "Gabriel Silva",
            Email = "gabrielgbr.contato@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/gbrgabriel/")
        }
    });

    var xmlFile = "RegistrationOfBooks.xml";
    var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFile);

    opt.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
