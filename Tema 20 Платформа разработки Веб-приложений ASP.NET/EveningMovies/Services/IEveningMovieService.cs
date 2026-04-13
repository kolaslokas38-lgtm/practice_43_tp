using EveningMovies.Models;

namespace EveningMovies.Services;

public interface IEveningMovieService
{
    IReadOnlyList<EveningMovieViewModel> GetByGenre(string? genre);
    void Add(EveningMovieViewModel movie);
}
