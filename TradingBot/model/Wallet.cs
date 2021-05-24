using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.enums;

namespace TradingBot.model
{
    public class Wallet
    {

        public List<BalanceItem> Spot { get; }

        public Wallet(ECoins[] coins, double[] balances) 
        {
            Spot = new List<BalanceItem>();

            if (coins.Length == balances.Length)
            {
                foreach (var coin in coins)
                {
                    Spot.Add(new BalanceItem(coin, balances.ElementAt(coins.ToList().IndexOf(coin))));
                }
            }
        }

        public bool AddCoin(ECoins coin, double value) 
        {
            if (!Spot.Any(s => s.Coin == coin))
            {
                Spot.Add(new BalanceItem(coin, value));

                return true;
            }
            else 
            {
                Spot.FirstOrDefault(s => s.Coin == coin).Balance += value;
                return true;
            }
        }

        public bool Convert(ECoins coinFrom, double valueFrom, ECoins coinTo, double valueTo) 
        {
            var itemFrom = Spot.FirstOrDefault(s => s.Coin == coinFrom);
            if (itemFrom != null)
                if (itemFrom.Balance >= valueFrom)
                {
                    itemFrom.Balance -= valueFrom;

                    var itemTo = Spot.FirstOrDefault(s => s.Coin == coinTo);
                    if (itemTo != null)
                    {
                        itemFrom.Balance -= valueFrom;
                    }
                    else
                    {
                        AddCoin(coinTo, valueTo);
                    }
                }

            return false;
        }
    }
}
