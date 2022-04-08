using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Interface
{
    public struct SubCategorieDTO
    {
        public int? Id { get; set; }
        public int? ParentSubCategorieId { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public bool IsActive { get; set; }
    }
}
