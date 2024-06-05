using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Commands.DeleteBook;
using Application.Features.Books.Commands.UpdateBook;
using Application.Features.Books.Queries.GetByIdBook;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Book, CreateBookCommandResponse>().ReverseMap();
        CreateMap<Book, CreateBookCommandRequest>().ReverseMap();

        CreateMap<Book, UpdateBookCommandResponse>().ReverseMap();
        CreateMap<Book, UpdateBookCommandRequest>().ReverseMap();

        CreateMap<Book, DeleteBookCommandResponse>().ReverseMap();
        CreateMap<Book, DeleteBookCommandRequest>().ReverseMap();

        CreateMap<Book, GetByIdBookQueryResponse>().ForMember(b => b.AuthorName, opt => opt.MapFrom(a => a.Author.Name));
    }
}
