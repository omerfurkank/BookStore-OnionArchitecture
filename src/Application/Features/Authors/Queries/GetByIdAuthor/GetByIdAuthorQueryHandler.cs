using Application.Features.Authors.Rules.BusinessRules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Queries.GetByIdAuthor;

public class GetByIdAuthorQueryHandler : IRequestHandler<GetByIdAuthorQueryRequest, GetByIdAuthorQueryResponse>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly AuthorBusinessRules _businessRules;
    public GetByIdAuthorQueryHandler(IAuthorRepository authorRepository, IMapper mapper, AuthorBusinessRules businessRules)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }
    public async Task<GetByIdAuthorQueryResponse> Handle(GetByIdAuthorQueryRequest request, CancellationToken cancellationToken)
    {
        await _businessRules.CheckAuthorIsNull(request.Id);
        Author? author = await _authorRepository.GetAsync(predicate: a => a.Id == request.Id);
        GetByIdAuthorQueryResponse response = _mapper.Map<GetByIdAuthorQueryResponse>(author);
        return response;
    }
}
