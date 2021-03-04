using h5todos.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace h5todos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TodosContext _todoscontext;
        private IDataProtector _provider;
        private readonly IConfiguration _config;


        public HomeController(ILogger<HomeController> logger, TodosContext todosContext,
            IDataProtectionProvider provider,
            IConfiguration configuration)
        {
            _todoscontext = todosContext;
            _logger = logger;
            _config = configuration;
            // laver en protector fra SecretKey
            _provider = provider.CreateProtector(_config["SecretKey"]);
        }
        [HttpGet]

        // get liste med value
        public IActionResult TodosList()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                return Redirect("/");
            }

            ViewBag.userId = userId;

            List<TodosItem> todos = _todoscontext.TodosItem.Where(t => t.loginId == userId).ToList();

            foreach (TodosItem todo in todos)
            {
                //unkryptere Title og Description så den vise på siden mens den er krypteret i databasen
                todo.Title = _provider.Unprotect(todo.Title);
                todo.Description = _provider.Unprotect(todo.Description);
            }
            ViewBag.Todos = todos;
            return View();
        }

        [HttpPost]
        //
        // Post Liste med value
        //
        public IActionResult TodosList(string itemTitle, string itemDescription)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                return Redirect("/");
            }
            ViewBag.userId = userId;

            _todoscontext.TodosItem.Add(new TodosItem
            {
                //kryptere Title og Description i databasen
                Title = _provider.Protect(itemTitle),
                Description = _provider.Protect(itemDescription),
                Added = DateTime.Now,
                loginId = (int)userId
            });

            _todoscontext.SaveChanges();
            return Redirect("/Home/TodosList");
            //return View();

        }

        [HttpGet]
        //
        // delete items
        //
        public IActionResult DeleteTodos(int id)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                return Redirect("/");
            }

            var todosItem = _todoscontext.TodosItem.SingleOrDefault(t => t.Id == id && t.loginId == userId);
            if (todosItem == null) return Redirect("/Home/TodosList");

            _todoscontext.TodosItem.Remove(todosItem);
            _todoscontext.SaveChanges();

            return Redirect("/Home/TodosList");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //
        // Log ind med bruger
        //
        public IActionResult Index(string username, string password)
        {
            Login login = _todoscontext.Login.SingleOrDefault(l => l.Username == username);
            if (login != null)
            {
                if (BC.Verify(password, login.Password))
                {
                    //Er logget ind
                    HttpContext.Session.SetInt32("userId", login.Id);
                    ViewBag.Message = "Du er logget på";
                    return Redirect("/Home/TodosList");
                }
                else ViewBag.Message = "Passwordet er forkert"; //Ingen bruger med det brugernavn
            }
            else ViewBag.Message = "Ingen bruger med det brugernavn"; //Ingen bruger med det brugernavn

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //
        // Opretter bruger
        //
        public IActionResult Register(string username, string email, string password)
        {
            //var knownLogin = _todoscontext.Login.SingleOrDefault()
           Login knownLogin = _todoscontext.Login.SingleOrDefault(l => l.Username == username);
            if (knownLogin == null)
            {
                Login login = new Login
                {
                    Username = username,
                    Email = email,
                    Password = BC.HashPassword (password)
                };

                _todoscontext.Login.Add(login);
                _todoscontext.SaveChanges();
                ViewBag.Message = "Bruger er nu oprettet";
            }
            return View();
        }

        

        








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
