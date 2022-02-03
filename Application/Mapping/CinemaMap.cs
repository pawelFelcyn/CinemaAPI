using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Modification;

namespace Application.Mapping;

public class CinemaMap : IMap
{
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<CreateCinemaDto, Cinema>()
            .ForMember(c => c.Address, m => m.MapFrom(s => new Address()
            {
                City = s.City,
                Street = s.Street,
                PostalCode = s.PostalCode
            }));

        profile.CreateMap<Cinema, CinemaDetailsDto>()
            .ForMember(c => c.City, m => m.MapFrom(s => s.Address.City))
            .ForMember(c => c.Street, m => m.MapFrom(s => s.Address.Street))
            .ForMember(c => c.PostalCode, m => m.MapFrom(s => s.Address.PostalCode));

        profile.CreateMap<Cinema, CinemaDto>();

        profile.CreateMap<UpdateCinemaDto, CinemaModificationParams>();
    }
}
