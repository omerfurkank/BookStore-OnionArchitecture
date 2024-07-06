using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Queries.GetByIdAuthor;

public class GetByIdAuthorQueryHandler : IRequestHandler<GetByIdAuthorQueryRequest, GetByIdAuthorQueryResponse>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    public GetByIdAuthorQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }
    public async Task<GetByIdAuthorQueryResponse> Handle(GetByIdAuthorQueryRequest request, CancellationToken cancellationToken)
    {
        Author author = await _authorRepository.GetAsync(predicate: a => a.Id == request.Id);
        GetByIdAuthorQueryResponse response = _mapper.Map<GetByIdAuthorQueryResponse>(author);
        return response;
    }
}
