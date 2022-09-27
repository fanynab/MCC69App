using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCC69_App.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee>
    {
        public EmployeeRepository(string request = "Employee/") : base(request)
        {

        }
    }
}
