using MealPlanner_Web.Models.Dto;

namespace MealPlanner_Web.Services.IServices
{
    public interface IMealService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(MealCreateDTO dto);
        Task<T> UpdateAsync<T>(MealUpdateDTO dTO);
        Task<T> DeleteAsync<T>(int id);
    }
}
