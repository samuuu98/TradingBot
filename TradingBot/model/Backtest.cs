using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.enums;

namespace TradingBot.model
{
    public class Backtest
    {
        public Wallet Wallet { get; set; }

        public Index Index { get; set; }

        //Commissione percentuale 
        public double Tax { get; set; }

        public List<Order> OrderList { get; }

        public Backtest(Wallet wallet, Index index, double tax = 0) 
        {
            OrderList = new List<Order>();
            Wallet = wallet;
            Index = index;
            Tax = tax;
            index.OnSignal += Index_OnSignal;
        }

        private void Index_OnSignal(int sig)
        {
            var couple = Index._Chart.CouplesCoins.FirstOrDefault();
            if (sig == 1)
            {
                //var spotFrom = Wallet.Spot.FirstOrDefault(s => s.Coin == couple.Item2);
                //if (spotFrom != null)
                ConvertBuy(couple.Item2, 0, couple.Item1);
            }
            else if(sig == 0)
            {
                //var spotFrom = Wallet.Spot.FirstOrDefault(s => s.Coin == couple.Item1);
                //if (spotFrom != null)
                    ConvertSell(couple.Item1, 0, couple.Item2);
            }
        }

        public void ConvertSell(ECoins coinFrom, double value, ECoins coinTo)
        {
            if (App.Current is App app)
            {
                //var change = app.GetCurrentValue(Index._Chart.Name);
                var change = this.Index._Chart.GetCurrentValue();
                //var valueTo = value * change * (1 - Tax);
                //var tax = value * change * Tax;
                // Order(coinFrom, value, coinTo, valueTo, change, tax, false);
                Order(coinFrom, value, coinTo, 0, change, 0, false);
            }

        }

        public void ConvertBuy(ECoins coinFrom, double value, ECoins coinTo)
        {
            if (App.Current is App app)
            {
                var change = this.Index._Chart.GetCurrentValue();
                //var valueTo = value / change * (1 - Tax);
                //var tax = value / change * Tax;
                //Order(coinFrom, value, coinTo, valueTo, change, tax, true);
                Order(coinFrom, value, coinTo, 0, change, 0, true);
            }
        }

        private void Order(ECoins coinFrom, double valueFrom, ECoins coinTo, double valueTo, double change, double tax, bool isBuy)
        {
            //var res = Wallet.Convert(coinFrom, valueFrom, coinTo, valueTo);
            //if(res)
                OrderList.Add(new Order(coinFrom, valueFrom, coinTo, valueTo, change, tax, true));

            Console.WriteLine(isBuy ? "--- BUY ---" : "--- SELL ---" + $"Change {change} {this.Index.IndexChart.Symbol}\nFrom: {valueFrom} {coinFrom}\nTo: {valueTo} {coinTo}\nTax: {tax}");
        }
    }
}
