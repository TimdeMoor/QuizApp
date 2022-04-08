using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Interface;

namespace UnitTestProject
{
    public class VraagStub : IVraagContainerDAO, IVraagDAO
    {
        public List<VraagDTO> vragenLijst = new List<VraagDTO>()
        {
            new VraagDTO()
            {
                Id = 1,
                SubCategorieId = null,
                Text = "Wat is 1 + 1?",
            },

            new VraagDTO()
            {
                Id = 2,
                SubCategorieId = 1,
                Text = "Wat is 2 + 2?",
            },

            new VraagDTO()
            {
                Id = 3,
                SubCategorieId = 1,
                Text = "Wat voor weer is het nu?",
            },
        };

        public void Delete(VraagDTO vraag, bool harddelete = false)
        {
            vragenLijst.Remove(GetById((int)vraag.Id));
        }

        public List<VraagDTO> GetAll()
        {
            return vragenLijst;
        }

        public List<VraagDTO> GetByCategorieId(int id)
        {
            throw new NotImplementedException();
        }

        public VraagDTO GetById(int id)
        {
            return vragenLijst.Where(vraag => vraag.Id == id).SingleOrDefault();
        }

        public VraagDTO GetByText(string vraagText)
        {
            return vragenLijst.Where(vraag => vraag.Text == vraagText).SingleOrDefault();
        }

        public void Save(VraagDTO v)
        {
            vragenLijst.Add(v);
        }
    }
}
