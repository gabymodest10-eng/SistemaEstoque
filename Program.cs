
using FluentValidation;
using Microsoft.OpenApi.Models;
using SistemaEstoque.Exceptions;
using SistemaEstoque.Interfaces;
using SistemaEstoque.Middlewares;
using SistemaEstoque.Repositories;
using SistemaEstoque.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos Controllers
builder.Services.AddControllers();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Url", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Configuração do FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Injeção de dependência
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware de tratamento de exceções
app.UseMiddleware<ExceptionMiddlewares>();

// Configuração do Swagger
if (app.Environment.IsDevelopment())
    {
    app.UseSwagger();
    app.UseSwaggerUI();
    }

// Configuração do CORS
app.UseCors("Url");

// Redirecionamento HTTPS
app.UseHttpsRedirection();

// Autorização
app.UseAuthorization();

// Mapeamento dos Controllers
app.MapControllers();

app.Run();
