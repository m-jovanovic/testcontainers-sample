using MediatR;

namespace Products.Api.Products;

public class DeleteProduct
{
    public sealed record Command : IRequest
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Command>
    {
        private readonly ApplicationDbContext _dbContext;

        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == request.Id);

            if (product is null)
            {
                throw new ApplicationException("Product not found");
            }

            _dbContext.Remove(product);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
