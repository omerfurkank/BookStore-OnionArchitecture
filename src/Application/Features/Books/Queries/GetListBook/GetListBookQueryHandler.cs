using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.Queries.GetListBook;

public class GetListBookQueryHandler : IRequestHandler<GetListBookQueryRequest, IList<GetListBookQueryResponse>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetListBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IList<GetListBookQueryResponse>> Handle(GetListBookQueryRequest request, CancellationToken cancellationToken)
    {
        IList<Book> books = await _bookRepository.GetListAsync(include: b => b.Include(b => b.Author), index: request.Index, size: request.Size);
        IList<GetListBookQueryResponse> response = _mapper.Map<IList<GetListBookQueryResponse>>(books);
        /*throw new Exception("dsf");*/
        return response;
    }
}