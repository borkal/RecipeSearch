using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Model.Utilities.Odbc
{
    public class OdbcManager : IDisposable
    {

        private OdbcClient _odbcClient;
        private OdbcCommand _odbcCommand;
        private static readonly ILog _log = LogManager.GetLogger(typeof(OdbcManager));

        internal void BeginNoTransaction()
        {
            _odbcClient = new OdbcClient();
            _odbcCommand = new OdbcCommand();

            _odbcCommand.Connection = _odbcClient.GetConnection();
        }

        internal void BeginTransaction()
        {
            _odbcClient = new OdbcClient();
            _odbcCommand = new OdbcCommand();

            var transaction = _odbcClient.GetConnection().BeginTransaction();

            _odbcCommand.Connection = _odbcClient.GetConnection();
            _odbcCommand.Transaction = transaction;
        }

        public OdbcDataReader ExecuteReadQuery(string query)
        {
            BeginNoTransaction();
            _odbcCommand.Connection = _odbcClient.GetConnection();
            _odbcCommand.CommandText = query;
            var reader = _odbcCommand.ExecuteReader();
            Dispose();
            return reader;
        }

        public void ExecuteUpdateQuery(string query)
        {
            _log.Info($"(Set the query for inserting data, query: {query}");
            BeginTransaction();
            _odbcCommand.CommandText = query;//.Replace("'","");
            var updatedRows = _odbcCommand.ExecuteReader();
            _log.Info($"(The count of the updated rows: {updatedRows})");
            Commit();
            Dispose();
        }

        public void Commit()
        {
            _odbcCommand.Transaction.Commit();
        }

        public void Dispose()
        {
            _odbcCommand?.Dispose();
            _odbcClient.Close();
        }

        public void RollBack()
        {
            _odbcCommand.Transaction.Rollback();
        }
    }
}
