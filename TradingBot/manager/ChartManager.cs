using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.model;
using TradingBOT.manager;
using TradingBOT.model;

namespace TradingBot.manager
{
    public class ChartManager
    {
        public ClientManager Client { get; }

        public List<Chart> Charts { get; }

        public ChartManager(ClientManager client) 
        {
            Client = client;
            Charts = new List<Chart>();
        }

        public void AddChart(Chart chart) 
        {
            Charts.Add(chart);
        }

        //public void SubscribeToUserDataUpdates()
        //{
        //    Client.SocketClient.Spot.SubscribeToUserDataUpdates(Client.StartResult.Data,
        //            orderUpdate =>
        //            { // Handle order update
        //                Console.WriteLine("Hello World!");
        //            },
        //            ocoUpdate =>
        //            { // Handle oco order update
        //                Console.WriteLine("Hello World!");
        //            },
        //            positionUpdate =>
        //            { // Handle account position update
        //                Console.WriteLine("Hello World!");
        //            },
        //            balanceUpdate =>
        //            { // Handle balance update
        //                Console.WriteLine(balanceUpdate.ToString());
        //            });
        //}

        public void SubscribeToBookTickerUpdates(StockChart chart)
        {
            Client.SocketClient.Spot.SubscribeToBookTickerUpdates(chart.Symbol.ToString(), data => {
                chart.Add(new decimal[] { data.BestBidPrice }, DateTime.Now);
            });
        }

        public void SubscribeToBookTickerUpdates(CandleChart chart)
        {
            Client.SocketClient.Spot.SubscribeToKlineUpdates(chart.Symbol.ToString(), chart.Interval, data => {
                chart.Add(data.Data);
            });
        }

        public void AddHistoricalData(CandleChart chart, DateTime start, DateTime end) 
        { 
            var candles = Client.Client.Spot.Market.GetKlines(symbol: chart.Symbol.ToString(), interval: chart.Interval, startTime: start, endTime: end, limit: 1000).Data.ToList();
            foreach (var candle in candles)
            {
                chart.Add(new decimal[] { candle.Open, candle.Close, candle.High, candle.Low, candle.QuoteVolume }, candle.OpenTime);
            }
        }
    }
}
