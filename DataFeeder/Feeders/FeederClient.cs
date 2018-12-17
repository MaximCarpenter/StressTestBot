using System;
using System.Data.SqlClient;
using System.Diagnostics;
using Feeders;

namespace DataFeeder
{
    public class FeederClient
    {
        private readonly string _connectionString;
        public event EventHandler<OperationEventArgs> OperationEnd;
        public event EventHandler<OperationEventStartArgs> OperationStart;
        protected virtual void OnOperationEnd(OperationEventArgs e)
        {
            OperationEnd?.Invoke(this, e);
        }
        protected virtual void OnOperationStart(OperationEventStartArgs e)
        {
            OperationStart?.Invoke(this, e);
        }

        public FeederClient(string connection)
        {
            _connectionString = connection;
        }

        public void Feed(IDataFeeder dataFeeder)
        {
            var queryString = dataFeeder.Next();
            while (!dataFeeder.End)
            {
                var stopwatch = Start(dataFeeder);
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(queryString, connection);
                    try
                    {
                        connection.Open();
                        var reader = command.ExecuteReader();
                        reader.Close();
                        End(dataFeeder, stopwatch, null);
                    }
                    catch (Exception ex)
                    {
                        End(dataFeeder, stopwatch, ex.Message);
                    }

                    stopwatch.Reset();
                    queryString = dataFeeder.Next();
                }
            }
        }

        private Stopwatch Start(IDataFeeder dataFeeder)
        {
            OnOperationStart(new OperationEventStartArgs
            {
                AmountOfRows = dataFeeder.CurrentAmountOfRows,
                TableName = dataFeeder.CurrentTableName
            });
            var stopwatch = Stopwatch.StartNew();
            return stopwatch;
        }

        private void End(IDataFeeder dataFeeder, Stopwatch stopwatch, string message)
        {
            stopwatch.Stop();
            OnOperationEnd(new OperationEventArgs
            {
                AmountOfRows = dataFeeder.CurrentAmountOfRows,
                TableName = dataFeeder.CurrentTableName,
                State = string.IsNullOrEmpty(message) ? "Success" : "Error",
                Error = message,
                Milliseconds = stopwatch.ElapsedMilliseconds
            });
        }
    }
}
