using MealPlanner_API.Models;
using System.Linq.Expressions;

namespace MealPlanner_API.Repository.IRepository
{
    public interface IMealIngredientsRepository : IRepository<MealIngredients>
    {

        Task<MealIngredients> UpdateAsync(MealIngredients entity);

    }
}
