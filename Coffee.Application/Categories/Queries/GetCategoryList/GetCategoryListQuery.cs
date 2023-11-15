using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQuery : IRequest<List<Category>>
    {
        public int Id { get; set; }
    }
}
