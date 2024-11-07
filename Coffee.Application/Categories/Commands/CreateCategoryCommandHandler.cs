using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private readonly IBaseRepository<Category> _repository;

        public CreateCategoryCommandHandler(IBaseRepository<Category> repository) => (_repository) = (repository);

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = new Category
            {
                Name = request.Name
            };

            try
            {
                category = await _repository.CreateAsync(category);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }

            return category;
        }
    }
}

