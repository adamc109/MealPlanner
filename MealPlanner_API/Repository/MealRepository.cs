using MealPlanner_API.Data;
using MealPlanner_API.Models;
using MealPlanner_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MealPlanner_API.Repository
{
    //Repository for Meals Table in Db
    public class MealRepository : Repository<Meal>, IMealRepository
    {
        private readonly ApplicationDbContext _db;
        public MealRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public async Task<Meal> UpdateAsync(Meal entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Meals.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
