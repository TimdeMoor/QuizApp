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
    public class GameController : Controller
    {
        VraagContainer vc = new VraagContainer(DAOS.vraagDao);
        AntwoordContainer ac = new AntwoordContainer(DAOS.antwoordDao);
        SubCategorieContainer scc = new SubCategorieContainer(DAOS.subCategorieDao);

        Random r = new Random();

        [HttpPost]
        public IActionResult StartGame()
        {
            int aantalVragen = Convert.ToInt32(Request.Form["txtAantalVragen"]);

            List<int> SelectedCategories = new List<int>();
            foreach(string key in Request.Form.Keys){ 
                if (key.StartsWith("C")){
                    SelectedCategories.Add(Convert.ToInt32(key.Remove(0, 2)));
                }
            }
            QuizStatusViewModel quiz = GenerateQuiz(aantalVragen, SelectedCategories);
            quiz.VraagNummer = 1;

            return View("QuizGame", quiz);
        }

        [HttpPost]
        public IActionResult CheckAnswers()
        {
            int score = 0;
            int aantalVragen = 0;

            List<string> vragen = new List<string>();
            List<string> correcteAntwoorden = new List<string>();
            List<string> geselecteerdeAntwoorden = new List<string>();

            foreach(string key in Request.Form.Keys)
            {
                int SelectedAntwoordId = Convert.ToInt32(Request.Form[key]);
                int CorrectAntwoordId = ac.GetCorrectAntwoordIdBySelectedAntwoordId(SelectedAntwoordId); 

                if (SelectedAntwoordId == CorrectAntwoordId)
                {
                    score++;
                }
                aantalVragen++;

                correcteAntwoorden.Add(ac.GetById(CorrectAntwoordId).Text);
                geselecteerdeAntwoorden.Add(ac.GetById(SelectedAntwoordId).Text);
                vragen.Add(vc.GetByAntwoordId(CorrectAntwoordId).Text);
            }

            QuizCompleteViewModel model = new QuizCompleteViewModel() {
                AantalVragen = aantalVragen,
                CorrecteAntwoorden = correcteAntwoorden,
                Score = score,
                SelectedAntwoorden = geselecteerdeAntwoorden,
                Vragen = vragen
            };

            return View("QuizComplete", model);
        }

        private QuizStatusViewModel GenerateQuiz(int aantalVragen, List<int> CategorieIds)
        {
            QuizStatusViewModel quiz = new QuizStatusViewModel();
            quiz.Quizvragen = new List<GameVraagViewModel>();
            List<Vraag> vragenLijst = new List<Vraag>();

            Queue<int> categorieIdsRemaining = new Queue<int>(CategorieIds);
            List<int> categorieIdsChecked = CategorieIds;

            while(categorieIdsRemaining.Count != 0)
            {
                List<SubCategorie> temp = scc.GetChildrenbyParentId(categorieIdsRemaining.Dequeue());
                if(temp.Count != 0)
                {
                    foreach(SubCategorie sc in temp)
                    {
                        categorieIdsRemaining.Enqueue((int)sc.Id);
                        categorieIdsChecked.Add((int)sc.Id);
                    }
                }
            }

            if(categorieIdsChecked.Count == 0)
            {
                vragenLijst = vc.GetAll();
            }
            else
            {
                foreach(int categorieId in categorieIdsChecked)
                {
                    foreach(Vraag v in vc.GetByCategorieId(categorieId))
                    {
                        vragenLijst.Add(v);
                    }
                }
            }

            for (int i = 0; i < aantalVragen; i++)
            {
                Vraag v = vragenLijst[r.Next(0, vragenLijst.Count())];
                quiz.Quizvragen.Add(new GameVraagViewModel()
                {
                    Vraag = v,
                    Antwoorden = ac.GetMogelijkeAntwoorden((int)v.Id, true),
                    Categorie = scc.GetByVraagId((int)v.Id),
                });
            }
            
            return quiz;
        }
    }
}
