using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Quizapp
{
    public class VraagContainer
    {
        private VraagRepository vraagRepository;
        private List<Vraag> vragenLijst;
        private Random r = new Random();

        //constructor
        public VraagContainer(dbDataContext _db)
        {
            vraagRepository = new VraagRepository(_db);
            syncVragenLijst();
        }

        //adds a new vraag to the vragenlijst and database
        public void AddVraag(Vraag v)
        {
            vragenLijst.Add(v);
            vraagRepository.addVraag(v);
        }

        //returns all the vragen in the list
        public List<Vraag> GetAlleVragen()
        {
            syncVragenLijst();
            return vragenLijst;
        }

        //returns a random vraag from the list
        public Vraag GetRandomVraag()
        {
            return vragenLijst[r.Next(vragenLijst.Count)];
        }

        //returns a random vraag from a specified categorie
        public Vraag GetRandomVraagFromCategorien(List<SubCategorie> subCategories)
        {
            List<Vraag> tempList = new List<Vraag>();

            foreach (SubCategorie sc in subCategories)
            {
                tempList.AddRange(GetVragenFromCategorie(sc));
            }

            return tempList[r.Next(tempList.Count)];
        }

        //returns a vraag by id
        public Vraag GetVraagById(int id)
        {
            return vraagRepository.getVraag(id);
        }

        //returns all the vragen from the subCategorie
        public List<Vraag> GetVragenFromCategorie(SubCategorie sc)
        {
            return vragenLijst.Where(x => x.subCategorieId == sc.id).ToList(); 
        }

        //asks the user to confirm deletion of the specified vraag from the container and the database
        public void DeleteVraag(Vraag v, bool askUser)
        {
            //tries to delete vraag
            try
            {
                if (askUser)
                {
                    frmConfirm form = new frmConfirm("Wil je de vraag: " + v.text + " verwijderen?");
                    if ((bool)form.ShowDialog())
                        DeleteVraag(v);
                    else
                        return;
                }
                else
                    DeleteVraag(v);
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan, probeer opnieuw");
            }
        }

        //deletes the vraag from the container and database
        private void DeleteVraag(Vraag v)
        {
            vragenLijst.Remove(v);
            vraagRepository.deleteVraag(v);
        }

        //synchronizes the vragenlijst in this container with the database
        private void syncVragenLijst()
        {
            vragenLijst = vraagRepository.getAlleVragen();
        }

        //Updates a vraag with new information
        public void UpdateVraag(Vraag oldVraag, string nieuweVraagText, int nieuwSubCategorieId)
        {
            vraagRepository.updateVraag(oldVraag.id, nieuweVraagText, nieuwSubCategorieId);
            syncVragenLijst();
        }
    }
}
