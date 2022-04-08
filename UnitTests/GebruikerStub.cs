using QuizLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    public class GebruikerStub : IGebruikerContainerDAO, IGebruikerDAO
    {
        public List<GebruikerDTO> GebruikerLijst = new List<GebruikerDTO>()
        {
            new GebruikerDTO()
            {
                Naam = "TestGebruiker1",
                Id = 1,
                Wachtwoord = "TestWachtwoord1",
                Email = "Gebruiker1@Test.com",
                IsAdmin = false
            },
            new GebruikerDTO()
            {
                Naam = "TestGebruiker2",
                Id = 2,
                Wachtwoord = "TestWachtwoord2",
                Email = "Gebruiker2@Test.com",
                IsAdmin = true,
            },
            new GebruikerDTO()
            {
                Naam = "TestGebruiker3",
                Id = 3,
                Wachtwoord = "TestWachtwoord3",
                Email = "Gebruiker3@Test.com",
                IsAdmin = true,
            },
            new GebruikerDTO()
            {
                Naam = "TestGebruiker4",
                Id = 4,
                Wachtwoord = "TestWachtwoord4",
                Email = "Gebruiker4@Test.com",
                IsAdmin = false,
            },
        };
        public GebruikerDTO CheckLogin(GebruikerDTO user)
        {
            if (user.Naam != null && user.Wachtwoord != null)
            {
                return GebruikerLijst.Where(x => x.Naam == user.Naam).Where(x => x.Wachtwoord == user.Wachtwoord).SingleOrDefault();
            }
            return new GebruikerDTO() { Id = null};
        }

        public bool Delete(GebruikerDTO dto, bool harddelete = false)
        {
            GebruikerLijst.Remove(dto);
            return true;
        }

        public bool DemoteUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<GebruikerDTO> GetAll()
        {
            return GebruikerLijst;
        }

        public GebruikerDTO GetById(int id)
        {
            return GebruikerLijst.Where(x => x.Id == id).SingleOrDefault();
        }

        public bool PromoteUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(GebruikerDTO dto)
        {
            GebruikerLijst.Add(dto);
            return true;
        }
    }
}
