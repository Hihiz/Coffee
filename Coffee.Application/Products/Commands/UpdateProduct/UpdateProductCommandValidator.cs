using FluentValidation;

namespace Coffee.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                   .NotEmpty().WithMessage("Название продукта не может быть пустым")
                   .MinimumLength(3).WithMessage("Название продукта слишком короткое");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Укажите цену")
                .GreaterThan(0).WithMessage("Цена должна быть больше нуля");

            RuleFor(x => x.Description)
                .MinimumLength(5).WithMessage("Описание должно быть не меннее 5 символов");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Категория продукта не может быть пустым")
                .GreaterThan(0).WithMessage("Выберите категорию");
        }
    }
}
