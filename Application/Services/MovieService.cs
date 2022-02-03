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

public class MovieService : IMovieService
{
    private readonly IMovieRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;
    private readonly IAuthorizationService _authorizationService;

    public MovieService(IMovieRepository repository, IMapper mapper, IUserContextService userContextService,
        IAuthorizationService authorizationService)
    {
        _repository = repository;
        _mapper = mapper;
        _userContextService = userContextService;
        _authorizationService = authorizationService;
    }

    public Page<MovieDto> GetAll(ResourceQuery query)
    {
        var page = _repository.GetAll(query);

        return new Page<MovieDto>()
        {
            Items = _mapper.Map<IEnumerable<MovieDto>>(page.Items),
            TotalPagesAmount = page.TotalPagesAmount,
            ItemsFrom = page.ItemsFrom,
            ItemsTo = page.ItemsTo,
            PageNumber = page.PageNumber,
            PageSize = page.PageSize
        };
    }

    public MovieDetailsDto GetById(int id)
    {
        var movie = _repository.GetById(id);

        return _mapper.Map<MovieDetailsDto>(movie);
    }

    public MovieDetailsDto Create(CreateMovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);
        movie.CreatedById = _userContextService.UserId;
        movie = _repository.Add(movie);

        return _mapper.Map<MovieDetailsDto>(movie);
    }

    public MovieDetailsDto Update(UpdateMovieDto dto, int id)
    {
        var movie = _repository.GetById(id);

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, movie, new MovieOperationRequirement()).Result;

        if (!authorizationResult.Succeeded)
        {
            throw new CantUpdateMovieException();
        }

        var modParams = _mapper.Map<MovieModificationParams>(dto);
        movie = _repository.Update(movie, modParams);

        return _mapper.Map<MovieDetailsDto>(movie);
    }

    public void Delete(int id)
    {
        var movie = _repository.GetById(id);

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, movie, new MovieOperationRequirement()).Result;

        if (!authorizationResult.Succeeded)
        {
            throw new CantDeleteMovieException();
        }

        _repository.Remove(movie);
    }
}
