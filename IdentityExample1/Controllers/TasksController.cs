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

            int id = int.Parse(_userManager.GetUserId(User));

            var results = dal.GetTasksByMostRecent(id);

            ViewData["nameof(Tasks)"] = results;

            return View();
        }

        [HttpGet]
        public IActionResult ChangeStatus(int id)
        {
            Tasks t = dal.GetTasksById(id);

            int result = dal.FlipStatus(t);

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
            int result = dal.CreateTask(t);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(int id)
        {
            int result = dal.DeleteTaskById(id);

            return RedirectToAction("Index");
        }
    }
}