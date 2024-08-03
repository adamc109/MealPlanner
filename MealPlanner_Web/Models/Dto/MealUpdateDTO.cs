using System.ComponentModel.DataAnnotations;

namespace MealPlanner_Web.Models.Dto
{
    public class MealUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]

        public string Name { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public string Image { get; set; }
        [Range(0, 10)]
        public int HealthRating { get; set; }
    }
}

