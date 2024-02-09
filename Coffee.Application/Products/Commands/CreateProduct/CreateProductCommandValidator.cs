using FluentValidation;

namespace Coffee.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название продукта не может быть пустым")
                .MinimumLength(3).WithMessage("Название продукта слишком короткое");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Категория продукта не может быть пустым")
                .GreaterThan(0).WithMessage("Выберите категорию");

        }
    }
}
