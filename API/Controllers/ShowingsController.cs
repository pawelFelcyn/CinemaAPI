using Application.Dtos;
using Application.Query;
using Application.Services;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/Cinemas/{cinemaId}/[controller]")]
[ApiController]
public class ShowingsController : ControllerBase
{
    private readonly IShowingService _service;

    public ShowingsController(IShowingService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<Page<ShowingDto>> GetAll([FromRoute]int cinemaId, [FromQuery]ResourceQuery<Showing> query)
    {
        var page = _service.GetAll(cinemaId, query);

        return Ok(page);
    }

    [HttpGet("{showingId}")]
    public ActionResult<ShowingDetailsDto> GetById([FromRoute]int cinemaId, [FromRoute]int showingId)
    {
        var showing = _service.GetById(cinemaId, showingId);

        return Ok(showing);
    }

    [HttpPost]
    public ActionResult<ShowingDetailsDto> Create([FromRoute]int cinemaId, [FromBody]CreateShowingDto dto)
    {
        var showing = _service.Create(cinemaId, dto);

        return Created($"api/Cinemas/{cinemaId}/Showings{showing.Id}", showing);
    }

    [HttpDelete("{showingId}")]
    public ActionResult Delete([FromRoute]int cinemaId, [FromRoute]int showingId)
    {
        _service.Delete(cinemaId, showingId);

        return NoContent();
    }

}
