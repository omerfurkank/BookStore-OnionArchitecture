using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Commands.CreateRange;

public class CreateRangeAuthorCommandHandler : IRequestHandler<CreateRangeAuthorCommandRequest, CreateRangeAuthorCommandResponse>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public CreateRangeAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<CreateRangeAuthorCommandResponse> Handle(CreateRangeAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        List<Author> authors = _mapper.Map<List<Author>>(request.Authors);
        var addedAuthors = _authorRepository.AddRangeAsync(authors);
        var response = new CreateRangeAuthorCommandResponse();
        return response;

    }
}
