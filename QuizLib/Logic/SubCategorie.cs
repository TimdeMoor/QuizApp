using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Interface;
using QuizLib.Data;

namespace QuizLib.Logic
{
    public class SubCategorie
    {
        public ISubCategorieDAO iSubCategorieDAO = new SubCategorieMSSQLDAO();
        public int? Id { get; set; }
        public int? ParentSubCategorieId { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }

        public SubCategorie(SubCategorieDTO dto)
        {
            Id = dto.Id;
            ParentSubCategorieId = dto.ParentSubCategorieId;
            Naam = dto.Naam;
            Beschrijving = dto.Beschrijving;
        }

        public void Save()
        {
            SubCategorieDTO dto = new SubCategorieDTO()
            {
                Id = this.Id,
                ParentSubCategorieId = this.ParentSubCategorieId,
                Naam = this.Naam,
                Beschrijving = this.Beschrijving,
            };
            iSubCategorieDAO.Save(dto);
        }

        public override string ToString()
        {
            return String.Format("Subcategorie: {0}", Naam);
        }
    }
}
