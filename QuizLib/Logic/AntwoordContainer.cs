using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Interface;

namespace QuizLib.Logic
{
    public class AntwoordContainer
    {
        private Random r = new Random();
        private IAntwoordContainerDAO iAntwoordContainerDAO;

        public AntwoordContainer(IAntwoordContainerDAO iAntwoordContainerDAO)
        {
            this.iAntwoordContainerDAO = iAntwoordContainerDAO;
        }

        public List<Antwoord> GetAll()
        {
            List<Antwoord> antwoordLijst = new List<Antwoord>();
            List<AntwoordDTO> antwoordDTOs = iAntwoordContainerDAO.GetAll();

            //convert alle dtos naar gewone antwoorden
            foreach (AntwoordDTO dto in antwoordDTOs)
            {
                antwoordLijst.Add(new Antwoord(dto));
            }
            return antwoordLijst;
        }

        public Antwoord GetById(int id)
        {
            return new Antwoord(iAntwoordContainerDAO.GetById(id));
        }

        public Antwoord GetCorrecteAntwoord(int vraagId)
        {
            return new Antwoord(iAntwoordContainerDAO.GetCorrectAntwoordByVraagId(vraagId));
        }

        public List<Antwoord> GetMogelijkeAntwoorden(int vraagId, bool shuffled = false)
        {
            List<Antwoord> results = new List<Antwoord>();
            foreach(AntwoordDTO dto in iAntwoordContainerDAO.GetByVraagId(vraagId))
            {
                results.Add(new Antwoord(dto));
            }

            if (shuffled)
            {
                return Shuffle(results);
            }
            else
            {
                results = (from a in results orderby a.IsCorrect descending select a).ToList(); 
                return results;
            }
        }

        public List<Antwoord> Shuffle(List<Antwoord> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                Antwoord value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public Antwoord GetCorrectAntwoordByVraagId(int id)
        {
            return new Antwoord(iAntwoordContainerDAO.GetCorrectAntwoordByVraagId(id));
        }

        public List<Antwoord> GetByVraagId(int vraagId)
        {
            List<Antwoord> results = new List<Antwoord>();
            foreach(AntwoordDTO dto in iAntwoordContainerDAO.GetByVraagId(vraagId))
            {
                results.Add(new Antwoord(dto));
            }
            return results;
        }

        public void Delete(AntwoordDTO dto, bool harddelete = false)
        {
            iAntwoordContainerDAO.Delete(dto);
        }

        public int GetCorrectAntwoordIdBySelectedAntwoordId(int id)
        {
            return iAntwoordContainerDAO.GetCorrectAntwoordIdBySelectedAntwoordId(id);
        }

        public void SetCorrect(int id)
        {
            iAntwoordContainerDAO.SetCorrect(id);
        }

        public void SetIncorrect(int id)
        {
            iAntwoordContainerDAO.SetIncorrect(id);
        }

        public void ChangeAntwoordText(int id, string newText)
        {
            iAntwoordContainerDAO.ChangeAntwoordText(id, newText);
        }
    }
}
