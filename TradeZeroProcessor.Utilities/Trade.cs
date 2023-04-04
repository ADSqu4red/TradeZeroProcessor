using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeZeroProcessor.Utilities
{
    public class Trade
    {
        public string Account { get; set; }
        public DateTime TradeDate { get; set; }
        public DateTime SettlementDate { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Commission { get; set; }
        public decimal GrossProceeds { get; set; }
        public decimal NetProceeds { get; set; }

        private List<Transaction> transactions = new List<Transaction>();

        public void AddTransaction(Transaction transaction)
        {
            if (transactions.Count == 0)
            {
                Account = transaction.Account;
                TradeDate = transaction.TradeDate;
                SettlementDate = transaction.SettlementDate;
                Symbol = transaction.Symbol;
                Quantity = transaction.Quantity;
                Price = transaction.Price;
                Commission = transaction.Commission;
                GrossProceeds = transaction.GrossProceeds;
                NetProceeds = transaction.NetProceeds;
            }
            else
            {
                if (Symbol != transaction.Symbol)
                {
                    throw new ArgumentException("Transaction symbol does not match trade symbol");
                }
                if (Quantity > 0 && transaction.Quantity < 0)
                {
                    throw new ArgumentException("Cannot add short sale to long trade");
                }
                if (Quantity < 0 && transaction.Quantity > 0)
                {
                    throw new ArgumentException("Cannot add long purchase to short trade");
                }
                Quantity += transaction.Quantity;
                Commission += transaction.Commission;
                GrossProceeds += transaction.GrossProceeds;
                NetProceeds += transaction.NetProceeds;
            }
            transactions.Add(transaction);
        }
        public override string ToString()
        {
            return $"Trade on {TradeDate.ToShortDateString()} for {Quantity} shares of {Symbol} at ${Price} per share, net proceeds of ${NetProceeds} with ${Commission} commission";
        }
    }
}
