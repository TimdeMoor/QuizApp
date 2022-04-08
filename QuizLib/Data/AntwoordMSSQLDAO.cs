using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Interface;

namespace QuizLib.Data
{
    public class AntwoordMSSQLDAO : IAntwoordDAO, IAntwoordContainerDAO
    {
        public List<AntwoordDTO> GetAll()
        {
            Db.TryOpenConnection();
            SqlCommand command = new SqlCommand("SELECT * " +
                                                "FROM Antwoord " +
                                                "WHERE isActive = 1", Db.conn);
            SqlDataReader reader = command.ExecuteReader();
            List<AntwoordDTO> results = new List<AntwoordDTO>();

            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    AntwoordDTO result = new AntwoordDTO();

                    result.Id = (int)reader["id"];
                    result.VraagId = (int)reader["vraagId"];
                    result.Text = (string)reader["text"];
                    result.IsActive = (bool)reader["isActive"];
                    result.IsCorrect = (bool)reader["isCorrect"];

                    results.Add(result);
                }
            }
            reader.Close();
            Db.TryCloseConnection();

            return results;
        }

        public AntwoordDTO GetById(int id)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("SELECT * " +
                                                "FROM Antwoord " +
                                                "WHERE isActive = 1" +
                                                "AND id = @id", Db.conn);

            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            AntwoordDTO result = new AntwoordDTO();
            if (reader.HasRows)
            {
                result.Id = (int)reader["id"];
                result.VraagId = (int)reader["vraagId"];
                result.Text = (string)reader["text"];
                result.IsActive = (bool)reader["isActive"];
                result.IsCorrect = (bool)reader["isCorrect"];
            }

            reader.Close();
            Db.TryCloseConnection();

            return result;
        }

        public void Save(AntwoordDTO a)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("INSERT INTO Antwoord(text, vraagId, isCorrect) " +
                                                "VALUES(@text, @vraagId, @isCorrect)", Db.conn);
            command.Parameters.AddWithValue("@text", a.Text);
            command.Parameters.AddWithValue("@vraagId", a.VraagId);
            command.Parameters.AddWithValue("@isCorrect", a.IsCorrect);

            command.ExecuteNonQuery();
            Db.TryCloseConnection();
        }

        public List<AntwoordDTO> GetByVraagId(int id)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("SELECT * " +
                                                "FROM Antwoord " +
                                                "WHERE isActive = 1 " +
                                                "AND vraagId = @id", Db.conn);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
            List<AntwoordDTO> results = new List<AntwoordDTO>();

            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    AntwoordDTO result = new AntwoordDTO();
                    result.Id = (int)reader["id"];
                    result.VraagId = (int)reader["vraagId"];
                    result.Text = (string)reader["text"];
                    result.IsActive = (bool)reader["isActive"];
                    result.IsCorrect = (bool)reader["isCorrect"];

                    results.Add(result);
                }
            }
            reader.Close();
            Db.TryCloseConnection();

            return results;
        }

        public AntwoordDTO GetCorrectAntwoordByVraagId(int id)
        {
            Db.TryOpenConnection();
            SqlCommand command = new SqlCommand("SELECT * " +
                                                "FROM Antwoord " +
                                                "WHERE isActive = 1 " +
                                                "AND vraagId = @id " +
                                                "AND isCorrect = 1", Db.conn);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            AntwoordDTO result = new AntwoordDTO();
            if (reader.HasRows)
            {
                result.Id = (int)reader["id"];
                result.VraagId = (int)reader["vraagId"];
                result.Text = (string)reader["text"];
                result.IsActive = (bool)reader["isActive"];
                result.IsCorrect = (bool)reader["isCorrect"];
            }

            reader.Close();
            Db.TryCloseConnection();

            return result;
        }

        public void Delete(AntwoordDTO dto, bool harddelete = false)
        {
            Db.TryOpenConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = Db.conn;

            if (harddelete)
            {
                command.CommandText = "DELETE FROM Antwoord " +
                                      "WHERE id = @id";
            }
            else
            {
                command.CommandText = "UPDATE Antwoord " +
                                      "SET isActive = 0 " +
                                      "WHERE id = @id";
            }

            command.Parameters.AddWithValue("@id", dto.Id);
            command.ExecuteNonQuery();

            Db.TryCloseConnection();
        }

        public int GetCorrectAntwoordIdBySelectedAntwoordId(int id)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("SELECT id " + 
                                                "FROM Antwoord " + 
                                                "WHERE Antwoord.vraagId = (SELECT vraagId " + 
                                                                          "FROM Antwoord " + 
                                                                          "WHERE id = @id )" + 
                                                "AND isCorrect = 1;", Db.conn);
            command.Parameters.AddWithValue("@id", id);
            var correctId = command.ExecuteScalar();

            Db.TryCloseConnection();

            return Convert.ToInt32(correctId);
        }

        public void SetCorrect(int AntwoordId)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("UPDATE Antwoord " +
                                                "Set isCorrect = 1 " +
                                                "WHERE id = @id", Db.conn);
            command.Parameters.AddWithValue("@id", AntwoordId);
            command.ExecuteNonQuery();
            Db.TryCloseConnection();
        }

        public void SetIncorrect(int AntwoordId)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("UPDATE Antwoord " +
                                                "Set isCorrect = 0 " +
                                                "WHERE id = @id", Db.conn);
            command.Parameters.AddWithValue("@id", AntwoordId);
            command.ExecuteNonQuery();
            Db.TryCloseConnection();
        }

        public void ChangeAntwoordText(int id, string newText)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("", Db.conn);
            command.CommandText = "UPDATE Antwoord " +
                                  "SET text = @newText " +
                                  "WHERE id = @id";
            command.Parameters.AddWithValue("@newText", newText);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();

            Db.TryCloseConnection();
        }
    }
}
