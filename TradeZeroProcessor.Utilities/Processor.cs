using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeZeroProcessor.Utilities
{
    public class Processor
    {

        public List<Transaction> ReadTransactionsFromCsv(string filePath)
        {
            List<Transaction> transactions = new List<Transaction>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                // Skip the first row (header) of the csv file
                reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    Transaction transaction = new Transaction
                    {
                        Account = values[0],
                        TradeDate = DateTime.ParseExact(values[1], "M/d/yyyy", CultureInfo.InvariantCulture),
                        SettlementDate = DateTime.ParseExact(values[2], "M/d/yyyy", CultureInfo.InvariantCulture),
                        Currency = values[3],
                        Type = int.Parse(values[4]),
                        Side = values[5],
                        Symbol = values[6],
                        Quantity = int.Parse(values[7]),
                        Price = decimal.Parse(values[8]),
                        ExecutionTime = TimeSpan.ParseExact(values[9], "h\\:mm\\:ss", CultureInfo.InvariantCulture),
                        Commission = decimal.Parse(values[10]),
                        SEC = decimal.Parse(values[11]),
                        TAF = decimal.Parse(values[12]),
                        NSCC = decimal.Parse(values[13]),
                        Nasdaq = decimal.Parse(values[14]),
                        ECNRemove = decimal.Parse(values[15]),
                        ECNAdd = decimal.Parse(values[16]),
                        GrossProceeds = decimal.Parse(values[17]),
                        NetProceeds = decimal.Parse(values[18]),
                        ClearingBroker = values[19],
                        Liq = values[20],
                        Note = values[21]
                    };

                    transactions.Add(transaction);
                }
            }

            return transactions;
        }

        public List<Trade> MatchTrades(List<Transaction> transactions)
        {
            // Sort transactions by TradeDate and ExecutionTime
            transactions.Sort((t1, t2) =>
            {
                int tradeDateComparison = t1.TradeDate.CompareTo(t2.TradeDate);
                if (tradeDateComparison == 0)
                {
                    return t1.ExecutionTime.CompareTo(t2.ExecutionTime);
                }
                return tradeDateComparison;
            });

            // Create a dictionary to keep track of trades that have already been matched
            Dictionary<string, Trade> matchedTrades = new Dictionary<string, Trade>();

            // Iterate through each transaction and match it to an existing trade or create a new trade
            foreach (Transaction transaction in transactions)
            {
                string tradeId = GetTradeId(transaction);

                if (matchedTrades.ContainsKey(tradeId))
                {
                    matchedTrades[tradeId].AddTransaction(transaction);
                }
                else
                {
                    Trade newTrade = new Trade();
                    newTrade.AddTransaction(transaction);
                    matchedTrades.Add(tradeId, newTrade);
                }
            }

            // Return the list of matched trades
            return matchedTrades.Values.ToList();
        }

        static string GetTradeId(Transaction transaction)
        {
            // Create a unique trade ID based on the Account, TradeDate, SettlementDate, and Symbol fields
            return $"{transaction.Account}_{transaction.TradeDate:yyyyMMdd}_{transaction.SettlementDate:yyyyMMdd}_{transaction.Symbol}";
        }   

    }
}
