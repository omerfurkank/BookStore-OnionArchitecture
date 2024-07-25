using Application.Exceptions.CustomExceptions;
using Application.Features.Books.Constants;
using Application.Repositories;
using Domain.Entities;

namespace Application.Features.Books.Rules.BusinessRules;

public class BookBusinessRules
{
    private readonly IBookRepository _bookRepository;
    public BookBusinessRules(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task CheckBookExists(string name)
    {
        Book? result = await _bookRepository.GetAsync(predicate: b => b.Name.ToLower() == name.ToLower());

        if (result is not null) { throw new BusinessException(BookMessages.BookNameExists); }
    }
}
