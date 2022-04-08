using ASPQuizApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizLib.Logic;
using QuizLib.Interface;
using QuizLib.Data;

namespace ASPQuizApp.Controllers
{
    public class SubCategorieController : Controller
    {
        SubCategorieContainer scc = new SubCategorieContainer(DAOS.subCategorieDao);

        public IActionResult Overzicht()
        {
            SubCategorieLijstViewModel model = new SubCategorieLijstViewModel();

            foreach (SubCategorie sc in scc.GetAll())
            {
                SubCategorieDetailsViewModel vm = new SubCategorieDetailsViewModel();

                vm.Id = (int)sc.Id;
                vm.Beschrijving = sc.Beschrijving;           
                vm.Naam = sc.Naam;

                SubCategorie parent = scc.GetParent((int)sc.Id);

                if(parent == null)
                {
                    vm.ParentSubCategorie = null;
                }
                else
                {
                    vm.ParentSubCategorie = parent.Naam;
                }

                model.SubCategorieLijst.Add(vm);
            }

            return View(model);
        }

        public IActionResult Details(SubCategorieDetailsViewModel SubCategorie)
        {
            SubCategorie s = scc.GetById(SubCategorie.Id);
            SubCategorieDetailsViewModel SubCategorieDetails = new SubCategorieDetailsViewModel()
            {
                Id = SubCategorie.Id,
                Naam = s.Naam,
                Beschrijving = s.Beschrijving
            };

            if (s.ParentSubCategorieId != null)
            {
                SubCategorieDetails.ParentSubCategorie = scc.GetById((int)s.ParentSubCategorieId).Naam;
            }
            else
            {
                SubCategorieDetails.ParentSubCategorie = "GEEN";
            }

            return View(SubCategorieDetails);
        }

        public IActionResult Add()
        {
            AddSubCategorieViewModel model = new AddSubCategorieViewModel();
            model.SubCategories = scc.GetAll();
            return View(model);
        }

        [HttpPost]
        public void Create()
        {
            string Naam = Request.Form["txtNaam"];
            string Beschrijving = Request.Form["txtBeschrijving"];
            int subCategorieId;
            SubCategorie sc;

            if (Request.Form["cmbCategorie"] != string.Empty)
            {
                subCategorieId = Convert.ToInt32(Request.Form["cmbCategorie"]);
                sc = new SubCategorie(new SubCategorieDTO() { Naam = Naam, Beschrijving = Beschrijving, ParentSubCategorieId = subCategorieId });
            }
            else
            {
                sc = new SubCategorie(new SubCategorieDTO() { Naam = Naam, Beschrijving = Beschrijving});
            }

            sc.Save();

            Response.Redirect("Overzicht");
        }

        [HttpPost]
        public void Delete()
        {
            scc.Delete(Convert.ToInt32(Request.Form["txtId"]));
            Response.Redirect("Overzicht");
        }
    }
}
