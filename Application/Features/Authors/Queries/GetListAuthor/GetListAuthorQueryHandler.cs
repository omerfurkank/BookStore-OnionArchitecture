using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Queries.GetListAuthor;

public class GetListAuthorQueryHandler : IRequestHandler<GetListAuthorQueryRequest,IList<GetListAuthorQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;

    public GetListAuthorQueryHandler(IMapper mapper, IAuthorRepository authorRepository)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
    }

    public async Task<IList<GetListAuthorQueryResponse>> Handle(GetListAuthorQueryRequest request, CancellationToken cancellationToken)
    {
        IList<Author> authors = await _authorRepository.GetListAsync(index: request.Index, size: request.Size);
        IList<GetListAuthorQueryResponse> response = _mapper.Map<IList<GetListAuthorQueryResponse>>(authors);
        return response;
    }
}

