using System.ComponentModel.DataAnnotations;

namespace EveningMovies.Models;

public class EveningMovie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите название фильма")]
    [Display(Name = "Название")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите жанр")]
    [Display(Name = "Жанр")]
    public string Genre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите тег настроения")]
    [Display(Name = "Тег настроения")]
    public string MoodTag { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите, кто добавил фильм")]
    [Display(Name = "Добавил")]
    public string AddedBy { get; set; } = string.Empty;
}
