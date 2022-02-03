using Application.Authorization;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Application.Services;

public class ShowingService : IShowingService
{
    private readonly IShowingRepository _showingRepository;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICinemaRepository _cinemaRepository;

    public ShowingService(IShowingRepository showingRepository, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService,
        ICinemaRepository cinemaRepository)
    {
        _showingRepository = showingRepository;
        _mapper = mapper;
        _userContextService = userContextService;
        _authorizationService = authorizationService;
        _cinemaRepository = cinemaRepository;
    }

    public Page<ShowingDto> GetAll(int cinemaId, ResourceQuery query)
    {
        var page = _showingRepository.GetAll(cinemaId, query);

        return new Page<ShowingDto>()
        {
            Items = _mapper.Map<IEnumerable<ShowingDto>>(page.Items),
            TotalPagesAmount = page.TotalPagesAmount,
            ItemsFrom = page.ItemsFrom,
            ItemsTo = page.ItemsTo,
            PageNumber = page.PageNumber,
            PageSize = page.PageSize
        };
    }

    public ShowingDetailsDto GetById(int cinemaId, int showingId)
    {
        var showing = _showingRepository.GetById(cinemaId, showingId);

        return _mapper.Map<ShowingDetailsDto>(showing);
    }

    public ShowingDetailsDto Create(int cinemaId, CreateShowingDto dto)
    {
        var cinema = _cinemaRepository.GetById(cinemaId);

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, cinema, new CinemaOperationRequirement()).Result;
        if (!authorizationResult.Succeeded)
        {
            throw new CantCreateShowingException();
        }

        var showing = _mapper.Map<Showing>(dto);
        showing.CreatedById = _userContextService.UserId;
        showing.CinemaId = cinemaId;

        showing = _showingRepository.Add(showing);

        return _mapper.Map<ShowingDetailsDto>(showing);
    }

    public void Delete(int cinemaId, int showingId)
    {
        var showing = _showingRepository.GetById(cinemaId, showingId);

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, showing, new ShowingOperationRequirement()).Result;
        if (!authorizationResult.Succeeded)
        {
            throw new CantDeleteShowingException();
        }

        _showingRepository.Remove(showing);
    }
}
