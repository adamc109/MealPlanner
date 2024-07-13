using System.ComponentModel.DataAnnotations;

namespace MealPlanner_API.Models.Dto
{
    public class MealDTO
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string URL { get; set; }
        public string Image { get; set; }
        public int HealthRating { get; set; }
    }
}

