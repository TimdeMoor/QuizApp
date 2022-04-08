using ASPQuizApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using QuizLib.Logic;
using QuizLib.Interface;
using QuizLib.Data;


namespace ASPQuizApp.Controllers
{
    public class VraagController : Controller
    {
        VraagContainer vc = new VraagContainer(DAOS.vraagDao);
        AntwoordContainer ac = new AntwoordContainer(DAOS.antwoordDao);
        SubCategorieContainer sc = new SubCategorieContainer(DAOS.subCategorieDao);

        public IActionResult Overzicht()
        {
            VragenLijstViewModel vragenViewModels = new VragenLijstViewModel();
            
            List<Vraag> vragenlijst = vc.GetAll();

            foreach (Vraag vraag in vragenlijst)
            {
                int vraagId = (int)vraag.Id;
                VraagDetailsViewModel vraagDetails = new VraagDetailsViewModel(
                    vraagId, 
                    vraag.Text, 
                    sc.GetByVraagId(vraagId).Naam, 
                    ac.GetMogelijkeAntwoorden(vraagId));

                vragenViewModels.Vragen.Add(vraagDetails);
            }

            return View(vragenViewModels);
        }
        

        public IActionResult Details(VraagDetailsViewModel vraag)
        {
            VraagDetailsViewModel vraagDetails = new VraagDetailsViewModel(
                    vraag.Id,
                    vc.GetById(vraag.Id).Text,
                    sc.GetByVraagId(vraag.Id).Naam,
                    ac.GetMogelijkeAntwoorden(vraag.Id));
            return View(vraagDetails);
        }

        public IActionResult Add()
        {
            AddVraagViewModel model = new AddVraagViewModel();
            model.SubCategories = sc.GetAll();
            return View(model);
        }

        [HttpPost]
        public void Create()
        {
            string vraagText = Request.Form["txtVraag"];
            int subCategorieId = Convert.ToInt32(Request.Form["cmbCategorie"]);

            Vraag v = new Vraag(new VraagDTO(){ Text = vraagText, SubCategorieId = subCategorieId });
            
            v.Save(); //push to database to generate id
            v = vc.GetByText(vraagText); //and retrieve back from database

            string correctAntwoord = Request.Form["txtCorrectAntwoord"];
            string[] fouteAntwoorden = Request.Form["txtFoutAntwoord[]"];

            int vraagId = (int)v.Id;

            new Antwoord(new AntwoordDTO() { Text = correctAntwoord, VraagId = vraagId, IsCorrect = true }).Save();
            foreach (string foutAntwoord in fouteAntwoorden)
            {
                new Antwoord(new AntwoordDTO() { Text = foutAntwoord, VraagId = vraagId, IsCorrect = false }).Save();
            }
           
            Response.Redirect("Overzicht");
        }

        [HttpPost]
        public void Delete()
        {
            vc.Delete(Convert.ToInt32(Request.Form["txtId"]));
            Response.Redirect("Overzicht");
        }

        [HttpPost]
        public IActionResult ChangeText()
        {
            int id = Convert.ToInt32(Request.Form["VraagId"]);
            string text = Request.Form["txtVraagText"];
            vc.ChangeVraagText(id, text);

            Vraag v = vc.GetById(id);
            VraagDetailsViewModel vraagDetails = new VraagDetailsViewModel(
                     (int)v.Id,
                     vc.GetById((int)v.Id).Text,
                     sc.GetByVraagId((int)v.Id).Naam,
                     ac.GetMogelijkeAntwoorden((int)v.Id));
            return View("Details", vraagDetails);
        }
    }
}
