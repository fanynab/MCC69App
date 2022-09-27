using API.ViewModel;
using MCC69_App.Models;
using MCC69_App.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MCC69_App.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            LoginResult loginResult = new LoginResult();
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsJsonAsync("https://localhost:44382/api/User/Login", login))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    loginResult = JsonConvert.DeserializeObject<LoginResult>(apiResponse);
                    HttpContext.Session.SetString("Token", loginResult.token);
                    if (loginResult.result == 200)
                    {
                        return View("Success");
                    }
                    return View("Index");
                }
            }
        }
    }
}
