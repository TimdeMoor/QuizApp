using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizLib.Logic;

namespace ASPQuizApp.Models
{
    public class QuizStatusViewModel
    {
        public List<GameVraagViewModel> Quizvragen { get; set; }
        public int Score { get; set; }
        public int VraagNummer { get; set; }


       
    }
}
