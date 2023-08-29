namespace Products.Api.Contracts;

public class UpdateProductRequest
{
    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public decimal Price { get; set; }
}
