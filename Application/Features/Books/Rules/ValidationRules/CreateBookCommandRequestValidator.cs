using Application.Features.Books.Commands.CreateBook;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Rules.ValidationRules;
public class CreateBookCommandRequestValidator : AbstractValidator<CreateBookCommandRequest>
{
    public CreateBookCommandRequestValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(2);
    }
}
