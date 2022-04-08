using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Interface;
using QuizLib.Data;

namespace QuizLib.Logic
{
    public class Gebruiker
    {
        public IGebruikerDAO iGebruikerDAO = new GebruikerMSSQLDAO();
        public int? Id { get; set; }
        public string Naam  { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public Gebruiker(GebruikerDTO dto)
        {
            Id = dto.Id;
            Naam = dto.Naam;
            Wachtwoord = dto.Wachtwoord;
            Email = dto.Email;
            IsAdmin = dto.IsAdmin;
        }

        public bool Save()
        {
            GebruikerDTO dto = new GebruikerDTO()
            {
                Id = this.Id,
                Naam = this.Naam,
                Wachtwoord = this.Wachtwoord,
                Email = this.Email,
                IsAdmin = this.IsAdmin
            };

            iGebruikerDAO.Save(dto);

            return true;
        }
    }
}
