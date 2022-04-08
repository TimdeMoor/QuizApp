using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Interface
{
    public interface IAntwoordDAO
    {
        public void Save(AntwoordDTO dto);
    }
}
