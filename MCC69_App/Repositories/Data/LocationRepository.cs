using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCC69_App.Repositories.Data
{
    public class LocationRepository : GeneralRepository<Location>
    {
        public LocationRepository(string request = "Location/") : base(request)
        {

        }
    }
}
