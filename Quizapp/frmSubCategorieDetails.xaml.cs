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
    /// <summary>
    /// Interaction logic for frmSubCategorieDetails.xaml
    /// </summary>
    public partial class frmSubCategorieDetails : Window
    {
        private SubCategorieContainer scc;
        private bool editMode = false;
        private SubCategorie subCategorie;

        //Constructor without subCategorie object (CreateMode)
        public frmSubCategorieDetails(SubCategorieContainer _scc)
        {
            InitializeComponent();
            scc = _scc;
            updateGui();
        }

        //Constructor with question object (EditMode)
        public frmSubCategorieDetails(SubCategorieContainer _scc, SubCategorie _subCategorieToUpdate)
        {
            InitializeComponent();
            scc = _scc;
            updateGui();
            populateFields(_subCategorieToUpdate);
            editMode = true;
            subCategorie = _subCategorieToUpdate;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SubCategorie selectedSubCategorie = getSelectedSubCategorie();
            //Checks if a subCategorie needs to be updated or created
            if (editMode)
            {
                //Checks if a categorie is selected
                if (selectedSubCategorie == null)
                {
                    //Removes connection with parent categorie
                    scc.DeleteParentConnection(subCategorie);

                    //Updates subcategorie with new details
                    scc.UpdateSubCategorie(subCategorie, null, txtNaam.Text, txtBeschrijving.Text);
                }
                else
                {
                    scc.UpdateSubCategorie(subCategorie, getSelectedSubCategorie().id, txtNaam.Text, txtBeschrijving.Text);
                }
            }
            else
            {
                //Create new subCategorie
                SubCategorie nieuweSubCategorie = new SubCategorie();
                nieuweSubCategorie.naam = txtNaam.Text;
                nieuweSubCategorie.beschrijving = txtBeschrijving.Text;
                nieuweSubCategorie.isActive = true;

                if (selectedSubCategorie != null)
                {
                    nieuweSubCategorie.parentSubCategorieId = selectedSubCategorie.id;
                }

                scc.AddSubCategorie(nieuweSubCategorie);
            }
            DialogResult = true;
            this.Close();
        }

        private void updateGui()
        {
            //puts all the subCategories in the combobox
            cmbSubCategorie.ItemsSource = scc.GetAllSubCategories();
        }

        //fills in details of the provided subCategorie
        private void populateFields(SubCategorie subCategorie)
        {
            txtNaam.Text = subCategorie.naam;
            txtBeschrijving.Text = subCategorie.beschrijving;

            //check if categorie has parent and fill in combobox
            if (subCategorie.parentSubCategorieId != null)
                cmbSubCategorie.SelectedItem = scc.GetSubCategorieById((int)subCategorie.parentSubCategorieId);              
        }

        //gets the selected categorie in the combobox
        private SubCategorie getSelectedSubCategorie()
        {
            if (cmbSubCategorie.SelectedIndex != -1)
                return (SubCategorie)cmbSubCategorie.SelectedItem;
            return null;
        }

        private void btnGeenCategorie_Click(object sender, RoutedEventArgs e)
        {
            cmbSubCategorie.SelectedIndex = -1;
        }
    }
}
