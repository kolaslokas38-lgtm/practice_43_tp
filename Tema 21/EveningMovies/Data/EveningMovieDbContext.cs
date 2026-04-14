using EveningMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace EveningMovies.Data;

public class EveningMovieDbContext(DbContextOptions<EveningMovieDbContext> options) : DbContext(options)
{
    public DbSet<EveningMovie> EveningMovies => Set<EveningMovie>();
}
