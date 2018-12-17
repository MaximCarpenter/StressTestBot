using System;
using System.Configuration;
using DataFeeder;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = ConfigurationManager.AppSettings["connectionString"];
            Console.WriteLine($"Started");
            var feeder = new FeederClient(connection);
            feeder.OperationEnd += OnOperationEnds;
            feeder.OperationStart += OnOperationStart;
            feeder.Feed(new ConcreteFeeder());
            Console.Read();
        }

        private static void OnOperationStart(object sender, OperationEventStartArgs e)
        {
            Console.WriteLine($"Operation starts for table {e.TableName} with amount of records {e.AmountOfRows}");
        }

        private static void OnOperationEnds(object sender, OperationEventArgs e)
        {
            Console.WriteLine($"{e.State} to add {e.AmountOfRows} records to {e.TableName} elapse {e.Milliseconds} milliseconds");
            if (e.IsError)
                Console.WriteLine($"{e.Error}");
            Console.WriteLine();
        }
    }
}
