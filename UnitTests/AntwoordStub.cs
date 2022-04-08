using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Interface;

namespace UnitTestProject
{
    public class AntwoordStub : IAntwoordContainerDAO, IAntwoordDAO
    {
        public List<AntwoordDTO> AntwoordenLijst = new List<AntwoordDTO>()
        {
            new AntwoordDTO()
            {
                Id = 1,
                IsActive = true,
                IsCorrect = true,
                Text = "2",
                VraagId = 1,
            },

            new AntwoordDTO()
            {
                Id = 2,
                IsActive = true,
                IsCorrect = false,
                Text = "3",
                VraagId = 1,
            },

            new AntwoordDTO()
            {
                Id = 3,
                IsActive = true,
                IsCorrect = false,
                Text = "1",
                VraagId = 1,
            },

            new AntwoordDTO()
            {
                Id = 4,
                IsActive = true,
                IsCorrect = false,
                Text = "kaas",
                VraagId = 1,
            },

            new AntwoordDTO()
            {
                Id = 5,
                IsActive = true,
                IsCorrect = true,
                Text = "4",
                VraagId = 2,
            },

            new AntwoordDTO()
            {
                Id = 6,
                IsActive = true,
                IsCorrect = false,
                Text = "5",
                VraagId = 2,
            },

            new AntwoordDTO()
            {
                Id = 7,
                IsActive = true,
                IsCorrect = false,
                Text = "6",
                VraagId = 2,
            },

            new AntwoordDTO()
            {
                Id = 8,
                IsActive = true,
                IsCorrect = false,
                Text = "broodje kaas",
                VraagId = 2,
            },

            new AntwoordDTO()
            {
                Id = 9,
                IsActive = true,
                IsCorrect = true,
                Text = "Mooi weer",
                VraagId = 3,
            },

            new AntwoordDTO()
            {
                Id = 10,
                IsActive = true,
                IsCorrect = false,
                Text = "Matig weer",
                VraagId = 3,
            },

            new AntwoordDTO()
            {
                Id = 11,
                IsActive = true,
                IsCorrect = false,
                Text = "Slecht weer",
                VraagId = 3,
            },

            new AntwoordDTO()
            {
                Id = 12,
                IsActive = true,
                IsCorrect = false,
                Text = "Kut weer",
                VraagId = 3,
            },
        };

        public List<AntwoordDTO> GetAll()
        {
            return AntwoordenLijst;
        }

        public AntwoordDTO GetById(int id)
        {
            return AntwoordenLijst.Where(a => a.Id == id).SingleOrDefault();
        }

        public List<AntwoordDTO> GetByVraagId(int id)
        {
            return AntwoordenLijst.Where(a => a.VraagId == id).ToList();
        }

        public AntwoordDTO GetCorrectAntwoordByVraagId(int id)
        {
            return GetByVraagId(id).Where(a => a.IsCorrect == true).SingleOrDefault();
        }

        public void Save(AntwoordDTO dto)
        {
            AntwoordenLijst.Add(dto);
        }

        public void Delete(AntwoordDTO dto)
        {
            AntwoordenLijst.Remove(dto);
        }

        public void Delete(AntwoordDTO dto, bool harddelete = false)
        {
            AntwoordenLijst.Remove(dto);
        }

        public int GetCorrectAntwoordIdBySelectedAntwoordId(int id)
        {
            throw new NotImplementedException();
        }

        public void SetCorrect(int id)
        {
            throw new NotImplementedException();
        }

        public void SetIncorrect(int id)
        {
            throw new NotImplementedException();
        }
    }
}
