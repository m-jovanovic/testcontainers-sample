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
    var productId = await sender.Send(request.Adapt<CreateProduct.Command>());

    return Results.Ok(productId);
});

app.MapGet("api/products/{productId}", async (Guid productId, ISender sender) =>
{
    var productResponse = await sender.Send(new GetProduct.Query { Id = productId });

    return Results.Ok(productResponse);
});

app.MapPut("api/products/{productId}", async (Guid productId, UpdateProductRequest request, ISender sender) =>
{
    await sender.Send(request.Adapt<UpdateProduct.Command>() with
    {
        Id = productId
    });

    return Results.NoContent();
});

app.MapDelete("api/products/{productId}", async (Guid productId, ISender sender) =>
{
    await sender.Send(new DeleteProduct.Command{ Id = productId });

    return Results.NoContent();
});

app.UseHttpsRedirection();

app.Run();

public partial class Program { }
