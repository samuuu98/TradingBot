using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.enums;

namespace TradingBot.model
{
    public class Order
    {
        public ECoins CoinFrom { get; }
        public ECoins CoinTo { get; }

        public double ValueFrom { get; }
        public double ValueTo { get; }

        /// <summary>
        /// Valore assoluto della tassa
        /// </summary>
        public double Tax { get; }

        public double Change { get; }

        //TODO : trovare un modo migliore per separare acquisti e vendite
        public bool IsBuy { get; }

        public Order(ECoins coinFrom, double valueFrom, ECoins coinTo, double valueTo, double change, double tax, bool isBuy) 
        {
            CoinFrom = coinFrom;
            ValueFrom = valueFrom;
            CoinTo = coinTo;
            ValueTo = valueTo;
            Change = change;
            Tax = tax;
            IsBuy = isBuy;
        }
    }
}
