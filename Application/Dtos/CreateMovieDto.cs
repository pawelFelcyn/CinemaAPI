namespace Application.Dtos;

public record CreateMovieDto(string Title, string Director, string Description, DateTime? DateOfRelease, string FilmDuration);
