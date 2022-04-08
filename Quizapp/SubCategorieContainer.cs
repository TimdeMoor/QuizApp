using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Quizapp
{
    public class SubCategorieContainer
    {
        private SubCategorieRepository subCategorieRepository;
        private List<SubCategorie> subCategorieLijst;

        //constructor
        public SubCategorieContainer(dbDataContext _db)
        {
            subCategorieRepository = new SubCategorieRepository(_db);
            subCategorieLijst = new List<SubCategorie>();
        }

        //add a new subCategorie to the lijst and database
        public void AddSubCategorie(SubCategorie sc)
        {
            subCategorieLijst.Add(sc);
            subCategorieRepository.addSubCategorie(sc);
        }

        //returns all the subcategories in the database
        public List<SubCategorie> GetAllSubCategories()
        {
            syncSubCategorieLijst();
            return subCategorieLijst;
        }

        //returns a single subcategorie by id
        public SubCategorie GetSubCategorieById(int id)
        {
            syncSubCategorieLijst();
            return subCategorieRepository.GetSubCategorie(id);
        }

        //synchronizes the list with the database
        private void syncSubCategorieLijst()
        {
            subCategorieLijst = subCategorieRepository.getAlleSubCategories();
        }

        //asks the user to confirm deletion of the specified subCategorie
        public void DeleteSubCategorie(SubCategorie sc, bool askUser)
        {
            //tries to delete subCategorie
            try
            {
                if (askUser)
                {
                    frmConfirm form = new frmConfirm("Wil je de categorie: " + sc.naam + " verwijderen?");
                    if ((bool)form.ShowDialog())
                        DeleteSubCategorie(sc);
                    else return;
                }
                else
                    DeleteSubCategorie(sc);
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan, zorg dat deze categorie geen vragen of andere categorieën bevat.");
            }
        }

        //deletes the subCategorie
        public void DeleteSubCategorie(SubCategorie sc)
        {
            subCategorieLijst.Remove(sc);
            subCategorieRepository.deleteSubCategorie(sc);
        }

        //updates a subCategorie with new information
        public void UpdateSubCategorie(SubCategorie oldSubCategorie, int? newParentId, string nieuweNaam, string nieuweBeschrijving)
        {
            subCategorieRepository.updateSubCategorie(oldSubCategorie.id, newParentId, nieuweNaam, nieuweBeschrijving);
            syncSubCategorieLijst();
        }

        //removes connection beween parent and child categorie
        public void DeleteParentConnection(SubCategorie child)
        {
            subCategorieRepository.deleteParentConnection(child);
            syncSubCategorieLijst();
        }
    }
}
