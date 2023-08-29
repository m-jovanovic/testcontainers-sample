using Products.Api.Products;

namespace Application.IntegrationTests;

public class ProductTests : BaseIntegrationTest
{
    public ProductTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldCreateProduct()
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

    [Fact]
    public async Task Get_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var productId = await CreateProduct();
        var query = new GetProduct.Query { Id = productId };

        // Act
        var productResponse = await Sender.Send(query);

        // Assert
        Assert.NotNull(productResponse);
    }

    [Fact]
    public async Task Get_ShouldThrow_WhenProductIsNull()
    {
        // Arrange
        var query = new GetProduct.Query { Id = Guid.NewGuid() };

        // Act
        Task Action() => Sender.Send(query);

        // Assert
        await Assert.ThrowsAsync<ApplicationException>(Action);
    }

    [Fact]
    public async Task Update_ShouldUpdateProduct_WhenProductExists()
    {
        // Arrange
        var productId = await CreateProduct();
        var command = new UpdateProduct.Command
        {
            Id = productId,
            Name = "Test",
            Category = "Test category",
            Price = 100.0m
        };

        // Act
        await Sender.Send(command);

        // Assert
    }

    [Fact]
    public async Task Update_ShouldThrow_WhenProductIsNull()
    {
        // Arrange
        var command = new UpdateProduct.Command
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Category = "Test category",
            Price = 100.0m
        };

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<ApplicationException>(Action);
    }

    [Fact]
    public async Task Delete_ShouldDeleteProduct_WhenProductExists()
    {
        // Arrange
        var productId = await CreateProduct();
        var command = new DeleteProduct.Command { Id = productId };

        // Act
        await Sender.Send(command);

        // Assert
        var product = DbContext.Products.FirstOrDefault(p => p.Id == productId);

        Assert.Null(product);
    }

    [Fact]
    public async Task Delete_ShouldThrow_WhenProductIsNull()
    {
        // Arrange
        var command = new DeleteProduct.Command { Id = Guid.NewGuid() };

        // Act
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<ApplicationException>(Action);
    }

    private async Task<Guid> CreateProduct()
    {
        var command = new CreateProduct.Command
        {
            Name = "Test",
            Category = "Test category",
            Price = 100.0m
        };

        var productId = await Sender.Send(command);

        return productId;
    }
}