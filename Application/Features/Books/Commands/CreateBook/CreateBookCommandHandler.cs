using Application.Features.Books.Rules.BusinessRules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommandRequest, CreateBookCommandResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly BookBusinessRules _businessRules;

    public CreateBookCommandHandler(IBookRepository bookRepository, IMapper mapper, BookBusinessRules businessRules)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<CreateBookCommandResponse> Handle(CreateBookCommandRequest request, CancellationToken cancellationToken)
    {
        await _businessRules.CheckBookExists(request.Name);

        Book mappedBook = _mapper.Map<Book>(request);
        Book createdBook = await _bookRepository.AddAsync(mappedBook);
        var response = _mapper.Map<CreateBookCommandResponse>(createdBook);
        return response;

    }
}
