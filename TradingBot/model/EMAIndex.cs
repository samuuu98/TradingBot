using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBOT.model;

namespace TradingBot.model
{
    public class EMAIndex : Index
    {
        private const string NAME = "EMA";
        public int Value { get; set; }
        public List<int> Values { get; set; }

        public EMAIndex(CandleChart chart, int value) : base(NAME + value, chart)
        {
            Value = value;
            IndexChart = new StockChart(NAME + value, chart.Symbol);
            chart.DataChanged += Chart_DataChanged;
            Calculate();
        }

        private void Chart_DataChanged(double obj)
        {
            Calculate();
        }

        public override void Calculate() 
        {
            if (_Chart.Quotes.Count >= Value)
            {
                ((StockChart)IndexChart).TickList.Clear();
                ((StockChart)IndexChart).SeriesCollection[0].Values.Clear();
                IEnumerable<SmaResult> results = Indicator.GetSma(_Chart.Quotes, Value);
                foreach (var res in results.Where(r => r.Sma.HasValue).ToList())
                {
                    IndexChart.Add(new decimal[] { res.Sma.Value }, res.Date);
                }
            }
        }
    }
}
