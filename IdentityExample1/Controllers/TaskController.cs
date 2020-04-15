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
    public class TaskController : Controller
    {
        private DAL dal;

        private readonly UserManager<DapperIdentityUser> _userManager;
        private readonly SignInManager<DapperIdentityUser> _signInManager;
        private readonly ILogger _logger;

        public TaskController(IConfiguration config, UserManager<DapperIdentityUser> userManager,
            SignInManager<DapperIdentityUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            dal = new DAL(config.GetConnectionString("default"));
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        public IActionResult Index()
        {
            //string userId = _userManager.GetUserId(User);
            ViewData["UID"] = _userManager.GetUserId(User);
            //ViewData["Tasks"] = dal.GetTasksByMostRecent(userId);
            return View();
        }

        public IActionResult AddTask()
        {

            return View();
        }
    }
}