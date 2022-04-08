using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Interface
{
    public struct AntwoordDTO
    {
        public int? Id { get; set; }
        public int VraagId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsActive { get; set; }
    }
}
