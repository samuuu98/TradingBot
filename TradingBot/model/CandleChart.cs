using Binance.Net.Enums;
using Binance.Net.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.enums;
using TradingBot.interfaces;

namespace TradingBot.model
{
    public class CandleChart : Chart
    {

        public KlineInterval Interval { get;}

        public DateTime LastCandle { get; set; }

        public CandleChart(string name, ESymbol symbol, KlineInterval interval, bool isCurrentValue = false) : base(name, symbol, isCurrentValue) 
        {
            //TODO
            if (name == "BTCEUR") 
            {
                CouplesCoins.Add((ECoins.BTC, ECoins.EUR));
            }

            Interval = interval;
            SeriesCollection = new SeriesCollection
                {
                    new OhlcSeries()
                    {
                        Values = new ChartValues<OhlcPoint>()
                    }
                };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");
            MaxValue = 37700;
            MinValue = 36000;
        }
        public override double GetCurrentValue() =>
            //TODO Controllare quale valore prendere, mi aspetto che
            ((double)Quotes.LastOrDefault().High + (double)Quotes.LastOrDefault().Low) / 2;


        public override void Add(decimal[] value, DateTime openTime)
        {
            if (value.Length == 5)
            {
                Quotes.Add(new Candle(value[0], value[1], value[2], value[3], value[4], openTime));
                SeriesCollection[0].Values.Add(new OhlcPoint((double)value[0], (double)value[2], (double)value[3], (double)value[1]));
                base.Add(value, openTime);
            }
        }

        public override void Add(IChartItem candle) 
        {
            if(candle is Candle can)
                Quotes.Add(can);
        }

        public void Add(IEnumerable<Candle> candles)
        {
            foreach(var c in candles)
                Quotes.Add(c);
        }

        internal void Add(IBinanceStreamKline data)
        {
            if (LastCandle == default)
                LastCandle = data.OpenTime;

            if (data.OpenTime == LastCandle && Quotes.Count > 0)
                 RemoveAt(Quotes.Count -1);

            Add(new decimal[] { data.Open, data.Close, data.High, data.Low, data.QuoteVolume }, data.OpenTime);
            LastCandle = data.OpenTime;
        }

        public void RemoveAt(int index) 
        {
            Quotes.RemoveAt(index);
            SeriesCollection[0].Values.RemoveAt(index);
        }
    }
}
