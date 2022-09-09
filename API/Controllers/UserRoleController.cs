using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : BaseController<UserRoleRepository, UserRole, int>
    {
        public UserRoleController(UserRoleRepository userRoleRepository) : base(userRoleRepository)
        {

        }
    }
}
