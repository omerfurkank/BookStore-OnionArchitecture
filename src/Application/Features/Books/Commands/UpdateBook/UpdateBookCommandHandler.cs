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
        Book? book = await _bookRepository.GetAsync(predicate: b => b.Id == request.Id);
        book = _mapper.Map(request, book);
        if (request.ImageUrl != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await request.ImageUrl.CopyToAsync(memoryStream);
                book.ImageUrl = Convert.ToBase64String(memoryStream.ToArray());
            }
        }
        Book updatedBook = await _bookRepository.UpdateAsync(book);
        var response = _mapper.Map<UpdateBookCommandResponse>(updatedBook);
        return response;
    }
}