using AutoMapper;
using DSCC_CW1_API.Dtos;
using DSCC_CW1_API.Models;

namespace DSCC_CW1_API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();

            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
        }
    }
}
