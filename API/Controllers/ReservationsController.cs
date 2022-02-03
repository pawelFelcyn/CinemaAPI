using Application.Dtos;
using Application.Query;
using Application.Services;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/Cinemas/{cinemaId}/Showings/{showingId}/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationsController(IReservationService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<Page<ReservationDto>> GetAll([FromRoute] int cinemaId, [FromRoute] int showingId, [FromQuery] ResourceQuery<Reservation> query)
    {
        var page = _service.GetAll(cinemaId, showingId, query);

        return Ok(page);
    }

    [HttpGet("{reservationId}")]
    public ActionResult<ReservationDto> GetById([FromRoute] int cinemaId, [FromRoute] int showingId, [FromRoute]int reservationId)
    {
        var reservation = _service.GetById(cinemaId, showingId, reservationId);

        return Ok(reservation);
    }

    [HttpPost]
    [Authorize(Roles="User")]
    public ActionResult<ReservationDto> Create([FromRoute]int cinemaId, [FromRoute]int showingId, [FromBody]CreateReservationDto dto)
    {
        var reservation = _service?.Create(cinemaId, showingId, dto);

        return Created($"api/Cinemas/{cinemaId}/Showings/{showingId}/Reservations/{reservation.Id}", reservation);
    }
}
