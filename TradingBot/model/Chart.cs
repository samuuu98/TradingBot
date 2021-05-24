using LiveCharts;
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
    public class Chart
    {
        public event Action<double> DataChanged;

        public ESymbol Symbol { get; }
        public string Name { get; }

        public List<(ECoins, ECoins)> CouplesCoins { get; set; }
        public List<IQuote> Quotes { get; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public bool IsCurrentValue { get; }

        public Chart(string name, ESymbol symbol, bool isCurrentValue = false)
        {
            Symbol = symbol;
            Name = name;
            Quotes = new List<IQuote>();
            CouplesCoins = new List<(ECoins, ECoins)>();
            IsCurrentValue = false;
        }

        public virtual void Add(IChartItem value) 
        {
            //DataChanged?.Invoke(0);
        }

        public virtual void Add(decimal[] value, DateTime dateTime)
        {
            DataChanged?.Invoke(0);
        }

        public virtual double GetCurrentValue() => 
            //TODO Controllare quale valore prendere, mi aspetto che
            (double)Quotes.LastOrDefault().Open;
        
    }
}
