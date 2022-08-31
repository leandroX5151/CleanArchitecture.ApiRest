using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandValidator: AbstractValidator<CreateStreamerCommand>
    {
        public CreateStreamerCommandValidator()
        {
            RuleFor(static p => p.Nombre)
                .NotEmpty().WithMessage("{Nombre} no puede estar en blanco")
                .NotNull().WithMessage("{Nombre} no puede estar en blanco")
                .MaximumLength(50).WithMessage("{Nombre} no puede superar los 50 caracteres");

            RuleFor(static p => p.Url)
                .NotEmpty().WithMessage("{Url} no puede estar en blanco");
        }
    }
}
