using AutoMapper;
using MealPlanner_API.Models;
using MealPlanner_API.Models.Dto;

//config file for automapper
namespace MealPlanner_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {

            CreateMap<Meal, MealDTO>();
            CreateMap<MealDTO, Meal>();

            CreateMap<Meal, MealCreateDTO>().ReverseMap();
            CreateMap<Meal, MealUpdateDTO>().ReverseMap();

        }
    }
}
