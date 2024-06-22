using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.Queries.GetByIdBook;

public class GetByIdBookQueryHandler : IRequestHandler<GetByIdBookQueryRequest, GetByIdBookQueryResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    public GetByIdBookQueryHandler(IBookRepository repository,IMapper mapper)
    {
        _bookRepository = repository;
        _mapper = mapper;
    }
    public async Task<GetByIdBookQueryResponse> Handle(GetByIdBookQueryRequest request, CancellationToken cancellationToken)
    {
        //if (request.Id<10)
        //{
        //    throw new Exception("x");
        //}
        var book = await _bookRepository.GetAsync(predicate: b => b.Id == request.Id, include: b => b.Include(p => p.Author));
        GetByIdBookQueryResponse response = _mapper.Map<GetByIdBookQueryResponse>(book);
        return response;
    }
}
