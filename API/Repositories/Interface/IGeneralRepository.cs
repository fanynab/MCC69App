using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    public interface IGeneralRepository<Entity, Primary>
        where Entity : class
    {
        List<Entity> Get();
        Entity Get(Primary id);
        int Post(Entity entity);
        int Put(Primary id, Entity entity);
        int Delete(Primary id);
    }
}
