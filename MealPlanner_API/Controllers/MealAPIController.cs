using AutoMapper;
using MealPlanner_API.Data;
using MealPlanner_API.Models;
using MealPlanner_API.Models.Dto;
using MealPlanner_API.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MealPlanner_API.Controllers
{
    [Route("api/MealAPI")]
    [ApiController]
    public class MealAPIController : ControllerBase
    {

        
        private readonly IMealRepository _dbMeal;
        private readonly IMapper _mapper;

        public MealAPIController(IMealRepository dbMeal, IMapper mapper)
        {
            _dbMeal = dbMeal;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //returns all meals
        public async Task<ActionResult<IEnumerable<MealDTO>>> GetMeals()
        {
            IEnumerable<Meal> mealList = await _dbMeal.GetAllAsync();
            return Ok(_mapper.Map<List<MealDTO>>(mealList));
        }

        //returns one meal based on Id
        [HttpGet("{id:int}", Name ="GetMeal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MealDTO>> GetMealAsync(int id)
        {
            if (id == 0)
            {
                
                return BadRequest();
            };

            var meal = await _dbMeal.GetAsync(u => u.Id == id);
            //checks if meal is not found
            if (meal == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MealDTO>(meal));
        }

        //add meal
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MealDTO>> CreateMeal([FromBody]MealCreateDTO createDTO) 
        {
            //returns name if it already exists, if Name does not exist returns null
            if (await _dbMeal.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Meal Already Exists");
                return BadRequest(ModelState);
            }

            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
  
            //converts mealDTO to meal

            Meal model = _mapper.Map<Meal>(createDTO);

            //creates new ID by +1 to highest existing ID
            await _dbMeal.CreateAsync(model);


            return CreatedAtRoute("GetMeal", new { id = model.Id }, model);


        }


        //delete meal
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteMeal")]
        public async Task<IActionResult> DeleteMeal(int id)
        { 
            if (id == 0)
            {
                return BadRequest();
            }
            var meal = await _dbMeal.GetAsync(u => u.Id == id);
            if (meal == null)
            {
                return NotFound();
            }
            await _dbMeal.RemoveAsync(meal);
            return NoContent();
           
        }

        [HttpPut("{id:int}", Name = "UpdateMeal")]
        public async Task<IActionResult> UpdateMeal(int id, [FromBody] MealUpdateDTO updateDTO)
        {

            //checks is meal exists or id is different
            if (updateDTO == null || id != updateDTO.Id)
            {
                return BadRequest();
            }

            //retrive meal based on Id
            //var meal = MealStore.mealList.FirstOrDefault(u => u.Id == id);
            //meal.Name = mealDTO.Name;
            //meal.Image = mealDTO.Image;
            //meal.URL = mealDTO.URL;
            //meal.HealthRating = mealDTO.HealthRating;

            Meal model = _mapper.Map<Meal>(updateDTO);


            await _dbMeal.UpdateAsync(model);
            
            return NoContent();

        }

    }  
}
