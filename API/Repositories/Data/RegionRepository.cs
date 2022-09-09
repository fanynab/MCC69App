using API.Context;
using API.Models;
using API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class RegionRepository : GeneralRepository<Region, int>
    {
        public RegionRepository(MyContext myContext) : base(myContext)
        {
            
        }
    }
}
