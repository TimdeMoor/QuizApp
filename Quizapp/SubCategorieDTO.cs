using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizapp
{
    public class SubCategorieDTO
    {
        private int? _id { get; }
        private int? _parentSubCategorieId { get; }
        private string _naam { get; }
        private string _beschrijving { get; }
        private bool _isActive { get; }

        public SubCategorieDTO(int? id, int? parentSubCategorieId, string naam, string beschrijving, bool isActive)
        {
            _id = id;
            _parentSubCategorieId = parentSubCategorieId;
            _naam = naam;
            _beschrijving = beschrijving;
            _isActive = isActive;
        }
    }
}
