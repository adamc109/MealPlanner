using MealPlanner_API.Data;
using MealPlanner_API.Models;
using MealPlanner_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MealPlanner_API.Repository
{
    //Repository for Meals Table in Db
    public class MealRepository : IMealRepository
    {
        private readonly ApplicationDbContext _db;
        public MealRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Meal entity)
        {
            await _db.Meals.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<Meal> GetAsync(Expression<Func<Meal, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Meal> query = _db.Meals;

            //if query does not need to be tracked by EF core
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Meal>> GetAllAsync(Expression<Func<Meal, bool>> filter = null)
        {
            //IQueryable out of memory DB (not all records returned)
            IQueryable<Meal> query = _db.Meals;

            //applied filter if filer is not null
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
            
        }

        public async Task RemoveAsync(Meal entity)
        {
            _db.Meals.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Meal entity)
        {
            _db.Meals.Update(entity);
            await SaveAsync();
        }
    }
}
