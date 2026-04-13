using System.ComponentModel.DataAnnotations;

namespace EveningMovies.Models;

public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите название")]
    [Display(Name = "Название")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите жанр")]
    [Display(Name = "Жанр")]
    public string Genre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите, кто рекомендует")]
    [Display(Name = "Рекомендовал")]
    public string RecommendedBy { get; set; } = string.Empty;
}
