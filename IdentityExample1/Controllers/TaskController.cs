using IdentityExample1.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Identity.Dapper.Entities;
using IdentityExample1.Models;

namespace IdentityExample1.Controllers
{
    //[Authorize]
    public class TaskController : Controller
    {
        private readonly UserManager<DapperIdentityUser> _userManager;
        private readonly SignInManager<DapperIdentityUser> _signInManager;
        private readonly ILogger _logger;
        private DAL dal;

        public TaskController(
            UserManager<DapperIdentityUser> userManager,
            SignInManager<DapperIdentityUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewData["Name"] = User.Identity.Name;
            ViewData["UID"] = _userManager.GetUserId(User);
            return View();
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            ViewData["Name"] = User.Identity.Name;
            ViewData["UID"] = _userManager.GetUserId(User);
            return View(new UserTask());
        }

        [HttpPost]
        public IActionResult CreateTask(UserTask t)
        {
            //t.OwnerId = int.Parse(_userManager.GetUserId(User));
            int result = dal.CreateTask(t);
            //add to the database here
            return RedirectToAction("Index");
        }
    }
}