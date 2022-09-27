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
    public class DepartmentController : BaseController<Department, DepartmentRepository>
    {
        LocationRepository locationRepository;

        public DepartmentController(DepartmentRepository departmentRepository, LocationRepository locationRepository) : base(departmentRepository)
        {
            this.locationRepository = locationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var department = await Get();
            return View(department.AsEnumerable());
        }


        //CREATE
        public async Task<IActionResult> Create()
        {
            var location = await locationRepository.Get();
            ViewBag.Location = new SelectList(location.AsEnumerable(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            var result = Post(department);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }


        //EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var location = await locationRepository.Get();
            ViewBag.Location = new SelectList(location.AsEnumerable(), "Id", "Name");
            var result = await Get(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Department department)
        {
            var result = Put(id, department);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }


        //DELETE
        public IActionResult Delete(int id)
        {
            var result = Get(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department)
        {
            var result = DeleteEntity(department);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
    }
}
