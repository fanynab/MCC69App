using API.Models;
using MCC69_App.Repositories.Data;
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
    public class JobHistoryController : BaseController<JobHistory, JobHistoryRepository>
    {
        public JobHistoryController(JobHistoryRepository jobHistoryRepository) : base(jobHistoryRepository)
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }


        /*public async Task<IActionResult> Index()
        {
            var jobHistory = await Get();
            return View(jobHistory.AsEnumerable());
        }*/

        //CREATE
        /*public async Task<IActionResult> Create()
        {
            var department = await departmentRepository.GetAll();
            var employee = await employeeRepository.GetAll();
            var job = await jobRepository.GetAll();
            ViewData["Department_Id"] = new SelectList(department.AsEnumerable(), "Id", "Name");
            ViewData["Id"] = new SelectList(employee.AsEnumerable(), "Id", "Id");
            ViewData["Job_Id"] = new SelectList(job.AsEnumerable(), "Id", "JobTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JobHistory jobHistory)
        {
            var result = Post(jobHistory);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(jobHistory);
        }


        //EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var result = await Get(id);
            var department = await departmentRepository.GetAll();
            var job = await jobRepository.GetAll();
            //var employee = await Get();
            ViewData["Department_Id"] = new SelectList(department.AsEnumerable(), "Id", "Id", result.Department_Id);
            //ViewData["Id"] = new SelectList(httpAPIEmployees.Get().ToList(), "Id", "Id", jobHistory.Id);
            ViewData["Job_Id"] = new SelectList(job.AsEnumerable(), "Id", "Id", result.Job_Id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, JobHistory jobHistory)
        {
            var result = Put(id, jobHistory);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(jobHistory);
        }


        //DELETE
        public IActionResult Delete(int id)
        {
            var result = Get(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(JobHistory jobHistory)
        {
            var result = DeleteEntity(jobHistory);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(jobHistory);
        }*/
    }
}
