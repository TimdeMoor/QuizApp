using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Interface
{
    public interface IGebruikerDAO
    {
        public bool Save(GebruikerDTO dto);
    }
}
