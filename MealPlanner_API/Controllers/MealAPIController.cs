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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //returns all meals
        public ActionResult<IEnumerable<MealDTO>> GetMeals()
        {
            return Ok(MealStore.mealList);
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

            var meal = MealStore.mealList.FirstOrDefault(u => u.Id == id);
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
            if (MealStore.mealList.FirstOrDefault(u => u.Name.ToLower() == mealDTO.Name.ToLower()) != null)
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

            //creates new ID by +1 to highest existing ID
            mealDTO.Id = MealStore.mealList.OrderByDescending(u=>u.Id).FirstOrDefault().Id +1;
            MealStore.mealList.Add(mealDTO);


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
            var meal = MealStore.mealList.FirstOrDefault(u => u.Id == id);
            if (meal == null)
            {
                return NotFound();
            }
            MealStore.mealList.Remove(meal);

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
            var meal = MealStore.mealList.FirstOrDefault(u => u.Id == id);
            meal.Name = mealDTO.Name;
            meal.Image = mealDTO.Image;
            meal.URL = mealDTO.URL;
            meal.HealthRating = mealDTO.HealthRating;

            return NoContent();

        }

    }  
}
