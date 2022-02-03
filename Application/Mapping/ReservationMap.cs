using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

internal class ReservationMap : IMap
{
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<CreateReservationDto, Reservation>();

        profile.CreateMap<Reservation, ReservationDto>()
            .ForMember(r => r.ReservedByFirstName, c => c.MapFrom(s => s.ReservedBy.FirstName))
            .ForMember(r => r.ReservedByLastName, c => c.MapFrom(s => s.ReservedBy.LastName))
            .ForMember(r => r.MovieTitle, c => c.MapFrom(s => s.Showing.Movie.Title))
            .ForMember(r => r.ShowingStarts, c => c.MapFrom(s => s.Showing.Starts));
    }
}
