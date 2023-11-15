using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<Category>>
    {
        private readonly IRepository<Category> _repository;

        public GetCategoryListQueryHandler(IRepository<Category> repository) => (_repository) = (repository);

        public async Task<List<Category>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            List<Category> categoryList = await _repository.GetAll();

            return categoryList;
        }
    }
}
