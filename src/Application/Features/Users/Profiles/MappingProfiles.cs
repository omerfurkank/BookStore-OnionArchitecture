using Application.Features.Users.Commands.UpdateUserWithRoles;
using AutoMapper;
using Domain.Entities.Identity;

namespace Application.Features.Users.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UpdateUserWithRolesCommandRequest>().ReverseMap();
    }
}