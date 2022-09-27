using API.Models;
using API.Repositories.Data;
using API.Services;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[AllowAnonymous]
    public class UserController : BaseController<UserRepository, User, int>
    {
        UserRepository userRepository;

        private IConfiguration config;

        public UserController(UserRepository userRepository, IConfiguration config) : base(userRepository)
        {
            this.userRepository = userRepository;
            this.config = config;
        }


        //LOGIN
        [HttpPost("Login")]
        public IActionResult Login(Login login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                return BadRequest();
            /*var check = userRepository.GetEmployee(login.Email);
            if (check == null)
            {
                return NotFound();
            }*/
            var data = userRepository.Login(login);
            if (data == null)
                return NotFound();
            var role = userRepository.GetRole(data.Id);
            var jwt = new JwtService(config);
            var token = jwt.GenerateSecurityToken(data.Id, data.Employee.Email, data.Employee.FirstName + " " + data.Employee.LastName, role.Role.Name);
            return Ok(new { result = 200, message = "Successfully Login", token = token });
        }


        //REGISTER
        [HttpPost("Register")]
        public IActionResult Register(Register register)
        {
            if (!string.IsNullOrWhiteSpace(register.Email) ||
                !string.IsNullOrWhiteSpace(register.FirstName) ||
                !string.IsNullOrWhiteSpace(register.LastName) ||
                !string.IsNullOrWhiteSpace(register.PhoneNumber) ||
                !string.IsNullOrWhiteSpace(register.Salary.ToString()) ||
                !string.IsNullOrWhiteSpace(register.Username) ||
                !string.IsNullOrWhiteSpace(register.Password))
            {
                if (ModelState.IsValid)
                {
                    var data = userRepository.Register(register);
                    var result = userRepository.GetUser(register.Email);
                    if (data > 0)
                        return Ok(new { result = 200, message = "Successfully Registered", data = new { Email = result.Employee.Email, Password = result.Password } } );
                    else if (data == -1)
                        return BadRequest(new { result = 400, message = "Email is already registered" });
                }
            }
            return BadRequest();
        }


        //CHANGE PASSWORD
        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            if (!string.IsNullOrWhiteSpace(changePassword.Email) || !string.IsNullOrWhiteSpace(changePassword.OldPassword) || !string.IsNullOrWhiteSpace(changePassword.NewPassword))
            {
                var data = userRepository.GetUser(changePassword.Email);
                if (data == null)
                    return NotFound();
                int result = userRepository.ChangePassword(changePassword);
                if (result > 0)
                    return Ok(new { status = 200, message = "Password Successfully Updated" });
                return BadRequest();
            }
            return BadRequest();
        }


        //FORGOT PASSWORD
        [HttpPut("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            /*if (ModelState.IsValid)
            {
                var result = userRepository.ForgotPassword(email, user);
                if (result > 0)
                    return Ok(new { result = 200, message = "Password Successfully Updated" });
                else if (result == -1)
                    return NotFound();
                return BadRequest();
            }
            return BadRequest();*/

            if (!string.IsNullOrWhiteSpace(forgotPassword.Email) && !string.IsNullOrWhiteSpace(forgotPassword.NewPassword))
            {
                var check = userRepository.GetUser(forgotPassword.Email);
                if (check == null)
                {
                    return NotFound();
                }
                int result = userRepository.ForgotPassword(forgotPassword);
                if (result > 0)
                    return Ok(new { status = 200, message = "Password Successfully Updated" });
                return BadRequest();
            }
            return BadRequest();
        }
    }
}
