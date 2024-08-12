using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MealPlanner_Web.Models.Dto
{
    public class MealCreateDTO
    {
        


        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string URL { get; set; }
        public string Image { get; set; }
        [Range(0, 10)]
        [DisplayName("Health Rating")]
        public int HealthRating { get; set; }
    }
}

