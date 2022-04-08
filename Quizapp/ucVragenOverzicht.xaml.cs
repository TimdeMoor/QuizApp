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
    /// Interaction logic for ucVragenOverzicht.xaml
    /// </summary>
    public partial class ucVragenOverzicht : UserControl
    {
        VraagContainer vc;
        SubCategorieContainer scc;
        public ucVragenOverzicht(VraagContainer _vc, SubCategorieContainer _scc)
        {
            vc = _vc;
            scc = _scc;
            InitializeComponent();
            UpdateGui();
        }

        //Delete the selected question
        private void btnDeleteVraag_Click(object sender, RoutedEventArgs e)
        {
            Vraag selectedVraag = (Vraag)dtgVragen.SelectedItem;
            vc.DeleteVraag(selectedVraag, true);
            UpdateGui();
        }

        //Opens a form to edit the selected question
        private void btnEditVraag_Click(object sender, RoutedEventArgs e)
        {
            Vraag selectedVraag = (Vraag)dtgVragen.SelectedItem;
            frmVraagDetails frmVraagDetails = new frmVraagDetails(vc, scc, selectedVraag);
            frmVraagDetails.ShowDialog();
            UpdateGui();
        }

        //Opens a form to add a new question and waits for the form to be closed
        private void btnVraagToevoegen_Click(object sender, RoutedEventArgs e)
        {
            frmVraagDetails frmVraagDetails = new frmVraagDetails(vc, scc);
            frmVraagDetails.ShowDialog();
            UpdateGui();
        }

        //Refreshes the gui with latest info from database
        private void UpdateGui()
        {
            dtgVragen.ItemsSource = vc.GetAlleVragen();
        }
    }
}
