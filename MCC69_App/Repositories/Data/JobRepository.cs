using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCC69_App.Repositories.Data
{
    public class JobRepository : GeneralRepository<Job>
    {
        public JobRepository(string request = "Job/") : base(request)
        {

        }
    }
}
