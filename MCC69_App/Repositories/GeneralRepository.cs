using API.Repositories.Interface;
using MCC69_App.Repositories.Interface;
using MCC69_App.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MCC69_App.Repositories
{
    public class GeneralRepository<Entity> : IGeneralRepository<Entity>
        where Entity : class, IEntity
    {
        private readonly string request;
        private readonly string address;
        
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public GeneralRepository(string request)
        {
            this.address = "https://localhost:44382/api/";
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address)
            };
            var token = _contextAccessor.HttpContext.Session.GetString("Token");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        //DELETE
        public HttpStatusCode Delete(int id)
        {
            var result = httpClient.DeleteAsync(request + id).Result;
            return result.StatusCode;
        }

        //GET ALL
        public async Task<List<Entity>> Get()
        {
            Json<Entity> entities = new Json<Entity>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<Json<Entity>>(apiResponse);
            }
            return entities.data;
        }

        //GET BY ID
        public async Task<Entity> Get(int? id)
        {
            //Entity entity = null;
            Results<Entity> entity = new Results<Entity>();

            using (var response = await httpClient.GetAsync(request + id.ToString()))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<Results<Entity>>(apiResponse);
            }
            return entity.data;
        }

        //POST
        public HttpStatusCode Post(Entity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address + request, content).Result;
            return result.StatusCode;
        }

        //PUT
        public HttpStatusCode Put(int id, Entity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + id, content).Result;
            return result.StatusCode;
        }
    }
}
