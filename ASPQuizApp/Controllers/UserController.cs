using ASPQuizApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using QuizLib.Data;
using QuizLib.Logic;
using QuizLib.Interface;

namespace ASPQuizApp.Controllers
{
    public class UserController : Controller
    {
        GebruikerContainer gc = new GebruikerContainer(DAOS.gebruikerDao);

        public IActionResult Overzicht()
        {
            GebruikerLijstViewModel model = new GebruikerLijstViewModel();
            foreach(Gebruiker g in gc.GetAll())
            {
                model.GebruikerDetailsLijst.Add(new GebruikerDetailsViewModel((int)g.Id, g.Naam, g.Email, g.IsAdmin));
            }
            return View(model);
        }

        public IActionResult Details(GebruikerDetailsViewModel gebr)
        {
            Gebruiker g = gc.GetById(gebr.Id);
            GebruikerDetailsViewModel gebruikerModel = new GebruikerDetailsViewModel(gebr.Id, g.Naam, g.Email, g.IsAdmin);

            return View(gebruikerModel);
        }

        [HttpPost]
        public void Delete()
        {
            gc.Delete(Convert.ToInt32(Request.Form["txtId"]));
            Response.Redirect("Overzicht");
        }

        [HttpPost]
        public void Promote()
        {
            gc.PromoteUser(Convert.ToInt32(Request.Form["txtId"]));
            Response.Redirect("Overzicht");
        }

        [HttpPost]
        public void Demote()
        {
            gc.DemoteUser(Convert.ToInt32(Request.Form["txtId"]));
            Response.Redirect("Overzicht");
        }
    }
}
