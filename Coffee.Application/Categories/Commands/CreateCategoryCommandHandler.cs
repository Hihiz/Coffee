using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private readonly IRepository<Category> _repository;

        public CreateCategoryCommandHandler(IRepository<Category> repository) => (_repository) = (repository);

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = new Category
            {
                Name = request.Name
            };

            try
            {
                _repository.Create(category);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при создании категории");
            }

            return category;
        }
    }
}

