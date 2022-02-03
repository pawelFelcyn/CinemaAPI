using Application.Dtos;
using Application.Query;
using Application.Services;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CinemasController : ControllerBase
{
    private readonly ICinemaService _service;

    public CinemasController(ICinemaService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<Page<CinemaDto>> GetAll([FromQuery]ResourceQuery<Cinema> query)
    {
        var page = _service.GetAll(query);

        return Ok(page);
    }

    [HttpGet("{id}")]
    public ActionResult<CinemaDetailsDto> GetById([FromRoute]int id)
    {
        var cinema = _service.GetById(id);

        return Ok(cinema);
    }

    [HttpPost]
    [Authorize(Roles ="Manager")]
    public ActionResult<CinemaDetailsDto> Create([FromBody]CreateCinemaDto dto)
    {
        var cinema = _service.Create(dto);

        return Created($"api/Cinamas/{cinema.Id}", cinema);
    }

    [HttpPut("{id}")]
    public ActionResult<CinemaDetailsDto> Update([FromBody]UpdateCinemaDto dto, [FromRoute]int id)
    {
        var cinema = _service.Update(dto, id);

        return Ok(cinema);
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute]int id)
    {
        _service.Delete(id);

        return NoContent();
    }
}
