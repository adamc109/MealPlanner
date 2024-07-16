using System.ComponentModel.DataAnnotations;

namespace MealPlanner_API.Models
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Image { get; set; }
        [Range(0, 10)]
        public int HealthRating { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
