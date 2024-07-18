using MealPlanner_API.Data;
using MealPlanner_API.Models;
using MealPlanner_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanner_API.Controllers
{
    [Route("api/MealAPI")]
    [ApiController]
    public class MealAPIController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        public MealAPIController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //returns all meals
        public ActionResult<IEnumerable<MealDTO>> GetMeals()
        {
            
            return Ok(_db.Meals.ToList());
        }

        //returns one meal based on Id
        [HttpGet("{id:int}", Name ="GetMeal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MealDTO> GetMeal(int id)
        {
            if (id == 0)
            {
                
                return BadRequest();
            };

            var meal = _db.Meals.FirstOrDefault(u => u.Id == id);
            //checks if meal is not found
            if (meal == null)
            {
                return NotFound();
            }

            return Ok(meal);
        }

        //add meal
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MealDTO> CreateMeal([FromBody]MealDTO mealDTO) 
        {
            //returns name if it already exists, if Name does not exist returns null
            if (_db.Meals.FirstOrDefault(u => u.Name.ToLower() == mealDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Meal Already Exists");
                return BadRequest(ModelState);
            }

            if (mealDTO == null)
            {
                return BadRequest(mealDTO);
            }
            if (mealDTO.Id < 0) 
            { 
                return StatusCode(StatusCodes.Status500InternalServerError); 
            }

            //converts mealDTO to meal
            Meal model = new()
            {
                Id = mealDTO.Id,
                Name = mealDTO.Name,
                URL = mealDTO.URL,
                Image = mealDTO.Image
            };
            //creates new ID by +1 to highest existing ID
            _db.Meals.Add(model);           
            _db.SaveChanges();


            return CreatedAtRoute("GetMeal", new { id = mealDTO.Id }, mealDTO);


        }


        //delete meal
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteMeal")]
        public IActionResult DeleteMeal(int id)
        { 
            if (id == 0)
            {
                return BadRequest();
            }
            var meal = _db.Meals.FirstOrDefault(u => u.Id == id);
            if (meal == null)
            {
                return NotFound();
            }
            _db.Meals.Remove(meal);
            _db.SaveChanges();
            return NoContent();
           
        }

        [HttpPut("{id:int}", Name = "UpdateMeal")]
        public IActionResult UpdateMeal(int id, [FromBody] MealDTO mealDTO)
        {

            //checks is meal exists or id is different
            if (mealDTO == null || id != mealDTO.Id)
            {
                return BadRequest();
            }

            //retrive meal based on Id
            //var meal = MealStore.mealList.FirstOrDefault(u => u.Id == id);
            //meal.Name = mealDTO.Name;
            //meal.Image = mealDTO.Image;
            //meal.URL = mealDTO.URL;
            //meal.HealthRating = mealDTO.HealthRating;

            Meal model = new()
            {
                Id = mealDTO.Id,
                Name = mealDTO.Name,
                URL = mealDTO.URL,
                Image = mealDTO.Image
            };

            _db.Meals.Update(model);
            _db.SaveChanges();
            return NoContent();

        }

    }  
}
