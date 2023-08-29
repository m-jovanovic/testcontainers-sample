using MediatR;
using Products.Api.Entities;

namespace Products.Api.Products;

public class CreateProduct
{
    public sealed class Command : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Command, Guid>
    {
        private readonly ApplicationDbContext _dbContext;

        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
                CreatedOnUtc = DateTime.UtcNow
            };

            _dbContext.Add(product);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
