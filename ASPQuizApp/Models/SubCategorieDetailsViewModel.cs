using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPQuizApp.Models
{
    public class SubCategorieDetailsViewModel
    {
        public int Id { get; set; }
        public string ParentSubCategorie { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }

        public SubCategorieDetailsViewModel() { }

        public SubCategorieDetailsViewModel(int id, string parentSubCategorie, string naam, string beschrijving)
        {
            this.Id = id;
            this.ParentSubCategorie = parentSubCategorie;
            this.Naam = naam;
            this.Beschrijving = beschrijving;
        }
    }
}
