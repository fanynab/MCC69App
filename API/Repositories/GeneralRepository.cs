using API.Context;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class GeneralRepository<Entity, Primary> : IGeneralRepository<Entity, Primary>
        where Entity : class, IEntity
    {
        MyContext myContext;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }


        //GETALL
        public List<Entity> Get()
        {
            var data = myContext.Set<Entity>().ToList();
            return data;
        }


        //GET
        public Entity Get(Primary id)
        {
            var data = myContext.Set<Entity>().Find(id);
            return data;
        }


        //POST
        public int Post(Entity entity)
        {
            myContext.Set<Entity>().Add(entity);
            var result = myContext.SaveChanges();
            return result;
        }


        //PUT
        public int Put(int id, Entity entity)
        {
            var data = myContext.Set<Entity>().Find(id);
            if (data == null)
            {
                return -1;
            }

            entity.Id = id;

            myContext.Entry(data).CurrentValues.SetValues(entity);
            //myContext.Entry(entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }


        //DELETE
        public int Delete(Primary id)
        {
            var data = myContext.Set<Entity>().Find(id);
            if (data == null)
            {
                return -1;
            }
            myContext.Set<Entity>().Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
