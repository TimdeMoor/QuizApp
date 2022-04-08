using QuizLib.Interface;
using QuizLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Logic
{
    public class Vraag
    {
        public IVraagDAO iVraagDAO = new VraagMSSQLDAO();
        public int? Id { get; set; }
        public string Text { get; set; }
        public int? SubCategorieId { get; set; }

        public Vraag(VraagDTO dto)
        {
            Id = dto.Id;
            Text = dto.Text;
            SubCategorieId = dto.SubCategorieId;
        }

        public bool Save()
        {
            VraagDTO dto = new VraagDTO()
            {
                Id = this.Id,
                Text = this.Text,
                SubCategorieId = this.SubCategorieId
            };

            iVraagDAO.Save(dto);

            return true;
        }

        public override string ToString()
        {
            return string.Format("Vraag: {0}", this.Text);
        }
    }
}
