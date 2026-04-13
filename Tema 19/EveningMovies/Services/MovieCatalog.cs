using EveningMovies.Models;

namespace EveningMovies.Services;

public class MovieCatalog
{
    private readonly List<Movie> _movies = new();
    private int _nextId = 1;

    public IReadOnlyList<Movie> GetByRecommender(string friendName)
    {
        var key = friendName.Trim();
        return _movies
            .Where(m => string.Equals(m.RecommendedBy, key, StringComparison.OrdinalIgnoreCase))
            .OrderBy(m => m.Title)
            .ToList();
    }

    public void Add(Movie movie)
    {
        movie.Id = _nextId++;
        _movies.Add(movie);
    }
}
