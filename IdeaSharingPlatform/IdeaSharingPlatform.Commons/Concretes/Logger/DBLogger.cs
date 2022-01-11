using IdeaSharingPlatform.Commons.Abstracts;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace IdeaSharingPlatform.Commons.Concretes.Logger
{
    internal class DBLogger : LogBase
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        public override void Log(string message, bool isError)
        {
            lock (lockObj)
            {
                if (isError)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand("spInsertLog", con);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter("@ExceptionMessage", message);
                    command.Parameters.Add(param);
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
