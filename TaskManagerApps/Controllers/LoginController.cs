using Microsoft.AspNetCore.Mvc;
using TaskManagerApps.Data;
using TaskManagerApps.Models;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagerApps.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Users user = new Users();
            return View();
        }
        [HttpPost]
        public IActionResult Index(Users _user)
        {
            TMContext _context = new TMContext();
            var status = _context.Users.Where(x => x.UserName == _user.UserName && x.PasswordHash == _user.PasswordHash).FirstOrDefault();
            if (status == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                _contextAccessor.HttpContext.Session.SetString("UserName", status.UserName);
                _contextAccessor.HttpContext.Session.SetInt32("UserId", status.UserId);
                ViewBag.UserId = status.UserId;
                ViewBag.UserName = status.UserName;
                return RedirectToAction("HomePage", "Login", new { id = status.UserId});
            }
            return View(_user);
        }
        public IActionResult HomePage(int id)
        {
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            _contextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
