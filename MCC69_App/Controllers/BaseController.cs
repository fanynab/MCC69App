using API.Repositories.Interface;
using MCC69_App.Repositories.Interface;
using MCC69_App.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MCC69_App.Controllers
{
    public class BaseController<TEntity, TRepository> : Controller
           where TEntity : class, IEntity
           where TRepository : IGeneralRepository<TEntity>
    {
        private readonly TRepository repository;

        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }

        //GET ALL
        [HttpGet]
        /*public async Task<List<TEntity>> GetAll()
        {
            var result = await repository.Get();
            return result;
        }*/
        public async Task<JsonResult> GetAll()
        {
            var result = await repository.GetAll();
            return Json(result);
        }

        //GET BY ID
        [HttpGet]
        public async Task<TEntity> Get(int id)
        {
            var result = await repository.Get(id);
            return result;
        }

        //POST
        [HttpPost]
        /*public HttpStatusCode Post(TEntity entity)
        {
            var result = repository.Post(entity);
            return result;
        }*/
        public JsonResult Post(TEntity entity)
        {
            var result = repository.Post(entity);
            return Json(result);
        }

        //PUT
        [HttpPut]
        public JsonResult Put(int id, TEntity entity)
        {
            var result = repository.Put(id, entity);
            return Json(result);
        }


        //DELETE
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = repository.Delete(id);
            return Json(result);
        }
    }
}
