using System.ComponentModel.DataAnnotations;

namespace MealPlanner_Web.Models.Dto
{
    public class MealIngredientsDTO
    {
        public int Id { get; set; }
        public string Ingredient { get; set; }
        [Required]
        public int MealID { get; set; }

        public int Quanitiy { get; set; }
        public string Unit { get; set; }
    }
}

