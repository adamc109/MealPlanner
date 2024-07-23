using MealPlanner_API.Models;
using System.Linq.Expressions;

namespace MealPlanner_API.Repository.IRepository
{
    //Generic Repository of class T
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);

        Task CreateAsync(T entity);
      

        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
