using Application.Features.Auth.Commands.Register;
using AutoMapper;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User,RegisterCommandRequest>().ReverseMap();
        CreateMap<User, RegisterCommandResponse>().ReverseMap();
    }
}
