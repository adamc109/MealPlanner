using MealPlanner_API.Models;
using MealPlanner_Web.Models;

namespace MealPlanner_Web.Services.IServices
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }

        Task<T> SendAsync<T>(APIRequest aPIRequest);
    }
}
