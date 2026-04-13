using EveningMovies.Models;
using EveningMovies.Services;
using Microsoft.AspNetCore.Mvc;

namespace EveningMovies.Controllers;

public class EveningMovieController : Controller
{
    private readonly IEveningMovieService _eveningMovieService;

    public EveningMovieController(IEveningMovieService eveningMovieService)
    {
        _eveningMovieService = eveningMovieService;
    }

    [HttpGet]
    public IActionResult Index(string? genre)
    {
        ViewBag.Genre = genre;
        ViewBag.Message = TempData["Message"] as string;
        var movies = _eveningMovieService.GetByGenre(genre);
        return View(movies);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new EveningMovieViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EveningMovieViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _eveningMovieService.Add(model);
        TempData["Message"] = $"Фильм \"{model.MovieTitle}\" добавлен.";
        return RedirectToAction(nameof(Index));
    }
}
