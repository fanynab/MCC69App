using MCC69_App.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MCC69_App.Repositories.Interface
{
    public interface IGeneralRepository<Entity>
        where Entity : class
    {
        Task<List<Entity>> Get();
        Task<Entity> Get(int? id);
        HttpStatusCode Post(Entity entity);
        HttpStatusCode Put(int id, Entity entity);
        HttpStatusCode Delete(int id);
    }
}
