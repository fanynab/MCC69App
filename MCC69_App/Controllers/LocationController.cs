using API.Models;
using MCC69_App.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MCC69_App.Controllers
{
    public class LocationController : BaseController<Location, LocationRepository>
    {
        CountryRepository countryRepository;

        public LocationController(LocationRepository locationRepository, CountryRepository countryRepository) : base(locationRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var location = await Get();
            return View(location.AsEnumerable());
        }


        //CREATE
        public async Task<IActionResult> Create()
        {
            var country = await countryRepository.Get();
            ViewBag.Country = new SelectList(country.AsEnumerable(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Location location)
        {
            var result = Post(location);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }


        //EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var country = await countryRepository.Get();
            ViewBag.Country = new SelectList(country.AsEnumerable(), "Id", "Name");
            var result = await Get(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Location location)
        {
            var result = Put(id, location);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }


        //DELETE
        public IActionResult Delete(int id)
        {
            var result = Get(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Location location)
        {
            var result = DeleteEntity(location);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }
    }
}
