using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

internal class AccountMap : IMap
{
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<RegisterDto, User>();
    }
}
