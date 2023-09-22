using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Threading.Tasks;
using TaskManagerApps.Data;
using TaskManagerApps.Models;

namespace TaskManagerApps.Controllers
{
    public class TasksController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TasksController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public IActionResult Create(int id)
        {
            ViewBag.UserId = id;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewTasks()
        {
            TMContext _tmContext = new TMContext();
            var userId = _contextAccessor.HttpContext.Session.GetInt32("UserId");
            var listTasks = await _tmContext.Tasks.Where(x => x.UserId == userId).OrderBy(x => x.TaskId).ToListAsync();
            return View(listTasks);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            TMContext _tmContext = new TMContext();
            var task = await _tmContext.Tasks.FirstOrDefaultAsync(x => x.TaskId == id);

            if(task != null)
            {
                var editModel = new Tasks()
                {
                    TaskId = id,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                };

                return View(task);
            }

            return RedirectToAction("ViewTasks");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Tasks taskmodel)
        {
            TMContext _tmContext = new TMContext();
            var task = await _tmContext.Tasks.FindAsync(taskmodel.TaskId);

            if (task != null)
            {
                task.TaskId = taskmodel.TaskId;
                task.Title = taskmodel.Title;
                task.Description = taskmodel.Description;
                task.DueDate = taskmodel.DueDate;
                task.IsCompleted = taskmodel.IsCompleted;

                await _tmContext.SaveChangesAsync();

                return RedirectToAction("ViewTasks");
            }
            return RedirectToAction("ViewTasks");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Tasks taskmodel)
        {
            TMContext _tmContext = new TMContext();
            var task = await _tmContext.Tasks.FindAsync(taskmodel.TaskId);

            if (task != null)
            {
                _tmContext.Tasks.Remove(task);
                await _tmContext.SaveChangesAsync();
            }
            return RedirectToAction("ViewTasks");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTasks(Tasks _tasks)
        {
            TMContext _tmContext = new TMContext();
            var task = new Tasks()
            {
                Title = _tasks.Title,
                Description = _tasks.Description,
                DueDate = _tasks.DueDate,
                UserId = _tasks.UserId,
                IsCompleted = _tasks.IsCompleted,
            };

            await _tmContext.AddAsync(task);
            await _tmContext.SaveChangesAsync();
            return RedirectToAction("ViewTasks");
        }
    }
}
