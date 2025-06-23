using AspnetCoreWebApiProjectPractice.DTO.Book;
using AspnetCoreWebApiProjectPractice.Models;
using AutoMapper;

namespace AspnetCoreWebApiProjectPractice.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, CreateBookDto>().ReverseMap();
        }
    }
}
