using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Interface
{
    public struct GebruikerDTO
    {
        public int? Id { get; set; }
        public string Naam { get; set; }
        public string Wachtwoord { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
