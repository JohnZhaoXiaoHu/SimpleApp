﻿global using SimpleApp.Common.Configuration;
using System.Runtime.Loader;

var builder = WebApplication.CreateBuilder(args);

//使用 Autofac 替换默认 DI 容器
builder.Host.UseAutofacProviderFactory();

// Add services to the container.

builder.Services.AddControllers();
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
