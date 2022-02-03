using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Helper;
using Infrastructure.Pagination;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICinemaRepository, CinemaRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IShowingRepository, ShowingRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

        return services;
    }

    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        services.AddScoped<IEmailValidationHelper, EmailValidationHelper>();
        services.AddScoped<IMovieIdValidationHelper, MovieIdValidationHelper>();

        return services;
    }

    public static IServiceCollection AddPagination(this IServiceCollection services)
    {
        services.AddScoped<IPageCreator<Cinema>, PageCreator<Cinema>>();
        services.AddScoped<IPageCreator<Movie>, PageCreator<Movie>>();
        services.AddScoped<IPageCreator<Showing>, PageCreator<Showing>>();
        services.AddScoped<IPageCreator<Reservation>, PageCreator<Reservation>>();
        services.AddScoped<IFilter<Cinema>, CinemaFilter>();
        services.AddScoped<IFilter<Movie>, MovieFilter>();
        services.AddScoped<IFilter<Showing>, ShowingFilter>();
        services.AddScoped<IFilter<Reservation>, ReservationFilter>();
        services.AddScoped<ISorter<Cinema>, CinemaSorter>();
        services.AddScoped<ISorter<Movie>, MovieSorter>();
        services.AddScoped<ISorter<Showing>, ShowingSorter>();
        services.AddScoped<ISorter<Reservation>, ReservationSorter>();

        return services;
    }
}
