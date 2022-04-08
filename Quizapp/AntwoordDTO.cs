using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizapp
{
    public class AntwoordDTO
    {
        private int? _id { get; }
        private string _text { get; }
        private bool _isCorrect { get; }
        private int _vraagId { get; }
        private bool _isActive { get; }


        public AntwoordDTO(int id, string text, bool isCorrect, int vraagId, bool isActive)
        {
            _id = id;
            _text = text;
            _isCorrect = isCorrect;
            _vraagId = vraagId;
            _isActive = isActive;
        }
    }
}
