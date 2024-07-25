using MealPlanner_API.Models;
using System.Linq.Expressions;

namespace MealPlanner_API.Repository.IRepository
{
    public interface IMealRepository : IRepository<Meal>
    {

        Task<Meal> UpdateAsync(Meal entity);

    }
}
