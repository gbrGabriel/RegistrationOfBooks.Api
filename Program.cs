using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
