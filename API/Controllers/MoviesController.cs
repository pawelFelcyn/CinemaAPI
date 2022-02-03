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
[Authorize(Roles="Manager")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _service;

    public MoviesController(IMovieService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<Page<MovieDto>> GetAll([FromQuery]ResourceQuery<Movie> query)
    {
        var page = _service.GetAll(query);

        return Ok(page);
    }

    [HttpGet("{id}")]
    public ActionResult<MovieDetailsDto> GetById([FromRoute]int id)
    {
        var movie = _service.GetById(id);

        return Ok(movie);
    }

    [HttpPost]
    public ActionResult<MovieDetailsDto> Create([FromBody]CreateMovieDto dto)
    {
        var movie = _service.Create(dto);

        return Created($"api/Movies/{movie.Id}", movie);
    }

    [HttpPut("{id}")]
    public ActionResult<MovieDetailsDto> Update([FromBody]UpdateMovieDto dto, [FromRoute]int id)
    {
        var movie = _service.Update(dto, id);

        return Ok(movie);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute]int id)
    {
        _service.Delete(id);

        return NoContent();
    }
}
