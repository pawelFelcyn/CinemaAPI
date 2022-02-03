using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Modification;

namespace Application.Mapping;

internal class MovieMap : IMap
{
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<CreateMovieDto, Movie>()
            .ForMember(m => m.Duration, s => s.MapFrom(c => TimeSpan.Parse(c.FilmDuration)));

        profile.CreateMap<Movie, MovieDetailsDto>();

        profile.CreateMap<Movie, MovieDto>();

        profile.CreateMap<UpdateMovieDto, MovieModificationParams>();
    }
}
