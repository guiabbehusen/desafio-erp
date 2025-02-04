using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using DesafioERP.API.Models;
using SistemaTarefas.Data;
using Microsoft.Extensions.Options;
using DesafioERP.Repositorios.Interfaces;
using DesafioERP.Repositorios; // Certifique-se de usar o namespace correto

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ERPDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapGet("/", () => "API rodando com sucesso! 🚀");


app.Run();
