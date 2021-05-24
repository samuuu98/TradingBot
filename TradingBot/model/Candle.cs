using Binance.Net.Enums;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.interfaces;

namespace TradingBot.model
{
    public class Candle : IChartItem, IQuote
    {
        public decimal Open { get; }
        public decimal Close { get; }
        public decimal High { get; }
        public decimal Low { get; }
        public DateTime Date { get; }
        public decimal Volume { get; }

        public KlineInterval Interval { get; }

        public Candle(decimal open, decimal close, decimal max, decimal min, decimal volume, DateTime date)
        {
            Open = open;
            Close = close;
            High = max;
            Low = min;
            Volume = volume;
            Date = date;
        }

        public Candle(decimal open, decimal close, decimal max, decimal min, decimal volume, DateTime date, KlineInterval interval) : this(open, close, max, min, volume, date)
        {
            Interval = interval;
        }
    }
}
