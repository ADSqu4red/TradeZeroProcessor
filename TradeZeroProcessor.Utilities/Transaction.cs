using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeZeroProcessor.Utilities
{
    public class Transaction
    {
        public string Account { get; set; }
        public DateTime TradeDate { get; set; }
        public DateTime SettlementDate { get; set; }
        public string Currency { get; set; }
        public int Type { get; set; }
        public string Side { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public decimal Commission { get; set; }
        public decimal SEC { get; set; }
        public decimal TAF { get; set; }
        public decimal NSCC { get; set; }
        public decimal Nasdaq { get; set; }
        public decimal ECNRemove { get; set; }
        public decimal ECNAdd { get; set; }
        public decimal GrossProceeds { get; set; }
        public decimal NetProceeds { get; set; }
        public string ClearingBroker { get; set; }
        public string Liq { get; set; }
        public string Note { get; set; }
    }
}
