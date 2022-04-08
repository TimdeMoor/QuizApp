using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizapp
{
    public class SubCategorieRepository
    {
        private dbDataContext db;
        public SubCategorieRepository(dbDataContext _db) { db = _db; }

        //gets all subcategories from the database and returns a list
        public List<SubCategorie> getAlleSubCategories()
        {
            return (from SubCategorie in db.SubCategories select SubCategorie).ToList();
        }

        //gets the specific question via id
        public SubCategorie GetSubCategorie(int id)
        {
            return (from SubCategorie in db.SubCategories where SubCategorie.id == id select SubCategorie).SingleOrDefault();
        }

        //add a new subcategorie to the database via subcategorie object
        public void addSubCategorie(SubCategorie sc)
        {
            db.SubCategories.InsertOnSubmit(sc);
            db.SubmitChanges();
        }

        //add a new subcategorie to the database with subcategorie details
        public void addSubCategorie(string _naam, string _beschrijving)
        {
            SubCategorie sc = new SubCategorie()
            {
                naam = _naam,
                beschrijving = _beschrijving,
                isActive = true,
            };
        }

        //delete subcategorie via id
        public void deleteSubCategorie(int id)
        {
            db.SubCategories.DeleteOnSubmit(GetSubCategorie(id));
            db.SubmitChanges();
        }

        //delete subcategorie via subcategorie object
        public void deleteSubCategorie(SubCategorie sc)
        {
            deleteSubCategorie(sc.id);
        }

        //updates the subcategorie corresponding with the given id with new details
        public void updateSubCategorie(int subCategorieId, int? newParentId, string nieuweNaam, string nieuweBeschrijving)
        {
            SubCategorie sc = GetSubCategorie(subCategorieId);
            sc.naam = nieuweNaam;
            sc.beschrijving = nieuweBeschrijving;

            //checks if a categorie will get a new parent and sets this parent
            if (newParentId != null)
                sc.SubCategorie1 = GetSubCategorie((int)newParentId);
            else
                sc.SubCategorie1 = null;

            db.SubmitChanges();
        }

        //gets the parent categorie
        public SubCategorie GetParent(SubCategorie sc)
        {
            //check if subcategorie has a parent
            if (sc.parentSubCategorieId != null)
                return GetSubCategorie((int)sc.parentSubCategorieId);
            return null;
        }

        //deletes the connection bewteen parent and child categorie
        public void deleteParentConnection(SubCategorie child)
        {
            child.SubCategorie1 = null;
            db.SubmitChanges();
        }

        //gets all the child categorieën of the provided subCategorie
        public List<SubCategorie> GetChildren(SubCategorie sc)
        {
            return getAlleSubCategories().Where(x => x.parentSubCategorieId == sc.id).ToList();
        }
    }
}
