using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Commands.DeleteBook;
using Application.Features.Books.Commands.UpdateBook;
using Application.Features.Books.Queries.GetByIdBook;
using Application.Features.Books.Queries.GetListBook;
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

        CreateMap<Book, GetByIdBookQueryResponse>().ReverseMap();
        //CreateMap<Book, GetByIdBookQueryResponse>().ForMember(r => r.AuthorName, opt => opt.MapFrom(b => b.Author.Name));
        CreateMap<Book, GetListBookQueryResponse>().ForMember(r => r.AuthorName, opt => opt.MapFrom(b => b.Author.Name));
    }
}
