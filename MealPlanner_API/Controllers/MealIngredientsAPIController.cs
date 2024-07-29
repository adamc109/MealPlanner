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
    [Route("api/MealIngredientsAPI")]
    [ApiController]
    public class MealIngredientsAPIController : ControllerBase
    {


        private readonly IMealIngredientsRepository _dbMealIngri;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public MealIngredientsAPIController(IMealIngredientsRepository dbMealIngri, IMapper mapper)
        {
            _dbMealIngri = dbMealIngri;
            _mapper = mapper;
            this._response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //returns all meal ingridients 
        public async Task<ActionResult<APIResponse>> GetMealIngredients()
        {

            try
            {
                //maps Meal's to MealDTO 
                IEnumerable<MealIngredients> mealIngriList = await _dbMealIngri.GetAllAsync();
                _response.Result = _mapper.Map<List<MealIngredientsDTO>>(mealIngriList);
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

        //returns one meal Ingredient based on Id
        [HttpGet("{id:int}", Name = "GetMealIngredient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetMealIngredient(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                };

                var mealingri = await _dbMealIngri.GetAsync(u => u.Id == id);
                //checks if meal is not found.
                if (mealingri == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                //maps Meal to MealDTO.
                _response.Result = _mapper.Map<MealIngredientsDTO>(mealingri);
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
        public async Task<ActionResult<APIResponse>> CreateMealIngredient([FromBody] MealIngredientsCreateDTO createDTO)
        {
            try
            {
                //returns name if it already exists, if Name does not exist returns null.
                if (await _dbMealIngri.GetAsync(u => u.Ingredient.ToLower() == createDTO.Ingredient.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Meal Already Exists");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                //Converts mealDTO to Meal
                MealIngredients mealIngri = _mapper.Map<MealIngredients>(createDTO);

                //creates new ID by +1 to highest existing ID. EF core handels ID.
                await _dbMealIngri.CreateAsync(mealIngri);
                _response.Result = _mapper.Map<MealIngredientsCreateDTO>(mealIngri);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetMeal", new { id = mealIngri.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;


        }


        //delete meal

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteMealIngridient")]
        public async Task<ActionResult<APIResponse>> DeleteMealIngridient(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var meal = await _dbMealIngri.GetAsync(u => u.Id == id);
                if (meal == null)
                {
                    return NotFound();
                }
                await _dbMealIngri.RemoveAsync(meal);
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

        [HttpPut("{id:int}", Name = "UpdateMealIngredient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateMealIngredient(int id, [FromBody] MealIngredientsUpdateDTO updateDTO)
        {
            try
            {

                //checks is meal exists or id is different
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }


                MealIngredients model = _mapper.Map<MealIngredients>(updateDTO);


                await _dbMealIngri.UpdateAsync(model);
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
