using AutoMapper;
using MealPlanner_Web.Models.Dto;


//config file for automapper
namespace MealPlanner_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {

            CreateMap<MealDTO, MealCreateDTO>().ReverseMap();
            CreateMap<MealDTO, MealUpdateDTO>().ReverseMap();
;

            CreateMap<MealIngredientsDTO, MealIngredientsCreateDTO>().ReverseMap();
            CreateMap<MealIngredientsDTO, MealIngredientsUpdateDTO>().ReverseMap();
        }
    }
}
