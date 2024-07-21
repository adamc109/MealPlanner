using MealPlanner_API.Models;
using System.Linq.Expressions;

namespace MealPlanner_API.Repository.IRepository
{
    public interface IMealRepository
    {
        Task<List<Meal>> GetAll(Expression<Func<Meal,bool>> filter = null);
        Task<Meal> Get(Expression<Func<Meal, bool>> filter = null, bool tracked=true);

        Task Create(Meal entity);
        Task Remove(Meal entity);
        Task Save();
    }
}
