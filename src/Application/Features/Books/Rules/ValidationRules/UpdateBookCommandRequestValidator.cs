using Application.Features.Books.Commands.UpdateBook;
using FluentValidation;

namespace Application.Features.Books.Rules.ValidationRules;

public class UpdateBookCommandRequestValidator : AbstractValidator<UpdateBookCommandRequest>
{
    public UpdateBookCommandRequestValidator()
    {
        RuleFor(b => b.Name).NotEmpty().MinimumLength(2);
    }
}
