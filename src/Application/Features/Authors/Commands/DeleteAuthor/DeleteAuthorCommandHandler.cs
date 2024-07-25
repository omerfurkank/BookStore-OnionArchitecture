using Application.Features.Authors.Rules.BusinessRules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommandRequest, DeleteAuthorCommandResponse>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly AuthorBusinessRules _businessRules;

    public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper, AuthorBusinessRules businessRules)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<DeleteAuthorCommandResponse> Handle(DeleteAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        Author mappedAuthor = _mapper.Map<Author>(request);
        Author deletedAuthor = await _authorRepository.DeleteAsync(mappedAuthor);
        var response = _mapper.Map<DeleteAuthorCommandResponse>(deletedAuthor);
        return response;

    }
}
