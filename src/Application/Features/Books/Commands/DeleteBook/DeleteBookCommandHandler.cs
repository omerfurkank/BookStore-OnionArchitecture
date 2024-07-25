using Application.Features.Books.Commands.UpdateBook;
using Application.Features.Books.Rules.BusinessRules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommandRequest, DeleteBookCommandResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly BookBusinessRules _businessRules;

    public DeleteBookCommandHandler(IBookRepository bookRepository, IMapper mapper, BookBusinessRules businessRules)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<DeleteBookCommandResponse> Handle(DeleteBookCommandRequest request, CancellationToken cancellationToken)
    {
        Book mappedBook = _mapper.Map<Book>(request);
        Book deletedBook = await _bookRepository.DeleteAsync(mappedBook);
        var response = _mapper.Map<DeleteBookCommandResponse>(deletedBook);
        return response;
    }
}
