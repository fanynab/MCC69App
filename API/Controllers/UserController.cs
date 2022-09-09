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
    public class UserController : BaseController<UserRepository, User, int>
    {
        UserRepository userRepository;

        public UserController(UserRepository userRepository) : base(userRepository)
        {
            this.userRepository = userRepository;
        }


        //LOGIN
        [HttpPost("Login")]
        public IActionResult Login(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                return BadRequest();
            var result = userRepository.Login(user.Username, user.Password);
            if (result == null)
                return NotFound();
            return Ok(new { result = 200, message = "Successfully Login" });
        }


        //REGISTER
        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password))
            {
                if (ModelState.IsValid)
                {
                    var result = userRepository.Register(user);
                    if (result > 0)
                        return Ok(new { result = 200, message = "Successfully Registered" });
                    else if (result == -1)
                        return BadRequest(new { result = 400, message = "The username has been taken" });
                }
            }
            return BadRequest();
        }


        //CHANGE PASSWORD
        [HttpPut("ChangePassword/{id}")]
        public IActionResult ChangePassword(int id, User user)
        {
            if (ModelState.IsValid)
            {
                var result = userRepository.ChangePassword(id, user);
                if (result > 0)
                    return Ok(new { result = 200, message = "Password Successfully Updated" });
                else if (result == -1)
                    return NotFound();
                return BadRequest();
            }
            return BadRequest();
        }


        //FORGOT PASSWORD
        [HttpPut("ForgotPassword")]
        public IActionResult ForgotPassword(User user)
        {
            if (ModelState.IsValid)
            {
                var result = userRepository.ForgotPassword(user);
                if (result > 0)
                    return Ok(new { result = 200, message = "Password Successfully Updated" });
                else if (result == -1)
                    return NotFound();
                return BadRequest();
            }
            return BadRequest();
        }
    }
}
