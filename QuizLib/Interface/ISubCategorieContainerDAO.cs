using QuizLib.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Interface
{
    public interface ISubCategorieContainerDAO
    {
        public SubCategorieDTO GetByVraagId(int id);
        public List<SubCategorieDTO> GetAll();
        public SubCategorieDTO GetById(int id);
        public SubCategorieDTO GetByName(string name);
        public void Delete(SubCategorieDTO subCategorie, bool harddelete = false);
        public List<SubCategorieDTO> GetChildrenByParentId(int id);
    }
}
