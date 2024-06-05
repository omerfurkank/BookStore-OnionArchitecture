using Application.Features.Books.Queries.GetByIdBook;
using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.Queries.GetListBook;

public class GetListBookQueryHandler : IRequestHandler<GetListBookQueryRequest, IList<GetByIdBookQueryResponse>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetListBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IList<GetByIdBookQueryResponse>> Handle(GetListBookQueryRequest request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetListAsync(include: b => b.Include(b => b.Author), index: request.Index, size: request.Size);
        IList<GetByIdBookQueryResponse> response = _mapper.Map<IList<GetByIdBookQueryResponse>>(books);
        throw new Exception("dsf");//return response;
    }
}