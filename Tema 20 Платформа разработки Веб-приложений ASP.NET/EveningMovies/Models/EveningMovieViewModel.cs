using System.ComponentModel.DataAnnotations;

namespace EveningMovies.Models;

public class EveningMovieViewModel
{
    [Required(ErrorMessage = "Укажите фильм")]
    [Display(Name = "Фильм")]
    public string MovieTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите жанр")]
    [Display(Name = "Жанр")]
    public string Genre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите время начала")]
    [DataType(DataType.Time)]
    [Display(Name = "Время начала")]
    public TimeSpan StartTime { get; set; }
}
