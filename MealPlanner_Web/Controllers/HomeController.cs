using AutoMapper;
using MealPlanner_API.Models;
using MealPlanner_Web.Models;
using MealPlanner_Web.Models.Dto;
using MealPlanner_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MealPlanner_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IMapper _mapper;
        public HomeController(IMealService mealService, IMapper mapper)
        {
            _mealService = mealService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<MealDTO> list = new();

            var response = await _mealService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<MealDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
    }
}
