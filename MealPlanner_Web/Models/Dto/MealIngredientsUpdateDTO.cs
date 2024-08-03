using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealPlanner_Web.Models.Dto
{
    public class MealIngredientsUpdateDTO
    {
        [Key]
        //automatically creates  Id value when inserting new meal
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int MealID { get; set; }
        public string Ingredient { get; set; }

        public int Quanitiy { get; set; }
        public string Unit { get; set; }
    }
}

