using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizLib.Logic;

namespace ASPQuizApp.Models
{
    public class GameVraagViewModel
    {
        public Vraag Vraag { get; set; }
        public List<Antwoord> Antwoorden { get; set; }
        public SubCategorie Categorie { get; set; }
    }
}
