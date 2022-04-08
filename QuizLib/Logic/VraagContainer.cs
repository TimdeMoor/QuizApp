using QuizLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Logic
{
    public class VraagContainer
    {
        private IVraagContainerDAO iVraagContainerDAO;

        public VraagContainer(IVraagContainerDAO iVraagContainerDAO)
        {
            this.iVraagContainerDAO = iVraagContainerDAO;
        }

        public List<Vraag> GetAll()
        {
            List<Vraag> vragenLijst = new List<Vraag>();
            List<VraagDTO> vraagDTOs = iVraagContainerDAO.GetAll();

            //convert alle dtos naar gewone vragen
            foreach (VraagDTO dto in vraagDTOs)
            {
                vragenLijst.Add(new Vraag(dto));
            }
            return vragenLijst;
        }

        public Vraag GetById(int id)
        {
            return new Vraag(iVraagContainerDAO.GetById(id));
        }

        public void Delete(int id)
        {
            iVraagContainerDAO.Delete(new VraagDTO() { Id = id });
        }

        public Vraag GetByText(string vraagText)
        {
            return new Vraag(iVraagContainerDAO.GetByText(vraagText));
        }

        public List<Vraag> GetByCategorieId(int id) {
            List<Vraag> vragenLijst = new List<Vraag>();
            List<VraagDTO> vraagDTOs = iVraagContainerDAO.GetByCategorieId(id);

            //convert alle dtos naar gewone vragen
            foreach (VraagDTO dto in vraagDTOs)
            {
                vragenLijst.Add(new Vraag(dto));
            }
            return vragenLijst;
        }

        public void ChangeVraagText(int id, string newText)
        {
            iVraagContainerDAO.ChangeVraagText(id, newText);
        }

        public Vraag GetByAntwoordId(int id)
        {
            return GetById(iVraagContainerDAO.GetVraagIdByAntwoordId(id));
        }
    }
}
