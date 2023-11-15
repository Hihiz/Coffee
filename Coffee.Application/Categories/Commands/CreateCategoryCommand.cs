using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<Category>
    {
        public string Name { get; set; }
    }
}
