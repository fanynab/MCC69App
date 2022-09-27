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
    public class EmployeeController : BaseController<Employee, EmployeeRepository>
    {
        DepartmentRepository departmentRepository;
        JobRepository jobRepository;

        public EmployeeController(EmployeeRepository employeeRepository, DepartmentRepository departmentRepository, JobRepository jobRepository) : base(employeeRepository)
        {
            this.departmentRepository = departmentRepository;
            this.jobRepository = jobRepository;
        }

        public async Task<IActionResult> Index()
        {
            var employee = await Get();
            return View(employee.AsEnumerable());
        }


        //CREATE
        public async Task<IActionResult> Create()
        {
            var department = await departmentRepository.Get();
            var job = await jobRepository.Get();
            var employee = await Get();
            ViewData["Department_Id"] = new SelectList(department.AsEnumerable(), "Id", "Name");
            ViewData["Job_Id"] = new SelectList(job.AsEnumerable(), "Id", "JobTitle");
            ViewData["Manager_Id"] = new SelectList(employee.AsEnumerable(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            var result = Post(employee);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }


        //EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var result = await Get(id);
            var department = await departmentRepository.Get();
            var job = await jobRepository.Get();
            var employee = await Get();
            ViewData["Department_Id"] = new SelectList(department.AsEnumerable(), "Id", "Id", result.Department_Id);
            ViewData["Job_Id"] = new SelectList(job.AsEnumerable(), "Id", "Id", result.Job_Id);
            ViewData["Manager_Id"] = new SelectList(employee.AsEnumerable(), "Id", "Id", result.Manager_Id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            var result = Put(id, employee);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }


        //DELETE
        public IActionResult Delete(int id)
        {
            var result = Get(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee)
        {
            var result = DeleteEntity(employee);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
    }
}
