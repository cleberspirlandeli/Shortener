using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Services.Validations
{
    public class KeyUrlValidation : AbstractValidator<string>
    {
        public KeyUrlValidation()
        {
            RuleFor(x => x.ToString())
                .NotEmpty()
                .WithMessage("The Key Url field is mandatory.")
                .Length(7, 10)
                .WithMessage("The Key Url field must be characters.");
        }
    }
}
