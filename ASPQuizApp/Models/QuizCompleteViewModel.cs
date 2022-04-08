using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPQuizApp.Models
{
    public class QuizCompleteViewModel
    {
        public int Score { get; set; }
        public int AantalVragen { get; set; }
        public List<string> Vragen { get; set; }
        public List<string> SelectedAntwoorden { get; set; }
        public List<string> CorrecteAntwoorden { get; set; }

        public int[] GetDisplayValues()
        {
            int[] values = new int[2] { 0, 0 };

            double value = Convert.ToDouble(Score) / Convert.ToDouble(AantalVragen) * 360d;

            if (value < 180)
            {
                values[0] = Convert.ToInt32(Math.Floor(value));
            }
            else
            {
                values[0] = 180;
                values[1] = Convert.ToInt32(Math.Floor(value - 180));
            }

            return values;
        }
    }
}
