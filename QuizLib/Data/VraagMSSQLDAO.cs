using QuizLib.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuizLib.Data
{
    public class VraagMSSQLDAO : IVraagDAO, IVraagContainerDAO
    {
        //Gets all the vragen from the database
        public List<VraagDTO> GetAll()
        {
            Db.TryOpenConnection();
            SqlDataReader reader = new SqlCommand("SELECT * FROM Vraag WHERE isActive = 1", Db.conn).ExecuteReader();

            List<VraagDTO> results = new List<VraagDTO>();

            while (reader.Read())
            {
                results.Add(new VraagDTO()
                {
                    Id = (int)reader["id"],
                    Text = (string)reader["text"],
                    SubCategorieId = (int)reader["subCategorieId"],
                });
            }
            reader.Close();

            Db.TryCloseConnection();

            return results;
        }

        public VraagDTO GetById(int id)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("SELECT * FROM Vraag WHERE id = @id", Db.conn);
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            VraagDTO result = new VraagDTO();
            if (reader.HasRows)
            {
                result.Id = (int)reader["id"];
                result.Text = (string)reader["text"];
                result.SubCategorieId = (int)reader["subCategorieId"];
            };

            reader.Close();
            Db.TryCloseConnection();

            return result;
        }

        public void Save(VraagDTO v)
        {
            Db.TryOpenConnection();

            SqlCommand command =
                new SqlCommand("INSERT INTO Vraag(text, subCategorieId) " +
                               "VALUES(@text, @categorieId)"
                               , Db.conn);

            //Parameter binding to prevent injection
            command.Parameters.AddWithValue("@text", v.Text);
            command.Parameters.AddWithValue("@categorieId", v.SubCategorieId);

            command.ExecuteNonQuery();

            Db.TryCloseConnection();
        }

        //Deletes vraag from database
        public void Delete(VraagDTO vraag, bool harddelete = false)
        {
            Db.TryOpenConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = Db.conn;

            //Check if vraag has to be deleted or deactivated
            if (harddelete)
            {
                command.CommandText = "DELETE FROM Vraag " +
                                      "WHERE id = @id";
            }
            else
            {
                command.CommandText = "UPDATE Vraag " +
                                      "SET isActive = 0 " +
                                      "WHERE id = @id";
            }

            command.Parameters.AddWithValue("@id", vraag.Id);
            command.ExecuteNonQuery();

            Db.TryCloseConnection();
        }

        public VraagDTO GetByText(string vraagText)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("SELECT * " +
                                                "FROM Vraag " +
                                                "WHERE text LIKE @text", Db.conn);

            command.Parameters.AddWithValue("@text", vraagText);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            VraagDTO result = new VraagDTO();
            if (reader.HasRows)
            {
                result.Id = (int)reader["id"];
                result.Text = (string)reader["text"];
                result.SubCategorieId = (int)reader["subCategorieId"];
            };

            reader.Close();
            Db.TryCloseConnection();

            return result;
        }

        public List<VraagDTO> GetByCategorieId(int id)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("SELECT * " +
                                                "FROM Vraag " +
                                                "WHERE subCategorieId = @id " +
                                                "AND isActive = 1;"
                                                , Db.conn);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();

            List<VraagDTO> results = new List<VraagDTO>();

            while (reader.Read())
            {
                results.Add(new VraagDTO()
                {
                    Id = (int)reader["id"],
                    Text = (string)reader["text"],
                    SubCategorieId = (int)reader["subCategorieId"],
                });
            }
            reader.Close();

            Db.TryCloseConnection();

            return results;
        }

        public void ChangeVraagText(int id, string newText)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("", Db.conn);
            command.CommandText = "UPDATE Vraag " +
                                  "SET text = @newText " +
                                  "WHERE id = @id";
            command.Parameters.AddWithValue("@newText", newText);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();

            Db.TryCloseConnection();
        }

        public int GetVraagIdByAntwoordId(int id)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("", Db.conn);
            command.CommandText = "SELECT Vraag.id " +
                                  "FROM Vraag " +
                                  "LEFT JOIN Antwoord " +
                                  "ON Vraag.id = Antwoord.vraagId " +
                                  "WHERE Vraag.isActive = 1 " +
                                  "AND Antwoord.id = @id";
            command.Parameters.AddWithValue("@id", id);
            int vraagid = Convert.ToInt32(command.ExecuteScalar());
            return vraagid;
        }
    }
}
