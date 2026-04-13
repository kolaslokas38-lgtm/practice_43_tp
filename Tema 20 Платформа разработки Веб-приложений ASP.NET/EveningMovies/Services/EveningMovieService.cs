using EveningMovies.Models;

namespace EveningMovies.Services;

public class EveningMovieService : IEveningMovieService
{
    private readonly List<EveningMovieViewModel> _movies = new();

    public IReadOnlyList<EveningMovieViewModel> GetByGenre(string? genre)
    {
        if (string.IsNullOrWhiteSpace(genre))
            return _movies.OrderBy(m => m.MovieTitle).ToList();

        var normalizedGenre = genre.Trim();
        return _movies
            .Where(m => string.Equals(m.Genre, normalizedGenre, StringComparison.OrdinalIgnoreCase))
            .OrderBy(m => m.MovieTitle)
            .ToList();
    }

    public void Add(EveningMovieViewModel movie)
    {
        _movies.Add(new EveningMovieViewModel
        {
            MovieTitle = movie.MovieTitle.Trim(),
            Genre = movie.Genre.Trim(),
            StartTime = movie.StartTime
        });
    }
}
