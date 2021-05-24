using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.enums;

namespace TradingBot.model
{
    public class BalanceItem
    {
        public ECoins Coin { get; }

        public double Balance { get; set; }

        public BalanceItem(ECoins coin, double balance) 
        {
            Coin = coin;
            Balance = balance;
        }
    }
}
