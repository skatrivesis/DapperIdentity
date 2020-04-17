using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityExample1.Models;
using Microsoft.Extensions.Configuration;
using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityExample1.Controllers
{
    public class TasksController : Controller
    {
        private DAL dal;

        private readonly UserManager<DapperIdentityUser> _userManager;
        private readonly SignInManager<DapperIdentityUser> _signInManager;
        private readonly ILogger _logger;

        public TasksController(IConfiguration config, UserManager<DapperIdentityUser> userManager,
            SignInManager<DapperIdentityUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            dal = new DAL(config.GetConnectionString("DefaultConnection"));
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        public IActionResult Index()
        {
            ViewData["UID"] = _userManager.GetUserId(User);

            var id = int.Parse(_userManager.GetUserId(User));

            var results = dal.GetTasksByMostRecent(id);

            ViewData["nameof(Tasks)"] = results;

            return View();
        }
        public IActionResult SortByStatusASC()
        {
            ViewData["UID"] = _userManager.GetUserId(User);

            var id = int.Parse(_userManager.GetUserId(User));

            var results = dal.GetTasksBySortASC(id);

            ViewData["nameof(Tasks)"] = results;

            return View();
        }
        public IActionResult SortByStatusDESC()
        {
            ViewData["UID"] = _userManager.GetUserId(User);

            var id = int.Parse(_userManager.GetUserId(User));

            var results = dal.GetTasksBySortDESC(id);

            ViewData["nameof(Tasks)"] = results;

            return View();
        }

        [HttpGet]
        public IActionResult ChangeStatus(int id)
        {
            var t = dal.GetTasksById(id);

            var result = dal.FlipStatus(t);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddTaskForm()
        {
            ViewData["UID"] = _userManager.GetUserId(User);
            return View(new Tasks());
        }

        [HttpPost]
        public IActionResult AddTaskDB(Tasks t)
        {
            t.UserId = int.Parse(_userManager.GetUserId(User));
            var result = dal.CreateTask(t);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(int id)
        {
            var result = dal.DeleteTaskById(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditTaskForm(int id)
        {
            var task = dal.GetTasksById(id);

            return View(task);
        }

        [HttpPost]
        public IActionResult EditTaskDB(Tasks t)
        {
            var result = dal.EditTask(t);

            return RedirectToAction("Index");
        }

        public IActionResult Search(string search)
        {
            var results = dal.Search(search);

            ViewData["nameof(Search)"] = search;
            ViewData["Search Results"] = results;

            return View();
        }
    }
}