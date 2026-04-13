using EveningMovies.Models;
using EveningMovies.Services;
using Microsoft.AspNetCore.Mvc;

namespace EveningMovies.Controllers;

public class EveningController : Controller
{
    private readonly MovieCatalog _catalog;

    public EveningController(MovieCatalog catalog)
    {
        _catalog = catalog;
    }

    [HttpGet]
    public IActionResult ByFriend(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return NotFound();

        var movies = _catalog.GetByRecommender(name);
        ViewBag.FriendName = name.Trim();
        return View(movies);
    }

    [HttpGet]
    public IActionResult Propose()
    {
        return View(new Movie());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Propose(Movie movie)
    {
        if (!ModelState.IsValid)
            return View(movie);

        _catalog.Add(movie);
        return RedirectToAction(nameof(ByFriend), new { name = movie.RecommendedBy });
    }
}
