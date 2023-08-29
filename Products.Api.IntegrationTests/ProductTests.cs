using Products.Api.Products;

namespace Application.IntegrationTests;

public class ProductTests : BaseIntegrationTest
{
    public ProductTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Create()
    {
        // Arrange
        var command = new CreateProduct.Command
        {
            Name = "Test",
            Category = "Test category",
            Price = 100.0m
        };

        // Act
        var productId = await Sender.Send(command);

        // Assert
        var product = DbContext.Products.FirstOrDefault(p => p.Id == productId);

        Assert.NotNull(product);
    }
}