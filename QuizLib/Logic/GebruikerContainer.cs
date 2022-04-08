using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Interface;

namespace QuizLib.Logic
{
    public class GebruikerContainer
    {
        private IGebruikerContainerDAO igebruikerContainerDAO;

        public GebruikerContainer(IGebruikerContainerDAO iGebruikerContainerDAO)
        {
            this.igebruikerContainerDAO = iGebruikerContainerDAO;
        }

        public List<Gebruiker> GetAll()
        {
            List<Gebruiker> gebruikerLijst = new List<Gebruiker>();
            List<GebruikerDTO> gebruikerDTOs = igebruikerContainerDAO.GetAll();
        
            foreach (GebruikerDTO dto in gebruikerDTOs)
            {
                gebruikerLijst.Add(new Gebruiker(dto));
            }

            return gebruikerLijst;
        }

        public Gebruiker CheckLogin(GebruikerDTO dto)
        {
            dto = (igebruikerContainerDAO.CheckLogin(dto));
            if(dto.Id != null)
            {
                return new Gebruiker(dto);
            }
            return new Gebruiker(new GebruikerDTO() { Id = null }) ;
        }

        public Gebruiker GetById(int id)
        {
            return new Gebruiker(igebruikerContainerDAO.GetById(id));
        }

        public void Delete(int id)
        {
            igebruikerContainerDAO.Delete(new GebruikerDTO() { Id = id });
        }

        public void PromoteUser(int id)
        {
            igebruikerContainerDAO.PromoteUser(id);
        }

        public void DemoteUser(int id)
        {
            igebruikerContainerDAO.DemoteUser(id);
        }
    }
}
