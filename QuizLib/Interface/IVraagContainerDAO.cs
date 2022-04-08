using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Interface
{
    public interface IVraagContainerDAO
    {
        public List<VraagDTO> GetAll();
        public VraagDTO GetById(int id);
        public void Delete(VraagDTO vraag, bool harddelete = false);
        public VraagDTO GetByText(string vraagText);
        public List<VraagDTO> GetByCategorieId(int id);
        public void ChangeVraagText(int id, string newText);
        public int GetVraagIdByAntwoordId(int id);
    }
}
