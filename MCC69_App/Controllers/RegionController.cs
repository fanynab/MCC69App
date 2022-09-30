using API.Models;
using MCC69_App.Repositories.Data;
using MCC69_App.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MCC69_App.Controllers
{
    [Authorize]
    public class RegionController : BaseController<Region, RegionRepository>
    {
        public RegionController(RegionRepository repository) : base(repository)
        {

        }

        /*public async Task<IActionResult> Index()
        {
            var region = await GetAll();
            return View(region.AsEnumerable());
        }*/
        public IActionResult Index()
        {
            return View();
        }


        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Region region)
        {
            var result = Post(region);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }


        //EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var result = await Get(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Region region)
        {
            var result = Put(id, region);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }


        //DELETE
        public IActionResult Delete(int id)
        {
            var result = Get(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Region region)
        {
            var result = DeleteEntity(region);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }
    }
}
