using Shortener.Domain.Modules;
using FluentValidation;
using System;

namespace Shortener.Services.Validations
{
    public class UrlValidation: AbstractValidator<Url>
    {
        public UrlValidation()
        {
            RuleFor(x => x.MainDestinationUrl)
                .NotEmpty()
                .WithMessage("The Main Destination Url field is mandatory.")
                .Length(7, 500)
                .WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters.");
        }
    }
}
