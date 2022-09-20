using MCC69_App.Models;
using MCC69_App.ViewModel;
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
    public class RegionController : Controller
    {
        //GET ALL
        public async Task<IActionResult> Index()
        {
            Json<Region> regionList = new Json<Region>();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:44382/api/Region"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    regionList = JsonConvert.DeserializeObject<Json<Region>>(apiResponse);
                }
            }
            return View(regionList.data);
        }


        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Region region)
        {
            Json<Region> regionList = new Json<Region>();
            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsJsonAsync("https://localhost:44382/api/Region", region))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    regionList = JsonConvert.DeserializeObject<Json<Region>>(apiResponse);
                    if (regionList.result == 200)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(region);
        }


        //EDIT
        public async Task<IActionResult> Edit(int id)
        {
            Results<Region> regionList = new Results<Region>();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:44382/api/Region/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    regionList = JsonConvert.DeserializeObject<Results<Region>>(apiResponse);
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Region region)
        {
            Json<Region> regionList = new Json<Region>();
            using (var client = new HttpClient())
            {
                using (var response = await client.PutAsJsonAsync("https://localhost:44382/api/Region/" + id.ToString(), region))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    regionList = JsonConvert.DeserializeObject<Json<Region>>(apiResponse);
                    if (regionList.result == 200)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(region);
        }
    }
}
