namespace MealPlanner_API.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Image { get; set; }
        public int HealthRating { get; set; }
    }
}
