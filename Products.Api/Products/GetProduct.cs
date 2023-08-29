using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Api.Entities;

namespace Products.Api.Products;

public class GetProduct
{
    public sealed record Query : IRequest<ProductResponse>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Query, ProductResponse>
    {
        private readonly ApplicationDbContext _dbContext;

        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product is null)
            {
                throw new ApplicationException("Product not found");
            }

            return product.Adapt<ProductResponse>();
        }
    }
}
