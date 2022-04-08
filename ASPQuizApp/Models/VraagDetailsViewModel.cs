using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizLib.Logic;

namespace ASPQuizApp.Models
{
    public class VraagDetailsViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string SubCategorieNaam { get; set; }
        public List<Antwoord> MogelijkeAntwoorden { get; set; }

        public VraagDetailsViewModel() { }

        public VraagDetailsViewModel(int Id, string Text, string SubCategorieNaam, List<Antwoord> Antwoorden)
        {
            this.Id = Id;
            this.Text = Text;
            this.SubCategorieNaam = SubCategorieNaam;
            this.MogelijkeAntwoorden = Antwoorden;
        }
    }
}
