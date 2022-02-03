using Application.Authorization;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.Modification;
using Microsoft.AspNetCore.Authorization;

namespace Application.Services;
public class CinemaService : ICinemaService
{
    private readonly ICinemaRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;
    private readonly IAuthorizationService _authorizationService;

    public CinemaService(ICinemaRepository repository, IMapper mapper, IUserContextService userContextService,
        IAuthorizationService authorizationService)
    {
        _repository = repository;
        _mapper = mapper;
        _userContextService = userContextService;
        _authorizationService = authorizationService;
    }

    public Page<CinemaDto> GetAll(ResourceQuery query)
    {
        var page = _repository.GetAll(query);

        return new Page<CinemaDto>()
        {
            Items = _mapper.Map<IEnumerable<CinemaDto>>(page.Items),
            TotalPagesAmount = page.TotalPagesAmount,
            ItemsFrom = page.ItemsFrom,
            ItemsTo = page.ItemsTo,
            PageNumber = page.PageNumber,
            PageSize = page.PageSize
        };
    }

    public CinemaDetailsDto GetById(int id)
    {
        var cinema = _repository.GetById(id);

        return _mapper.Map<CinemaDetailsDto>(cinema);
    }

    public CinemaDetailsDto Create(CreateCinemaDto dto)
    {
        var cinema = _mapper.Map<Cinema>(dto);
        cinema.CreatedById = _userContextService.UserId;
        cinema = _repository.Add(cinema);

        return _mapper.Map<CinemaDetailsDto>(cinema);
    }

    public CinemaDetailsDto Update(UpdateCinemaDto dto, int id)
    {
        var cinema = _repository.GetById(id);

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, cinema, new CinemaOperationRequirement()).Result;

        if (!authorizationResult.Succeeded)
        {
            throw new CantUpdateCinemaException();
        }

        var modParams = _mapper.Map<CinemaModificationParams>(dto);
        cinema = _repository.Update(cinema, modParams);

        return _mapper.Map<CinemaDetailsDto>(cinema);
    }

    public void Delete(int id)
    {
        var cinema = _repository.GetById(id);

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, cinema, new CinemaOperationRequirement()).Result;

        if (!authorizationResult.Succeeded)
        {
            throw new CantDeleteCinemaException();
        }

        _repository.Remove(cinema);
    }
}
