using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace api.Util
{
    public class DBUtil
    {

        public SqlConnection GetInterfaceDBConnection()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("InterfaceConnectionString");
            return new SqlConnection(connectionString);
        }

    }
}