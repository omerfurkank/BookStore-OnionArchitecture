using Application.Repositories;
using Application.Services.SignalR.HubServices;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommandRequest, CreateAuthorCommandResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorHubService _hubService;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper, IAuthorHubService authorHubService)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _hubService = authorHubService;
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

            await _hubService.SendMessage("added");

            return response;

        }
    }
}
