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
    /// Interaction logic for ucCategorieOverzicht.xaml
    /// </summary>
    public partial class ucCategorieOverzicht : UserControl
    {
        SubCategorieContainer scc;
        public ucCategorieOverzicht(SubCategorieContainer _scc)
        {
            scc = _scc;
            InitializeComponent();
            UpdateGui();
        }


        private void btnCategorieToevoegen_Click(object sender, RoutedEventArgs e)
        {
            frmSubCategorieDetails form = new frmSubCategorieDetails(scc);
            form.ShowDialog();
            UpdateGui();
        }

        private void btnEditCategorie_Click(object sender, RoutedEventArgs e)
        {
            SubCategorie selectedSubCategorie = (SubCategorie)dtgCategorieën.SelectedItem;
            frmSubCategorieDetails form = new frmSubCategorieDetails(scc, selectedSubCategorie);
            form.ShowDialog();
            UpdateGui();
        }

        private void btnDeleteCategorie_Click(object sender, RoutedEventArgs e)
        {
            SubCategorie selectedSubCategorie = (SubCategorie)dtgCategorieën.SelectedItem;
            scc.DeleteSubCategorie(selectedSubCategorie, true);
            UpdateGui();
        }

        private void UpdateGui()
        {
            dtgCategorieën.ItemsSource = scc.GetAllSubCategories();
        }
    }
}
