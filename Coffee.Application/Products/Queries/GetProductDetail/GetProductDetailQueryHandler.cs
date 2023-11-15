using AutoMapper;
using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Products.Queries.GetProductDetail
{
    public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProductDetailDto>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public GetProductDetailQueryHandler(IRepository<Product> repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

        public async Task<ProductDetailDto> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            Product product = await _repository.GetById(request.Id);

            if (product == null)
            {
                throw new Exception("Продукт не найден");
            }

            return _mapper.Map<ProductDetailDto>(product);
        }
    }
}
