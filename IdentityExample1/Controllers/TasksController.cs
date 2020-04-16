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

        public IActionResult ChangeStatus(Tasks t)
        {
            int result = dal.FlipStatus(t);

            return RedirectToAction("Index");
        }

        public IActionResult AddTask()
        {

            return View();
        }
    }
}