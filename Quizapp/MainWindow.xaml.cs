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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quizapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private dbDataContext db = new dbDataContext();

        private VraagContainer vc;
        private SubCategorieContainer scc;

        private ucVragenOverzicht ucVragenOverzicht;
        private ucCategorieOverzicht ucCategorieOverzicht;

        public MainWindow()
        {
            InitializeComponent();

            vc = new VraagContainer(db);
            scc = new SubCategorieContainer(db);

            ucVragenOverzicht = new ucVragenOverzicht(vc, scc);
            ucCategorieOverzicht = new ucCategorieOverzicht(scc);
            
            UpdateGui();
        }


        //Refresh the gui with new details
        private void UpdateGui()
        {
            frameVragen.Content = ucVragenOverzicht;
            frameCategorieën.Content = ucCategorieOverzicht;
        }

        //Opens the main game form and closes itself
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            frmQuiz frm = new frmQuiz(vc);
            frm.Show();
            this.Close();
        }     
    }
}
