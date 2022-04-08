using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Quizapp
{
    //Main screen to answer questions
    public partial class frmQuiz : Window
    {
        private VraagContainer vc;
        private Vraag huidigeVraag;

        public frmQuiz(VraagContainer _vc)
        {
            vc = _vc;
            InitializeComponent();
            
            //Gets new question
            nieuweVraag();
        }

        //Checks if given answer is correct
        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            //if (txtAntwoord.Text == huidigeVraag.antwoord)
            //{
            //    MessageBox.Show("Correct");
            //}
            //else
            //{
            //    MessageBox.Show("Fout");
            //}
            //Gets new question
            nieuweVraag();
        }

        //Get a new question and display it
        private void nieuweVraag()
        {
            huidigeVraag = vc.GetRandomVraag();
            lblVraag.Content = huidigeVraag.text;
            txtAntwoord.Text = string.Empty;
        }

        private void txtAntwoord_KeyDown(object sender, KeyEventArgs e)
        {
            btnCheck_Click(sender, e);
        }
    }
}
