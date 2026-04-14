using EveningMovies.Data;
using EveningMovies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace EveningMovies.Controllers;

public class EveningMovieController : Controller
{
    private readonly EveningMovieDbContext _dbContext;

    public EveningMovieController(EveningMovieDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? moodTag)
    {
        ViewBag.MoodTag = moodTag;
        ViewBag.Message = TempData["Message"] as string;
        var query = _dbContext.EveningMovies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(moodTag))
        {
            var normalizedMoodTag = moodTag.Trim();
            query = query.Where(m => m.MoodTag.ToLower() == normalizedMoodTag.ToLower());
        }

        var movies = await query
            .OrderBy(m => m.Title)
            .ToListAsync();

        return View(movies);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new EveningMovie());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EveningMovie model)
    {
        if (!ModelState.IsValid)
            return View(model);

        model.Title = model.Title.Trim();
        model.Genre = model.Genre.Trim();
        model.MoodTag = model.MoodTag.Trim();
        model.AddedBy = model.AddedBy.Trim();

        _dbContext.EveningMovies.Add(model);
        await _dbContext.SaveChangesAsync();

        TempData["Message"] = $"Фильм \"{model.Title}\" добавлен.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _dbContext.EveningMovies.FirstOrDefaultAsync(m => m.Id == id);
        if (movie is null)
        {
            TempData["Message"] = "Фильм не найден.";
            return RedirectToAction(nameof(Index));
        }

        _dbContext.EveningMovies.Remove(movie);
        await _dbContext.SaveChangesAsync();
        TempData["Message"] = $"Фильм \"{movie.Title}\" удален.";
        return RedirectToAction(nameof(Index));
    }
}
