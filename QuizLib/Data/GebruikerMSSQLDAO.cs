using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Interface;

namespace QuizLib.Data
{
    //TODO: Use error handling
    public class GebruikerMSSQLDAO : IGebruikerDAO, IGebruikerContainerDAO
    {
        public List<GebruikerDTO> GetAll()
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = Db.conn;
            command.CommandText = "SELECT * " +
                                  "FROM Gebruiker " +
                                  "WHERE isActive = 1";

            SqlDataReader reader = command.ExecuteReader();

            List<GebruikerDTO> gebruikerDTOs = new List<GebruikerDTO>();
            while (reader.Read())
            {
                gebruikerDTOs.Add(new GebruikerDTO()
                {
                    Id = (int)reader["id"],
                    Naam = (string)reader["naam"],
                    Wachtwoord = Base64Decode((string)reader["wachtwoord"]),
                    Email = (string)reader["email"],
                    IsAdmin = (bool)reader["isAdmin"],
                });
            }
            reader.Close();
            Db.TryCloseConnection();
            return gebruikerDTOs;
        }

        public GebruikerDTO GetById(int id)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = Db.conn;
            command.CommandText = "SELECT * " +
                                  "FROM Gebruiker " +
                                  "WHERE isActive = 1" +
                                  "AND id = @id";
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            GebruikerDTO gebruikerDTO = new GebruikerDTO()
            {
                Id = (int)reader["id"],
                Naam = (string)reader["naam"],
                Wachtwoord = Base64Decode((string)reader["wachtwoord"]),
                Email = (string)reader["email"],
                IsAdmin = (bool)reader["isAdmin"],
            };

            reader.Close();
            Db.TryCloseConnection();

            return gebruikerDTO;
        }

        public bool Save(GebruikerDTO dto)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand();

            command.Connection = Db.conn;
            command.CommandText = "INSERT INTO Gebruiker(naam, wachtwoord, email, isAdmin) " +
                                  "VALUES(@naam, @wachtwoord, @email, @isAdmin)";

            command.Parameters.AddWithValue("@naam", dto.Naam);
            command.Parameters.AddWithValue("@wachtwoord", Base64Encode(dto.Wachtwoord));
            command.Parameters.AddWithValue("@email", dto.Email);
            command.Parameters.AddWithValue("@isAdmin", dto.IsAdmin);

            command.ExecuteNonQuery();

            Db.TryCloseConnection();

            return true;
        }

        public bool Delete(GebruikerDTO dto, bool harddelete = false)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = Db.conn;

            if (harddelete)
            {
                command.CommandText = "DELETE FROM Gebruiker " +
                                      "WHERE id = @id";
            }
            else
            {
                command.CommandText = "UPDATE Gebruiker " +
                                      "SET isActive = 0 " +
                                      "WHERE id = @id";
            }

            command.ExecuteNonQuery();
            Db.TryCloseConnection();
            return true;
        }

        private string Base64Encode(string text)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        private string Base64Decode(string base64Text)
        {
            byte[] base64Bytes = Convert.FromBase64String(base64Text);
            return Encoding.UTF8.GetString(base64Bytes);
        }

        public GebruikerDTO CheckLogin(GebruikerDTO user)
        {
            Db.TryOpenConnection();

            if (user.Naam != null && user.Wachtwoord != null)
            {
                SqlCommand command = new SqlCommand("SELECT * " +
                                                    "FROM Gebruiker " +
                                                    "WHERE Naam = @Naam " +
                                                    "AND Wachtwoord = @Ww", Db.conn);
                command.Parameters.AddWithValue("@Naam", user.Naam);
                command.Parameters.AddWithValue("@Ww", user.Wachtwoord);

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                GebruikerDTO dto = new GebruikerDTO();
                if (reader.HasRows)
                {
                    dto.Id = (int)reader["id"];
                    dto.Naam = (string)reader["naam"];
                    dto.Wachtwoord = Base64Decode((string)reader["wachtwoord"]);
                    dto.Email = (string)reader["email"];
                    dto.IsAdmin = (bool)reader["isAdmin"];
                }
                reader.Close();
                Db.TryCloseConnection();

                return dto;
            }
            return new GebruikerDTO();
        }

        public bool PromoteUser(int id)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = Db.conn;

            command.CommandText = "UPDATE Gebruiker " +
                                  "SET isAdmin = 1 " +
                                  "WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            Db.TryCloseConnection();
            return true;
        }

        public bool DemoteUser(int id)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = Db.conn;

            command.CommandText = "UPDATE Gebruiker " +
                                  "SET isAdmin = 0 " +
                                  "WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            Db.TryCloseConnection();
            return true;
        }
    }
}
