using ASPQuizApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using QuizLib.Logic;
using QuizLib.Interface;

namespace ASPQuizApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        GebruikerContainer gc = new GebruikerContainer(DAOS.gebruikerDao);
        SubCategorieContainer scc = new SubCategorieContainer(DAOS.subCategorieDao);

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            SubCategorieLijstViewModel model = new SubCategorieLijstViewModel();
            foreach(SubCategorie sc in scc.GetAll())
            {
                model.SubCategorieLijst.Add(new SubCategorieDetailsViewModel() { 
                    Id = (int)sc.Id,
                    Beschrijving = sc.Beschrijving,
                    Naam = sc.Naam,                    
                });
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginUser()
        {
            string name = Request.Form["Name"];
            string pass = Request.Form["Pass"];

            if (name == "Admin" && pass == "Admin")
            {
                SessionHelper.SetSessionString(HttpContext.Session, "Admin", "true");
            }

            SubCategorieLijstViewModel model = new SubCategorieLijstViewModel();
            foreach (SubCategorie sc in scc.GetAll())
            {
                model.SubCategorieLijst.Add(new SubCategorieDetailsViewModel()
                {
                    Id = (int)sc.Id,
                    Beschrijving = sc.Beschrijving,
                    Naam = sc.Naam,
                });
            }

            return View("Index", model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveUser()
        {
            //TODO: Check if username exists
            string name = Request.Form["Name"];
            string pass = Request.Form["Pass"];
            string pass2 = Request.Form["Pass2"];
            string mail = Request.Form["EMail"];

            if (pass == pass2)
            {
                Gebruiker g = new Gebruiker(new GebruikerDTO()
                {
                    Naam = name,
                    Wachtwoord = pass,
                    Email = mail,
                    IsAdmin = false
                });

                g.Save();
            }

            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Start()
        {
            return View();
        }
    }
}
