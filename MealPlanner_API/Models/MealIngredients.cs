using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealPlanner_API.Models
{
    public class MealIngredients
    {
        [Key]
        //automatically creates  Id value when inserting new meal
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ingredient {  get; set; }

        public int Quanitiy { get; set; }
        public string Unit { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        //add fk to recipie

    }
}
