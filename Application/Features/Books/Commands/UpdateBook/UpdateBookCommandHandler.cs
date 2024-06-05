using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Rules.BusinessRules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommandRequest, UpdateBookCommandResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly BookBusinessRules _businessRules;

    public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper, BookBusinessRules businessRules)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<UpdateBookCommandResponse> Handle(UpdateBookCommandRequest request, CancellationToken cancellationToken)
    {
        Book mappedBook = _mapper.Map<Book>(request);
        Book updatedBook = await _bookRepository.UpdateAsync(mappedBook);
        var response = _mapper.Map<UpdateBookCommandResponse>(updatedBook);
        return response;
    }
}