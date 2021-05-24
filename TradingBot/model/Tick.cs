using System;
using System.Collections.Generic;
using System.Text;
using TradingBot.interfaces;

namespace TradingBOT.model
{
    public class Tick : IChartItem
    {
        public DateTime DateTime { get; }

        public decimal Value { get; }

        public Tick(DateTime dateTime, decimal value) 
        {
            DateTime = dateTime;
            Value = value;
        }
    }
}
