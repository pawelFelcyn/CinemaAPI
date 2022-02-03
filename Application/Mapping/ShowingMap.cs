using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

internal class ShowingMap : IMap
{
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<CreateShowingDto, Showing>();

        profile.CreateMap<Showing, ShowingDetailsDto>()
            .ForMember(s => s.MovieDirector, c => c.MapFrom(d => d.Movie.Director))
            .ForMember(s => s.MovieTitle, c => c.MapFrom(d => d.Movie.Title))
            .ForMember(s => s.MovieDescription, c => c.MapFrom(d => d.Movie.Description))
            .ForMember(s => s.MovieDuration, c => c.MapFrom(d => d.Movie.Duration))
            .ForMember(s => s.CinemaName, c => c.MapFrom(d => d.Cinema.Name));

        profile.CreateMap<Showing, ShowingDto>()
            .ForMember(d => d.MovieTitle, c => c.MapFrom(s => s.Movie.Title));
    }
}
