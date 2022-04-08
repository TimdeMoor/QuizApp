using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Interface
{
    public interface IGebruikerContainerDAO
    {
        public List<GebruikerDTO> GetAll();
        public GebruikerDTO GetById(int id);
        public GebruikerDTO CheckLogin(GebruikerDTO user);
        public bool Delete(GebruikerDTO dto, bool harddelete = false);
        public bool PromoteUser(int id);
        public bool DemoteUser(int id);
    }
}
