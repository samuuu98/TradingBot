using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TradingBot.enums;
using TradingBot.interfaces;
using TradingBot.model;

namespace TradingBOT.model
{
    public class StockChart : Chart
    {
        public List<Tick> TickList { get; set; }

        public StockChart(string name,ESymbol symbol, bool isCurrentValue = false) : base(name, symbol, isCurrentValue)
        {
            TickList = new List<Tick>();
            SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = name,
                        Values = new ChartValues<double> { },
                        PointGeometrySize = 0 
                    },
                };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");
            //MaxValue = 37700;
            //MinValue = 37100;
        }

        public override double GetCurrentValue() =>
            (double)TickList.LastOrDefault().Value;

        public override void Add(IChartItem tick)
        {
            if (tick is Tick t)
                TickList.Add(t);
        }

        public override void Add(decimal[] value, DateTime openTime) 
        {

            if (value.Length == 1)
            {
                if (openTime.Second % 5 == 0)
                {
                    TickList.Add(new Tick(openTime, value[0]));
                    SeriesCollection[0].Values.Add((double)value[0]);
                    if (SeriesCollection[0].Values.Count > 1000)
                        SeriesCollection[0].Values.RemoveAt(0);
                }
            }
        }
    }
}
