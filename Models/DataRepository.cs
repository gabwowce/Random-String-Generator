using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MySql.Data.MySqlClient;

namespace Random_String_Generator.Models
{
    public class DataRepository
    {
        private string connectionString;

        public DataRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["MyConnectionToDB"].ConnectionString;
        }

        public void SaveGeneratedData(GeneratedData data)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                    INSERT INTO GeneratedDat (ThreadID, Time, Data)
                    VALUES (@ThreadID, @Time, @Data);";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ThreadID", data.ThreadID);
                        cmd.Parameters.AddWithValue("@Time", data.Time);
                        cmd.Parameters.AddWithValue("@Data", data.Data);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or rethrow it
                throw new Exception("Error saving generated data to the database.", ex);
            }
        }
    }

}

   
