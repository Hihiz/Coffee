using FluentValidation;

namespace Coffee.Application.Categories.Commands
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Укажите название")
                .MinimumLength(3).WithMessage("Название категории должно быть не менее 3 символов");
        }
    }
}
