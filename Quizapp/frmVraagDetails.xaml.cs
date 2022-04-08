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
    //Form can be used to edit and create a question
    public partial class frmVraagDetails : Window
    {
        bool editMode = false;
        VraagContainer vc;
        SubCategorieContainer scc;
        Vraag vraag;

        //Constructor without question object (CreateMode)
        public frmVraagDetails(VraagContainer _vc, SubCategorieContainer _scc)
        {
            InitializeComponent();
            vc = _vc;
            scc = _scc;

            UpdateGui();
        }

        //Constructor with question object (EditMode)
        public frmVraagDetails(VraagContainer _vc, SubCategorieContainer _scc, Vraag _vraag)
        {
            InitializeComponent();
            vc = _vc;
            scc = _scc;
            UpdateGui();
            populateFields(_vraag);
            editMode = true;
            vraag = _vraag;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //checks if all fields are filled
            if (!checkInput())
            {
                MessageBox.Show("Vul alle velden in");
                return;
            }

            int selectedSubCategorieId = ((SubCategorie)cmbSubCategorie.SelectedItem).id;
            //Checks if a question needs to be updated or created
            if (editMode)
            {
                //updates provided vraag
                vc.UpdateVraag(vraag, txtVraag.Text, selectedSubCategorieId);
            }
            else
            {
                //Create new vraag
                vc.AddVraag(new Vraag()
                {
                    text = txtVraag.Text,
                    subCategorieId = selectedSubCategorieId,
                });
            }
            DialogResult = true;
            this.Close();
        }

        private void UpdateGui()
        {
            cmbSubCategorie.ItemsSource = scc.GetAllSubCategories();
        }

        //Puts the details of the provided question in the form
        private void populateFields(Vraag v)
        {
            txtVraag.Text = v.text;
            cmbSubCategorie.SelectedItem = scc.GetSubCategorieById(v.subCategorieId);
        }

        private bool checkInput()
        {
            //Checks if all the fields have data and returns true
            if (txtVraag.Text != string.Empty
                && lstAntwoorden.Items.Count > 0
                && cmbSubCategorie.SelectedIndex != -1)
                return true;
            return false;
        }

        private void btnAntwoordToevoegen_Click(object sender, RoutedEventArgs e)
        {
            generateAntwoordField();
        }

        private void generateAntwoordField(string antwoordText = null, Boolean isCorrect = false)
        {
            //Add new listboxitem containing a grid containing a textbox and a checkbox
            lstAntwoorden.Items.Add(
                new Grid()
                {
                    Width = 200,
                    Children = {
                        new TextBox()
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Width = 120,
                            Height = 20,
                            TextWrapping = TextWrapping.Wrap,
                            Text = antwoordText,
                        },
                        new CheckBox()
                        {
                            HorizontalAlignment = HorizontalAlignment.Right,
                            Margin = new Thickness(0, 15, 40, 10),
                            Width = 20,
                            Height = 20,
                            IsChecked = isCorrect,
                        },
                        new Button()
                        {
                            HorizontalAlignment = HorizontalAlignment.Right,
                            Margin = new Thickness(10),
                            Name = "btnDeleteAnswer",
                            Width = 20,
                            Height = 20,
                            Content = "X",
                        }
                    }
                }
            );
        }
    }
}
