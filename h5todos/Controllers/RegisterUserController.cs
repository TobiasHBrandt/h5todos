using h5todos.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace h5todos.Controllers
{
    

    public class RegisterUserController : Controller
    {

        //private readonly ILogger<RegisterUserController> _logger;
        //private readonly TodosContext _todoscontext;
        //private IDataProtector _provider;
        //private readonly IConfiguration _config;


        //public RegisterUserController(ILogger<RegisterUserController> logger, TodosContext todosContext,
        //    IDataProtectionProvider provider,
        //    IConfiguration configuration)
        //{
        //    _todoscontext = todosContext;
        //    _logger = logger;
        //    _config = configuration;
        //    // laver en protector fra SecretKey
        //    _provider = provider.CreateProtector(_config["SecretKey"]);
        //}
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpPost]
        ////
        //// Opretter bruger
        ////
        //public IActionResult Register(string username, string email, string password)
        //{
        //    //var knownLogin = _todoscontext.Login.SingleOrDefault()
        //    Login knownLogin = _todoscontext.Login.SingleOrDefault(l => l.Username == username);
        //    if (knownLogin == null)
        //    {
        //        Login login = new Login
        //        {
        //            Username = username,
        //            Email = email,
        //            Password = BC.HashPassword(password)
        //        };

        //        _todoscontext.Login.Add(login);
        //        _todoscontext.SaveChanges();
        //        ViewBag.Message = "Bruger er nu oprettet";
        //    }
        //    return View();
        //}
    }
}
