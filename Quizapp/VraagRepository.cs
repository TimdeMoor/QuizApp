using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizapp
{
    class VraagRepository
    {
        dbDataContext db;
        Random r = new Random();

        //Constructor
        public VraagRepository(dbDataContext _db) { db = _db; }

        //Gets all questions from the database and returns a list
        public List<Vraag> getAlleVragen()
        {
            return (from vraag in db.Vraags select vraag).ToList();
        }

        //Gets the specific question via id
        public Vraag getVraag(int id)
        {
            return (from vraag in db.Vraags where vraag.id == id select vraag).SingleOrDefault();
        }
        

        //Add a new question to the database with question object
        public void addVraag(Vraag v)
        {
            db.Vraags.InsertOnSubmit(v);
            db.SubmitChanges();
        }

        //Add a new question to the database with question details
        public void addVraag(string text, int subCategorieId)
        {
            Vraag v = new Vraag()
            {
                text = text,
                subCategorieId = subCategorieId,
                isActive = true,
            };
            addVraag(v);
        }

        //Gets a random question
        public Vraag getRandomVraag()
        {
            List<Vraag> vragen = getAlleVragen();
            return vragen[r.Next(vragen.Count)];
        }

        //Delete question via id
        public void deleteVraag(int id)
        {  
            //getVraag(id).isActive = false;
            db.Vraags.DeleteOnSubmit(getVraag(id));
            db.SubmitChanges();
        }

        //Delete question via question object
        public void deleteVraag(Vraag v)
        {
            deleteVraag(v.id);
        }

        //Updates the question corresponding with the given id with the given new details
        public void updateVraag(int vraagId, string nieuweText, int nieuwSubCategorieId)
        {
            Vraag v = getVraag(vraagId);
            v.text = nieuweText;
            v.SubCategorie = (from c in db.SubCategories where c.id == nieuwSubCategorieId select c).SingleOrDefault();
            db.SubmitChanges();
        }
    }
}
