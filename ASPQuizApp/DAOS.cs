using QuizLib.Data;
using QuizLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPQuizApp
{
    public static class DAOS
    {
        public static VraagMSSQLDAO vraagDao = new VraagMSSQLDAO();
        public static AntwoordMSSQLDAO antwoordDao = new AntwoordMSSQLDAO();
        public static SubCategorieMSSQLDAO subCategorieDao = new SubCategorieMSSQLDAO();
        public static GebruikerMSSQLDAO gebruikerDao = new GebruikerMSSQLDAO();
    }
}
