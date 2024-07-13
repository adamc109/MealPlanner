using MealPlanner_API.Models.Dto;

namespace MealPlanner_API.Data
{
    public static class MealStore
    {
        public static List<MealDTO> mealList = new List<MealDTO>
        {
                new MealDTO{Id=1, Name = "test 1" , Image = "", URL= "", HealthRating = 0},
                new MealDTO{Id=2, Name = "test 2", Image = "", URL= "", HealthRating = 5}
        };
    }
}
