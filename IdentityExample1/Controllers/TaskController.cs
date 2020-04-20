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
using Microsoft.Extensions.Configuration;

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
            ILoggerFactory loggerFactory,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            dal = new DAL(config.GetConnectionString("DefaultConnection"));
        }

        //[Authorize]
        public IActionResult Index()
        {
            ViewData["Name"] = User.Identity.Name;
            ViewData["OwnerId"] = _userManager.GetUserId(User);
            IEnumerable<UserTask> usertasks = dal.GetTasks(_userManager.GetUserId(User));
            ViewData["UserTasks"] = usertasks;
            return View();
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            ViewData["Name"] = User.Identity.Name;
            ViewData["OwnerId"] = _userManager.GetUserId(User);
            return View(new UserTask());
        }

        [HttpPost]
        public IActionResult CreateTask(UserTask t)
        {
            int result = dal.CreateTask(t);

            return View("../Home/Index");
        }

        public IActionResult MarkAsComplete(UserTask t)
        {
            int result = dal.MarkAsComplete(t);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(UserTask t)
        {
            int result = dal.DeleteTask(t);

            return RedirectToAction("Index");
        }

        public IActionResult SearchTask(string search)
        {
            string id = _userManager.GetUserId(User);

            IEnumerable<UserTask> results = dal.SearchTask(search, id);

            ViewData["UserTasks"] = results;

            return View();
        }

        public IActionResult GetTasksCompleted()
        {
            string id = _userManager.GetUserId(User);

            IEnumerable<UserTask> results = dal.GetTasksCompleted(id);

            ViewData["UserTasks"] = results;

            return View();
        }

        public IActionResult GetTasksNotCompleted()
        {
            string id = _userManager.GetUserId(User);

            IEnumerable<UserTask> results = dal.GetTasksNotCompleted(id);

            ViewData["UserTasks"] = results;

            return View();
        }

        public IActionResult SortTasksByDueDate()
        {
            string id = _userManager.GetUserId(User);

            IEnumerable<UserTask> results = dal.SortTasksByDueDate(id);

            ViewData["UserTasks"] = results;

            return View();
        }

        public IActionResult SortTasksByDueDateDESC()
        {
            string id = _userManager.GetUserId(User);

            IEnumerable<UserTask> results = dal.SortTasksByDueDateDESC(id);

            ViewData["UserTasks"] = results;

            return View();
        }


        [HttpGet]
        public IActionResult EditTask(int id)
        {
            UserTask t = dal.GetTaskById(id);

            return View(t);
        }

        [HttpPost]
        public IActionResult EditTask(UserTask t)
        {
            int result = dal.EditTaskById(t);

            return RedirectToAction("Index");
        }
    }
}