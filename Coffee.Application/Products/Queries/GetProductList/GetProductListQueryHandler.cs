using AutoMapper;
using AutoMapper.QueryableExtensions;
using Coffee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Application.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _db;

        public GetProductListQueryHandler(IMapper mapper, IApplicationDbContext db) => (_mapper, _db) = (mapper, db);

        public async Task<ProductListViewModel> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            List<ProductListDto> productListDto = await _db.Products.ProjectTo<ProductListDto>(_mapper.ConfigurationProvider).OrderBy(p => p.Id).ToListAsync();

            return new ProductListViewModel { ProductList = productListDto };
        }
    }
}
