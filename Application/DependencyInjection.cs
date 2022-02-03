using Application.Authentication;
using Application.Authorization;
using Application.Dtos;
using Application.Query;
using Application.Services;
using Application.Validation;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICinemaService, CinemaService>();
        services.AddScoped<IUserContextService, UsereContextService>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IShowingService, ShowingService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthorizationHandler, CinemaOperationRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, MovieOperationRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, ShowingOperationRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, ReservationOperationRequirementHandler>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidation();
        services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
        services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
        services.AddScoped<IValidator<CreateCinemaDto>, CreateCinemaDtoValidator>();
        services.AddScoped<IValidator<CreateMovieDto>, CreateMovieDtoValidator>();
        services.AddScoped<IValidator<UpdateMovieDto>, UpdateMovieDtoValidator>();
        services.AddScoped<IValidator<ResourceQuery<Cinema>>, ResourceQueryValidator<ResourceQuery<Cinema>>>();
        services.AddScoped<IValidator<ResourceQuery<Movie>>, ResourceQueryValidator<ResourceQuery<Movie>>>();
        services.AddScoped<IValidator<ResourceQuery<Showing>>, ResourceQueryValidator<ResourceQuery<Showing>>>();
        services.AddScoped<IValidator<ResourceQuery<Reservation>>, ResourceQueryValidator<ResourceQuery<Reservation>>>();
        services.AddScoped<IValidator<CreateShowingDto>, CreateShowingDtoValidator>();

        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
