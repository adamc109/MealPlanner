﻿using System.ComponentModel.DataAnnotations;

namespace MealPlanner_API.Models.Dto
{
    public class MealCreateDTO
    {
        


        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string URL { get; set; }
        public string Image { get; set; }
        [Range(0, 10)]
        public int HealthRating { get; set; }
    }
}

