using MealPlanner_API.Models;
using System.Linq.Expressions;

namespace MealPlanner_API.Repository.IRepository
{
    public interface IMealRepository
    {
        Task<List<Meal>> GetAllAsync(Expression<Func<Meal,bool>> filter = null);
        Task<Meal> GetAsync(Expression<Func<Meal, bool>> filter = null, bool tracked=true);

        Task CreateAsync(Meal entity);
        Task UpdateAsync(Meal entity);
        Task RemoveAsync(Meal entity);
        Task SaveAsync();
    }
}
