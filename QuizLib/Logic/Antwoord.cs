using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Data;
using QuizLib.Interface;

namespace QuizLib.Logic
{
    public class Antwoord
    {
        public IAntwoordDAO iAntwoordDAO = new AntwoordMSSQLDAO();
        public int? Id {get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int VraagId { get; set; }
        public bool IsActive { get; set; }

        public Antwoord(AntwoordDTO dto)
        {
            Id = dto.Id;
            Text = dto.Text;
            IsCorrect = dto.IsCorrect;
            VraagId = dto.VraagId;
            IsActive = dto.IsActive;
        }

        public void Save()
        {
            iAntwoordDAO.Save(new AntwoordDTO()
            {
                Id = this.Id,
                Text = this.Text,
                IsCorrect = this.IsCorrect,
                VraagId = this.VraagId,
                IsActive = this.IsActive,
            });
        }

        public override string ToString()
        {
            return String.Format("Antwoord: {0}, Correct: {1}", Text, IsCorrect);
        }
    }
}
