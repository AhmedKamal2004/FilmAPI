using AutoMapper;

namespace FilmAPI.Helpers
{
    public class MappingProfile:Profile 
    {
        public MappingProfile()
        {
            CreateMap<_Movie, MovieDetailsDto>();
            CreateMap<MoviesDto,_Movie>().ForMember(Src => Src.Poster, opt => opt.Ignore());
        }

    }
}
