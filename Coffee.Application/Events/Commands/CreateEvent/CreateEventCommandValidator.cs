using FluentValidation;

namespace Coffee.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название новости не должно быть пустым")
                .MinimumLength(3).WithMessage("Название новости должно быть не менее 3 символов");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Описание новости не должно быть пустым")
                .MinimumLength(5).WithMessage("Описание новости должно быть не менее 5 символов");
        }
    }
}
