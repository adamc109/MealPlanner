using AutoMapper;
using MealPlanner_API.Data;
using MealPlanner_API.Models;
using MealPlanner_API.Models.Dto;
using MealPlanner_API.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MealPlanner_API.Controllers
{
    [Route("api/MealAPI")]
    [ApiController]
    public class MealAPIController : ControllerBase
    {


        private readonly IMealRepository _dbMeal;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public MealAPIController(IMealRepository dbMeal, IMapper mapper)
        {
            _dbMeal = dbMeal;
            _mapper = mapper;
            this._response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //returns all meals
        public async Task<ActionResult<APIResponse>> GetMeals()
        {

            try
            {
                //maps Meal's to MealDTO 
                IEnumerable<Meal> mealList = await _dbMeal.GetAllAsync();
                _response.Result = _mapper.Map<List<MealDTO>>(mealList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;

        }

        //returns one meal based on Id
        [HttpGet("{id:int}", Name = "GetMeal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetMeal(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                };

                var meal = await _dbMeal.GetAsync(u => u.Id == id);
                //checks if meal is not found.
                if (meal == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                //maps Meal to MealDTO.
                _response.Result = _mapper.Map<MealDTO>(meal);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        //add meal
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateMeal([FromBody] MealCreateDTO createDTO)
        {
            try
            {
                //returns name if it already exists, if Name does not exist returns null.
                if (await _dbMeal.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Meal Already Exists");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                //Converts mealDTO to Meal
                Meal meal = _mapper.Map<Meal>(createDTO);

                //creates new ID by +1 to highest existing ID. EF core handels ID.
                await _dbMeal.CreateAsync(meal);
                _response.Result = _mapper.Map<MealDTO>(meal);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetMeal", new { id = meal.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;


        }


        //delete meal

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteMeal")]
        public async Task<ActionResult<APIResponse>> DeleteMeal(int id)
        {
            try
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
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateMeal")]
        public async Task<ActionResult<APIResponse>> UpdateMeal(int id, [FromBody] MealUpdateDTO updateDTO)
        {
            try
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
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;


        }

    }
}
