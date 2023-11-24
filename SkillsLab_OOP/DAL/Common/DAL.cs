using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SkillsLab_OOP.DAL.Common
{
    public class DAL
    {
        // public const string connectionString = @"server=jedicarpooldbserver.database.windows.net;database=JEDICarpool_db;uid=wbpoc;pwd=sql@tfs2008";
        public const string CONNECTION_STRING = @"server=localhost;database=SkillsLabDB;uid=wbpoc;pwd=sql@tfs2008";

        public SqlConnection Connection;

        public DAL()
        {
            Connection = new SqlConnection(CONNECTION_STRING);
            OpenConnection();
        }

        public void OpenConnection()
        {
            try
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }

                Connection.Open();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void CloseConnection()
        {
            if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }
    }
}