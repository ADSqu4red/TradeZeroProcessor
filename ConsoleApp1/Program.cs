

using TradeZeroProcessor.Utilities;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            // Import your csv file from tradezero
            string filePath = "YOUR CSV FILE FROM TRADEZERO";

            // Create an instance of the processor
            Processor processor = new Processor();
            // Create an instance of the Exporter
            Exporter exporter = new Exporter();

            List<Transaction> transactions = processor.ReadTransactionsFromCsv(filePath);
            List<Trade> trades = processor.MatchTrades(transactions);

            // Export the matched trades to an excel file.
            exporter.ExportToExcel(trades, "C:\\temp\\trades.xlsx");
        }
    }
}