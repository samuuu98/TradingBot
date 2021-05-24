using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBOT.model;

namespace TradingBot.model
{
    public class MACDIndex : Index
    {
        private const string NAME = "MACD";
        public int? ValueSlow { get; set; }
        public int? SignalPeriod { get; set; }
        public int? ValueFast { get; set; }
        public List<int> Values { get; set; }

        public List<MacdResult> MACDResults { get; set; }

        public MACDIndex(CandleChart chart, int valueSlow = 0, int valueFast = 0, int signalPeriod = 0) : base($"{NAME}_{valueFast}_{valueSlow}_{signalPeriod}", chart)
        {
            if (valueSlow != 0)
                ValueSlow = valueSlow;
            else
                ValueSlow = null;
            if (valueFast != 0)
                ValueFast = valueFast;
            else
                ValueFast = null;
            if (signalPeriod != 0)
                SignalPeriod = signalPeriod;
            else
                SignalPeriod = null;

            IndexChart = new StockChart(Name, chart.Symbol);
            MACDResults = new List<MacdResult>();

            chart.DataChanged += Chart_DataChanged;
            Calculate();
        }

        private void Chart_DataChanged(double obj)
        {
            Calculate();
        }

        public override void Calculate()
        {
            if (_Chart.Quotes.Count >= 135)
            {
                ((StockChart)IndexChart).TickList.Clear();
                ((StockChart)IndexChart).SeriesCollection[0].Values.Clear();
                IEnumerable<MacdResult> results;
                if (ValueFast.HasValue && ValueSlow.HasValue && SignalPeriod.HasValue)
                    results = Indicator.GetMacd(_Chart.Quotes, ValueFast.Value, ValueSlow.Value, SignalPeriod.Value);
                else if(ValueFast.HasValue && ValueSlow.HasValue)
                    results = Indicator.GetMacd(_Chart.Quotes, ValueFast.Value, ValueSlow.Value);
                else
                    results = Indicator.GetMacd(_Chart.Quotes);
                foreach (var res in results.Where(r => r.Macd.HasValue && r.Signal.HasValue).ToList())
                {
                    if(MACDResults.Count() == 0)
                        MACDResults.Add(res);
                    if ((double) Math.Sign(res.Signal.Value - res.Macd.Value) != Math.Sign(MACDResults.LastOrDefault().Signal.Value - MACDResults.LastOrDefault().Macd.Value))
                    {
                        if (res.Histogram.Value < MACDResults.LastOrDefault()?.Histogram)
                            OnBuySignal();
                        else
                            OnSellSignal();
                    }
                    ((StockChart)IndexChart).Add(new decimal[] { res.Macd.Value }, res.Date);
                    MACDResults.Add(res);
                }
            }
        }
    }
}
