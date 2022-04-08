using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Interface;

namespace QuizLib.Logic
{
    public class SubCategorieContainer
    {
        private ISubCategorieContainerDAO iSubCategorieContainerDAO;
        public SubCategorieContainer(ISubCategorieContainerDAO dao)
        {
            iSubCategorieContainerDAO = dao;
        }

        public SubCategorie GetByVraagId(int id)
        {
            return new SubCategorie(iSubCategorieContainerDAO.GetByVraagId(id));
        }

        public List<SubCategorie> GetAll()
        {
            List<SubCategorie> results = new List<SubCategorie>();
            foreach (SubCategorieDTO dto in iSubCategorieContainerDAO.GetAll())
            {
                results.Add(new SubCategorie(dto));
            }
            return results;
        }

        public SubCategorie GetById(int id)
        {
            return new SubCategorie(iSubCategorieContainerDAO.GetById(id));
        }

        public SubCategorie GetByName(string name)
        {
            return new SubCategorie(iSubCategorieContainerDAO.GetByName(name));
        }

        public SubCategorie GetParent(int id)
        {
            SubCategorie child = GetById(id);
            SubCategorie parent;
            if (child.ParentSubCategorieId != null)
            {
                parent = GetById((int)child.ParentSubCategorieId);
                return parent;
            }
            return null;
        }

        public void Delete(int id)
        {
            //TODO: Also delete questions within this category
            iSubCategorieContainerDAO.Delete(new SubCategorieDTO() { Id = id });
        }

        public List<SubCategorie> GetChildrenbyParentId(int id)
        {
            List<SubCategorie> temp = new List<SubCategorie>();
            foreach(SubCategorieDTO dto in iSubCategorieContainerDAO.GetChildrenByParentId(id))
            {
                temp.Add(new SubCategorie(dto));
            }
            return temp;
        }
    }
}
