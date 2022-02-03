using Application.Authorization;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IShowingRepository _showingRepository;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authorizationService;

    public ReservationService(IReservationRepository reservationRepository, IShowingRepository showingRepository, IUserContextService userContextService, IMapper mapper,
        IAuthorizationService authorizationService)
    {
        _reservationRepository = reservationRepository;
        _showingRepository = showingRepository;
        _userContextService = userContextService;
        _mapper = mapper;
        _authorizationService = authorizationService;
    }

    public Page<ReservationDto> GetAll(int cinemaId, int showingId, ResourceQuery query)
    {
        var showing = _showingRepository.GetById(cinemaId, showingId);

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, showing, new ShowingOperationRequirement()).Result;
        if (!authorizationResult.Succeeded)
        {
            throw new CantGetReservationsException();
        }

        var page = _reservationRepository.GetAll(cinemaId, showingId, query);

        return new Page<ReservationDto>()
        {
            Items = _mapper.Map<IEnumerable<ReservationDto>>(page.Items),
            TotalPagesAmount = page.TotalPagesAmount,
            ItemsFrom = page.ItemsFrom,
            ItemsTo = page.ItemsTo,
            PageNumber = page.PageNumber,
            PageSize = page.PageSize
        };
    }

    public ReservationDto GetById(int cinemaId, int showingId, int resrevationId)
    {
        var reservation = _reservationRepository.GetById(cinemaId, showingId, resrevationId);

        var authorisationResult = _authorizationService.AuthorizeAsync(_userContextService.User, reservation, new ReservationOperationRequirement()).Result;
        if (!authorisationResult.Succeeded)
        {
            throw new CantGetReservationException();
        }

        return _mapper.Map<ReservationDto>(reservation);
    }

    public ReservationDto Create(int cinemaId, int showingId, CreateReservationDto dto)
    {
        var showing = _showingRepository.GetById(cinemaId, showingId);
        if (showing.TicketsAmount < dto.FullPriceTicketsAmount + dto.HalfPriceTicketsAmount)
        {
            throw new TooFewTicketsException();
        }

        var reservation = _mapper.Map<Reservation>(dto);
        reservation.ShowingId = showingId;
        reservation.ReservedById = _userContextService.UserId;
        reservation = _reservationRepository.Add(reservation);

        return _mapper.Map<ReservationDto>(reservation);
    }
}
