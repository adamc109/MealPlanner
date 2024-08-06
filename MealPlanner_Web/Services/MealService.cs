using MealPlanner_Utiliy;
using MealPlanner_Web.Models;
using MealPlanner_Web.Models.Dto;
using MealPlanner_Web.Services.IServices;

namespace MealPlanner_Web.Services
{
    public class MealService : BaseService, IMealService
    {


        private readonly IHttpClientFactory _clientFactory;
        private string mealUrl;
        public MealService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            mealUrl = configuration.GetValue<string>("ServiceUrls:MealAPI");

        }

        public Task<T> CreateAsync<T>(MealCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = mealUrl + "/api/mealAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = mealUrl + "/api/mealAPI"+id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = mealUrl + "/api/mealAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = mealUrl + "/api/mealAPI" + id
            });
        }

        public Task<T> UpdateAsync<T>(MealUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = mealUrl + "/api/mealAPI"+dto.Id
            });
        }
    }

}


