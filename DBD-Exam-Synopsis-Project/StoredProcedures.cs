using ConsoleTables;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBD_Exam_Synopsis_Project
{
    internal class StoredProcedures : IStoredProcedures
    {
        public int CreatePerson(string PName)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                // Create command and set its properties
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_CreatePerson";
                command.CommandType = CommandType.StoredProcedure;

                // Add the DName input parameter and set its properties.
                SqlParameter PNameParam = new SqlParameter();
                PNameParam.ParameterName = "@PName";
                PNameParam.SqlDbType = SqlDbType.VarChar;
                PNameParam.Direction = ParameterDirection.Input;
                PNameParam.Value = PName;

                //Add the parameter @PName the parameters collection.
                command.Parameters.Add(PNameParam);

                // Add the output parameter.
                SqlParameter PIdParam = new SqlParameter("@PId", SqlDbType.Int);
                PIdParam.Direction = ParameterDirection.Output;

                command.Parameters.Add(PIdParam);

                // Open the connection and execute procedure
                connection.Open();
                int affectedRows = command.ExecuteNonQuery();

                int PId = (int)PIdParam.Value;

                return PId;
                
            }
        }

        public int CreatePersonFriend(int PId1, int PId2)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                // Create command and set its properties
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_CreatePersonFriend";
                command.CommandType = CommandType.StoredProcedure;

                // Add the PId1 input parameter and set its properties.
                SqlParameter PId1Param = new SqlParameter();
                PId1Param.ParameterName = "@PId1";
                PId1Param.SqlDbType = SqlDbType.Int;
                PId1Param.Direction = ParameterDirection.Input;
                PId1Param.Value = PId1;

                //Add the parameter @PId1 the parameters collection.
                command.Parameters.Add(PId1Param);

                // Add the PId2 input parameter and set its properties.
                SqlParameter PId2Param = new SqlParameter();
                PId2Param.ParameterName = "@PId2";
                PId2Param.SqlDbType = SqlDbType.Int;
                PId2Param.Direction = ParameterDirection.Input;
                PId2Param.Value = PId2;

                //Add the parameter @PId2 the parameters collection.
                command.Parameters.Add(PId2Param);
                
                // Open the connection and execute procedure
                connection.Open();
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows;
            }
        }

        public int DeletePerson(int PId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                // Create command and set its properties
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_DeletePerson";
                command.CommandType = CommandType.StoredProcedure;

                // Add the PId input parameter and set its properties.
                SqlParameter PIdParam = new SqlParameter();
                PIdParam.ParameterName = "@PId";
                PIdParam.SqlDbType = SqlDbType.Int;
                PIdParam.Direction = ParameterDirection.Input;
                PIdParam.Value = PId;

                //Add the parameter @PId the parameters collection.
                command.Parameters.Add(PIdParam);

                // Open the connection and execute procedure
                connection.Open();
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows;
            }
        }

        public void GetAllPersons()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                // Create command and set its properties
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_GetAllPersons";
                command.CommandType = CommandType.StoredProcedure;

                // Open the connection and execute procedure
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var table = new ConsoleTable();

                    if (reader.HasRows)
                    {
                        var columns = new List<string>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columns.Add(reader.GetName(i));
                        }

                        table.AddColumn(columns);

                        while (reader.Read())
                        {
                            table.AddRow(reader[0], reader[1]);
                        }

                        table.Write();
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }

            }
        
        }

        public void GetPerson(int PId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                // Create command and set its properties
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_GetPerson";
                command.CommandType = CommandType.StoredProcedure;

                // Add the PId input parameter and set its properties.
                SqlParameter PIdParam = new SqlParameter();
                PIdParam.ParameterName = "@PId";
                PIdParam.SqlDbType = SqlDbType.Int;
                PIdParam.Direction = ParameterDirection.Input;
                PIdParam.Value = PId;

                //Add the parameter @PId the parameters collection.
                command.Parameters.Add(PIdParam);

                // Open the connection and execute procedure
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var table = new ConsoleTable();

                    if (reader.HasRows)
                    {
                        var columns = new List<string>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columns.Add(reader.GetName(i));
                        }

                        table.AddColumn(columns);

                        while (reader.Read())
                        {
                            table.AddRow(reader[0], reader[1]);
                        }

                        table.Write();
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }

            }
        }

        public void GetAllFriendsOfPerson(string PName)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                // Create command and set its properties
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_GetAllFriendsOfPerson";
                command.CommandType = CommandType.StoredProcedure;

                // Add the PName input parameter and set its properties.
                SqlParameter PNameParam = new SqlParameter();
                PNameParam.ParameterName = "@PName";
                PNameParam.SqlDbType = SqlDbType.NVarChar;
                PNameParam.Direction = ParameterDirection.Input;
                PNameParam.Value = PName;

                //Add the parameter @PName the parameters collection.
                command.Parameters.Add(PNameParam);

                // Open the connection and execute procedure
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var table = new ConsoleTable();

                    if (reader.HasRows)
                    {
                        var columns = new List<string>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columns.Add(reader.GetName(i));
                        }

                        table.AddColumn(columns);

                        while (reader.Read())
                        {
                            table.AddRow(reader[0]);
                        }

                        table.Write();
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }

            }
        }

        public void GetAllFriendsOfPersonReciprocal(string PName)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                // Create command and set its properties
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_GetAllFriendsOfPersonReciprocal";
                command.CommandType = CommandType.StoredProcedure;

                // Add the PName input parameter and set its properties.
                SqlParameter PNameParam = new SqlParameter();
                PNameParam.ParameterName = "@PName";
                PNameParam.SqlDbType = SqlDbType.NVarChar;
                PNameParam.Direction = ParameterDirection.Input;
                PNameParam.Value = PName;

                //Add the parameter @PName the parameters collection.
                command.Parameters.Add(PNameParam);

                // Open the connection and execute procedure
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var table = new ConsoleTable();

                    if (reader.HasRows)
                    {
                        var columns = new List<string>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columns.Add(reader.GetName(i));
                        }

                        table.AddColumn(columns);

                        while (reader.Read())
                        {
                            table.AddRow(reader[0]);
                        }

                        table.Write();
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }

            }
        }

        public int UpdatePersonName(int PId, string PName)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                // Create command and set its properties
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_UpdatePersonName";
                command.CommandType = CommandType.StoredProcedure;

                // Add the PId input parameter and set its properties.
                SqlParameter PIdParam = new SqlParameter();
                PIdParam.ParameterName = "@PId";
                PIdParam.SqlDbType = SqlDbType.Int;
                PIdParam.Direction = ParameterDirection.Input;
                PIdParam.Value = PId;

                //Add the parameter @PId the parameters collection.
                command.Parameters.Add(PIdParam);

                // Add the PName input parameter and set its properties.
                SqlParameter PNameParam = new SqlParameter();
                PNameParam.ParameterName = "@PName";
                PNameParam.SqlDbType = SqlDbType.NVarChar;
                PNameParam.Direction = ParameterDirection.Input;
                PNameParam.Value = PName;

                //Add the parameter @PName the parameters collection.
                command.Parameters.Add(PNameParam);

                // Open the connection and execute procedure
                connection.Open();

                int affectedRows = command.ExecuteNonQuery();
                return affectedRows;
            }
        }

        public void usp_GetAllFriendsOfFriendsPerson(string PName)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                // Create command and set its properties
                SqlCommand command = new SqlCommand();
                
                command.Connection = connection;
                command.CommandText = "usp_GetAllFriendsOfFriendsOfPerson";
                command.CommandType = CommandType.StoredProcedure;

                // Add the PName input parameter and set its properties.
                SqlParameter PNameParam = new SqlParameter();
                PNameParam.ParameterName = "@PName";
                PNameParam.SqlDbType = SqlDbType.NVarChar;
                PNameParam.Direction = ParameterDirection.Input;
                PNameParam.Value = PName;

                //Add the parameter @PName the parameters collection.
                command.Parameters.Add(PNameParam);

                // Open the connection and execute procedure
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var table = new ConsoleTable();

                    if (reader.HasRows)
                    {
                        var columns = new List<string>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columns.Add(reader.GetName(i));
                        }

                        table.AddColumn(columns);

                        while (reader.Read())
                        {
                            table.AddRow(reader[0], reader[1]);
                        }

                        table.Write();
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }

            }
        }
    }
}
