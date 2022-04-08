using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Interface
{
    public interface ISubCategorieDAO
    {
        void Save(SubCategorieDTO dto);
    }
}
