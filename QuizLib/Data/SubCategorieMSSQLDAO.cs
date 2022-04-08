using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizLib.Interface;
using QuizLib.Logic;
using System.Data.SqlClient;
using System.Data;

namespace QuizLib.Data
{
    public class SubCategorieMSSQLDAO : ISubCategorieDAO, ISubCategorieContainerDAO
    {
        public SubCategorieDTO GetByVraagId(int vraagId)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("SELECT * " +
                                                "FROM SubCategorie " +
                                                "LEFT JOIN Vraag " +
                                                "ON Vraag.subCategorieId = SubCategorie.id " +
                                                "WHERE Vraag.id = @vraagId", Db.conn);
            command.Parameters.AddWithValue("@vraagId", vraagId);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            SubCategorieDTO result = new SubCategorieDTO();
            if (reader.HasRows)
            {
                result.Id = (int)reader["id"];

                if (reader.IsDBNull("parentSubCategorieId"))
                {
                    result.ParentSubCategorieId = null;
                }
                else
                {
                    result.ParentSubCategorieId = (int)reader["parentSubCategorieId"];
                }

                result.Naam = (string)reader["naam"];
                result.Beschrijving = (string)reader["beschrijving"];
                result.IsActive = (bool)reader["isActive"];
            }


            reader.Close();
            Db.TryCloseConnection();
            return result;
        }

        public List<SubCategorieDTO> GetAll()
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("SELECT * " +
                                                "FROM SubCategorie " +
                                                "WHERE isActive = 1"
                                                , Db.conn);

            SqlDataReader reader = command.ExecuteReader();
            List<SubCategorieDTO> results = new List<SubCategorieDTO>();

            while (reader.Read())
            {
                SubCategorieDTO result = new SubCategorieDTO();
                result.Id = (int)reader["id"];

                if (reader.IsDBNull("parentSubCategorieId"))
                {
                    result.ParentSubCategorieId = null;
                }
                else
                {
                    result.ParentSubCategorieId = (int)reader["parentSubCategorieId"];
                }

                result.Naam = (string)reader["naam"];
                result.Beschrijving = (string)reader["beschrijving"];
                result.IsActive = (bool)reader["isActive"];

                results.Add(result);
            }

            reader.Close();
            Db.TryCloseConnection();
            return results;
        }

        public void Save(SubCategorieDTO dto)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("", Db.conn);

            if (dto.ParentSubCategorieId != null)
            {
                command.CommandText = "INSERT INTO SubCategorie(naam, beschrijving, parentSubCategorieId) " +
                                      "VALUES(@naam, @beschrijving, @parentId)";
                command.Parameters.AddWithValue("@parentId", dto.ParentSubCategorieId);
            }
            else
            {
                command.CommandText = "INSERT INTO SubCategorie(naam, beschrijving) " +
                                      "VALUES(@naam, @beschrijving)";
            }

            command.Parameters.AddWithValue("@naam", dto.Naam);
            command.Parameters.AddWithValue("@beschrijving", dto.Beschrijving);

            command.ExecuteNonQuery();

            Db.TryCloseConnection();
        }

        public SubCategorieDTO GetById(int id)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("SELECT * " +
                                                "FROM SubCategorie " +
                                                "WHERE id = @id", Db.conn);
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            SubCategorieDTO result = new SubCategorieDTO();
            result.Id = (int)reader["id"];

            if (reader.IsDBNull("parentSubCategorieId"))
            {
                result.ParentSubCategorieId = null;
            }
            else
            {
                result.ParentSubCategorieId = (int)reader["parentSubCategorieId"];
            }

            result.Naam = (string)reader["naam"];
            result.Beschrijving = (string)reader["beschrijving"];
            result.IsActive = (bool)reader["isActive"];

            reader.Close();
            Db.TryCloseConnection();

            return result;
        }

        public SubCategorieDTO GetByName(string name)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("SELECT * " +
                                                "FROM SubCategorie " +
                                                "WHERE naam = @naam", Db.conn);
            command.Parameters.AddWithValue("@naam", name);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            SubCategorieDTO result = new SubCategorieDTO();
            result.Id = (int)reader["id"];

            if (reader.IsDBNull("parentSubCategorieId"))
            {
                result.ParentSubCategorieId = null;
            }
            else
            {
                result.ParentSubCategorieId = (int)reader["parentSubCategorieId"];
            }

            result.Naam = (string)reader["naam"];
            result.Beschrijving = (string)reader["beschrijving"];
            result.IsActive = (bool)reader["isActive"];

            reader.Close();
            Db.TryCloseConnection();

            return result;
        }

        public void Delete(SubCategorieDTO subCategorie, bool harddelete = false)
        {
            Db.TryOpenConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = Db.conn;

            if (harddelete)
            {
                command.CommandText = "DELETE FROM SubCategorie " +
                                      "WHERE id = @id";
            }
            else
            {
                command.CommandText = "UPDATE SubCategorie " +
                                      "SET isActive = 0 " +
                                      "WHERE id = @id";
            }

            command.Parameters.AddWithValue("@id", subCategorie.Id);
            command.ExecuteNonQuery();

            Db.TryCloseConnection();
        }

        public List<SubCategorieDTO> GetChildrenByParentId(int parentId)
        {
            Db.TryOpenConnection();

            SqlCommand command = new SqlCommand("", Db.conn);
            command.CommandText = "SELECT child.id " +
                                  "FROM SubCategorie as child " +
                                  "LEFT JOIN SubCategorie as parent " +
                                  "ON child.parentSubCategorieId = parent.Id " +
                                  "WHERE parent.id = @parentId;";
            command.Parameters.AddWithValue("@parentId", parentId);

            SqlDataReader reader = command.ExecuteReader();

            List<int> childIds = new List<int>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    childIds.Add(Convert.ToInt32((int)reader["id"]));
                }
            }
            reader.Close();

            List<SubCategorieDTO> children = new List<SubCategorieDTO>();
            foreach(int childId in childIds)
            {
                children.Add(GetById(childId));
            }

            
            Db.TryCloseConnection();

            return children;
        }
    }
}
