using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using DesafioERP.API.Models;
using SistemaTarefas.Data;
using DesafioERP.Repositorios.Interfaces;
using DesafioERP.Repositorios;
using DesafioERP.API.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ 1️⃣ Configurar CORS ANTES do `builder.Build()`
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Troque pela URL do seu React
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});

// ✅ 2️⃣ Configurar o banco de dados e serviços
builder.Services.AddDbContext<ERPDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ 3️⃣ Aplicar CORS ANTES de mapear os controladores
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/", () => "API rodando com sucesso! 🚀");

app.Run();
