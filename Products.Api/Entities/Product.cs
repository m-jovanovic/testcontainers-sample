namespace Products.Api.Entities;

public sealed class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public DateTime CreatedOnUtc { get; set; }
}
