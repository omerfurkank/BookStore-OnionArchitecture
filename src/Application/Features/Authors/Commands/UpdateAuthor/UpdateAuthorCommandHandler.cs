using Application.Features.Auth.Rules.BusinessRules;
using Application.Features.Authors.Rules.BusinessRules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommandRequest, UpdateAuthorCommandResponse>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly AuthorBusinessRules _businessRules;

    public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper, AuthorBusinessRules businessRules)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<UpdateAuthorCommandResponse> Handle(UpdateAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        Author? author = await _authorRepository.GetAsync(predicate: b => b.Id == request.Id);
        author = _mapper.Map(request, author);
        if (request.ImageUrl != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await request.ImageUrl.CopyToAsync(memoryStream);
                author.ImageUrl = Convert.ToBase64String(memoryStream.ToArray());
            }
        }
        Author updatedAuthor = await _authorRepository.UpdateAsync(author);
        UpdateAuthorCommandResponse response = _mapper.Map<UpdateAuthorCommandResponse>(updatedAuthor);
        return response;
    }
}
