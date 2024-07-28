using MealPlanner_API.Data;
using MealPlanner_API.Models;
using MealPlanner_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MealPlanner_API.Repository
{
    //Repository for Meals Table in Db
    public class MealIngredientsRepository : Repository<MealIngredients>, IMealIngredientsRepository
    {
        private readonly ApplicationDbContext _db;
        public MealIngredientsRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public async Task<MealIngredients> UpdateAsync(MealIngredients entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.MealIngredients.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
