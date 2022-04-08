using Microsoft.AspNetCore.Http;
using QuizLib.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPQuizApp
{
    public static class SessionHelper
    {
        public static void SetSessionString(ISession session, string key, string value)
        {
            session.Set(key, Encoding.ASCII.GetBytes(value));
        }

        public static string GetSessionString(ISession session, string key)
        {
            try
            {
                byte[] Bytes;
                session.TryGetValue(key, out Bytes);
                return Encoding.ASCII.GetString(Bytes);
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static Gebruiker GetLoggedinUser(ISession session)
        {
            int? userId = Convert.ToInt32(GetSessionString(session, "LoggedInUserId"));
            if (userId != null)
            {
                GebruikerContainer gc = new GebruikerContainer(DAOS.gebruikerDao);
                return gc.GetById((int)userId);
            }
            return null;
        }
    }
}
