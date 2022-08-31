using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public  class UpdateStreamerCommandValidator: AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            RuleFor(static p => p.Name)
                .NotNull().WithMessage("{Nombre} no s epermiten nulos");
            RuleFor(static p => p.Url)
                .NotNull().WithMessage("{Url} no s epermiten nulos");
        }
    }
}
