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
    public class AntwoordController : Controller
    {
        AntwoordContainer ac = new AntwoordContainer(DAOS.antwoordDao);
        VraagContainer vc = new VraagContainer(DAOS.vraagDao);
        SubCategorieContainer scc = new SubCategorieContainer(DAOS.subCategorieDao);

        [HttpPost]
        public void Delete()
        {
            int antwoordId = Convert.ToInt32(Request.Form["txtAntwoordId"]);
            int vraagId = Convert.ToInt32(Request.Form["txtVraagId"]);
            ac.Delete(new AntwoordDTO() { Id = antwoordId });

            Vraag v = vc.GetById(vraagId);

            Response.Redirect(Url.Action("Details", "Vraag", new VraagDetailsViewModel()
            {
                Id = vraagId,
                MogelijkeAntwoorden = ac.GetMogelijkeAntwoorden(vraagId),
                SubCategorieNaam = scc.GetByVraagId((int)v.SubCategorieId).Naam,
                Text = v.Text,
            }));
        }

        [HttpPost]
        public void Add()
        {
            Antwoord a = new Antwoord(new AntwoordDTO() { 
                IsCorrect = false,
                Text = Request.Form["txtAntwoordText"].ToString(),
                VraagId = Convert.ToInt32(Request.Form["VraagId"]),
            });

            a.Save();

            Vraag v = vc.GetById(a.VraagId);
            Response.Redirect(Url.Action("Details", "Vraag", new VraagDetailsViewModel() { 
                Id = a.VraagId,
                MogelijkeAntwoorden = ac.GetMogelijkeAntwoorden(a.VraagId),
                SubCategorieNaam = scc.GetByVraagId((int)v.SubCategorieId).Naam,
                Text = v.Text,
            }));
        }

        [HttpPost]
        public void SetCorrect()
        {
            int antwoordId = Convert.ToInt32(Request.Form["txtAntwoordId"]);
            int vraagId = Convert.ToInt32(Request.Form["txtVraagId"]);

            Vraag v = vc.GetById(vraagId);

            ac.SetIncorrect((int)ac.GetCorrectAntwoordByVraagId((int)v.Id).Id); 
            ac.SetCorrect(antwoordId);

            Response.Redirect(Url.Action("Details", "Vraag", new VraagDetailsViewModel()
            {
                Id = vraagId,
                MogelijkeAntwoorden = ac.GetMogelijkeAntwoorden(vraagId),
                SubCategorieNaam = scc.GetByVraagId((int)v.SubCategorieId).Naam,
                Text = v.Text,
            }));
        }
    }
}
