using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Курсовой_проект.Services
{
    public class DBConnection : IDisposable
    {
        public SqlConnection myConnection { get; set; }
        public string ServerConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            }
        }

        public DBConnection()
        {
            ConnectionOpen();
        }
        ~DBConnection()
        {
            ConnectionClose();
        }

        private void ConnectionOpen()
        {
            myConnection = new SqlConnection(ServerConnectionString);
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void ConnectionClose()
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Dispose()
        {
            ConnectionClose();
        }
    }
}
