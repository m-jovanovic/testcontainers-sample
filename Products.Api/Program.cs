using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Api;
using Products.Api.Contracts;
using Products.Api.Products;
using Web.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.MapPost("api/products", async (CreateProductRequest request, ISender sender) =>
{
    await sender.Send(request.Adapt<CreateProduct>());

    return Results.Ok();
});

app.UseHttpsRedirection();

app.Run();

public partial class Program { }
