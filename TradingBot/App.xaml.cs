using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TradingBot.enums;
using TradingBot.manager;
using TradingBot.model;
using TradingBOT.manager;
using TradingBOT.model;

namespace TradingBot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ChartManager ChartM { get; set; }

        public List<Chart> ChartList { get; set; }

        public List<Chart> CurrentValueList { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ChartList = new List<Chart>();
            CurrentValueList = new List<Chart>();
            var clientManager = new ClientManager();
            ChartM = new ChartManager(clientManager);

            CreateChart();


            //foreach (var chart in ChartList.Union(CurrentValueList).Distinct())
            //{
            //    if (chart is CandleChart candleC)
            //    {
            //        //var now = DateTime.Now;
            //        //ChartM.AddHistoricalData(candleC, new DateTime(now.Year, now.Month, now.Day, now.Hour - 5, now.Minute, now.Second), new DateTime(now.Year, now.Month, now.Day, now.Hour - 1, now.Minute, now.Second));
            //        //ChartM.SubscribeToBookTickerUpdates(candleC);
            //    }
            //    else if (chart is StockChart stockC)
            //        //ChartM.SubscribeToBookTickerUpdates(stockC);
            //}
        }

        public void CreateChart()
        {
            var stockChart1 = new StockChart("BTCUSDT", ESymbol.BTCEUR, true);
            var candleChart1 = new CandleChart("BTCEUR", ESymbol.BTCEUR, KlineInterval.OneMinute);
            //var ema21 = new EMAIndex(candleChart1, 21);
            var macd = new MACDIndex(candleChart1);
            var wallet = new Wallet(new ECoins[] { ECoins.EUR }, new double[] { 10000 });
            var backtest = new Backtest(wallet, macd);

            var now = DateTime.Now;
            //TODO MEttere gli intervalli di tempo giusti
            ChartM.AddHistoricalData(candleChart1, now.AddMinutes(-400), now.AddMinutes(-1));

            //ChartList.Add(stockChart1);
            ChartList.Add(candleChart1);
            //ChartList.Add(ema21.IndexChart);
            ChartList.Add(macd.IndexChart);

            CurrentValueList.Add(stockChart1);

            ChartM.SubscribeToBookTickerUpdates(stockChart1);
        }

        public double GetCurrentValue(string name) =>
            CurrentValueList.FirstOrDefault(c => c.Name == name && c.IsCurrentValue).GetCurrentValue();

        public double GetCurrentWalletValue(string name) =>
           CurrentValueList.FirstOrDefault(c => c.Name == name && c.IsCurrentValue).GetCurrentValue();

    }
}
