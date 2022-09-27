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
        public async Task<List<TEntity>> Get()
        {
            var result = await repository.Get();
            return result;
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
        public HttpStatusCode Post(TEntity entity)
        {
            var result = repository.Post(entity);
            return result;
        }

        //PUT
        [HttpPut]
        public HttpStatusCode Put(int id, TEntity entity)
        {
            var result = repository.Put(id, entity);
            return result;
        }

        //DELETE
        [HttpDelete]
        public HttpStatusCode DeleteEntity(TEntity entity)
        {
            var result = repository.Delete(entity.Id);
            return result;
        }
    }
}
