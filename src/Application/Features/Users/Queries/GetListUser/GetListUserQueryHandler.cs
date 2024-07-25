using Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Queries.GetListUser;

public class GetListUserQueryHandler : IRequestHandler<GetListUserQueryRequest, IList<GetListUserQueryResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetListUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IList<GetListUserQueryResponse>> Handle(GetListUserQueryRequest request, CancellationToken cancellationToken)
    {
        List<GetListUserQueryResponse> response = new();

        foreach (var user in await _userRepository.GetAllUserAsync())
        {
            var roles = await _userRepository.GetUserRolesAsync(user);
            response.Add(new GetListUserQueryResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Roles = roles
            });
        }
        return response;
    }
}
