using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizapp
{
    public class VraagDTO
    {
        private int? _id { get; }
        private string _text { get; }
        private int? _subCategorieId { get; }
        private bool _isActive { get; }

        public VraagDTO(int? id, string text, int? subCategorieId, bool isActive)
        {
            _id = id;
            _text = text;
            _subCategorieId = subCategorieId;
            _isActive = isActive;
        }
    }
}
