using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Data;
using QuizLib.Interface;
using QuizLib.Logic;
using UnitTestProject;

namespace UnitTestsProject
{
    public class SubCategorieStub : ISubCategorieDAO, ISubCategorieContainerDAO
    {
        public List<SubCategorieDTO> CategorieLijst = new List<SubCategorieDTO>()
        {
            new SubCategorieDTO()
            {
                Id = 1,
                Naam = "TestCategorie1",
                IsActive = true,
                ParentSubCategorieId = null,
                Beschrijving = "Dit is TestCategorie1"
            },

            new SubCategorieDTO()
            {
                Id = 2,
                Naam = "TestCategorie2",
                IsActive = true,
                ParentSubCategorieId = null,
                Beschrijving = "Dit is TestCategorie2"
            }
        };

        public void Delete(SubCategorieDTO subCategorie, bool harddelete = false)
        {
            CategorieLijst.Remove(GetById((int)subCategorie.Id));
        }

        public List<SubCategorieDTO> GetAll()
        {
            return CategorieLijst;
        }

        public SubCategorieDTO GetById(int id)
        {
            return CategorieLijst.Where(c => c.Id == id).SingleOrDefault();
        }

        public SubCategorieDTO GetByName(string name)
        {
            return CategorieLijst.Where(c => c.Naam == name).SingleOrDefault();
        }

        public SubCategorieDTO GetByVraagId(int vraagId)
        {
            VraagStub vs = new VraagStub();
            VraagDTO vraag = vs.GetById(vraagId);
            SubCategorieDTO sub = CategorieLijst.Where(c => c.Id == vraag.SubCategorieId).SingleOrDefault();
            if(sub.Id != null)
            {
                return sub;
            }
            return new SubCategorieDTO();
        }

        public void Save(SubCategorieDTO dto)
        {
            CategorieLijst.Add(dto);
        }
    }
}
