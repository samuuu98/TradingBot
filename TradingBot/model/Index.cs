using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.model
{
    public abstract class Index
    {
        public event Action<int> OnSignal;
        public string Name { get; }
        public Chart _Chart { get; }
        public Chart IndexChart { get; set; }

        public Index(string name, Chart chart) 
        {
            Name = name;
            _Chart = chart;
        }

        public abstract void Calculate();

        public virtual void OnBuySignal() 
        {
            OnSignal.Invoke(1);
        }

        public virtual void OnSellSignal()
        {
            OnSignal.Invoke(0);
        }
    }
}
