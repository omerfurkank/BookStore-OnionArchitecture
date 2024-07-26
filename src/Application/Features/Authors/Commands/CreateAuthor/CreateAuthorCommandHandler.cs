using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Rules.BusinessRules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommandRequest, CreateAuthorCommandResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<CreateAuthorCommandResponse> Handle(CreateAuthorCommandRequest request, CancellationToken cancellationToken)
        {
            Author mappedAuthor = _mapper.Map<Author>(request);
            if (request.ImageUrl != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.ImageUrl.CopyToAsync(memoryStream);
                    mappedAuthor.ImageUrl = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            Author createdAuthor = await _authorRepository.AddAsync(mappedAuthor);
            var response = _mapper.Map<CreateAuthorCommandResponse>(createdAuthor);
            return response;

        }
    }
}
