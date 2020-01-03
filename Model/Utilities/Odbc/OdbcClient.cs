using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.Odbc
{
    public class OdbcClient
    {
        private readonly OdbcConnection _odbcConnection;

        public OdbcClient()
        {
            var connectionString = "Driver={PostgreSQL UNICODE}; " +
                                   "Server=postgresql.wmi.amu.edu.pl; " +
                                   "Port=5432; " +
                                   "Database=s413656_recipesearch; " +
                                   "Uid=s413656_recipesearch; " +
                                   "Pwd=xesonsfyingedsc;" +
                                   "sslmode=require;";

            _odbcConnection = new OdbcConnection(connectionString);
            Open();
        }
        public OdbcConnection GetConnection()
        {
            return _odbcConnection;
        }

        public void Open()
        {
            try
            {
                _odbcConnection.Open();

            }
            catch (Exception e)
            {
                Close();
                Console.WriteLine(e.Message);
                throw;
            }

        }

        public void Close()
        {
            _odbcConnection.Close();
        }
    }
}
