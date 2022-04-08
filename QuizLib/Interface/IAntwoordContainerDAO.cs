using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Interface
{
    public interface IAntwoordContainerDAO
    {
        List<AntwoordDTO> GetAll();
        AntwoordDTO GetById(int id);
        List<AntwoordDTO> GetByVraagId(int id);
        public AntwoordDTO GetCorrectAntwoordByVraagId(int id);
        public void Delete(AntwoordDTO dto, bool harddelete = false);
        public int GetCorrectAntwoordIdBySelectedAntwoordId(int id);
        public void SetCorrect(int id);
        public void SetIncorrect(int id);
        public void ChangeAntwoordText(int id, string newText);
    }
}
