using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TradingBot.model;
using TradingBOT.model;

namespace TradingBot.view.selectors
{
    public class ChartTemplateSeletor : DataTemplateSelector
    {
        public DataTemplate StockChartTemplate { get; set; }
        public DataTemplate CandleChartTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is StockChart)
                return StockChartTemplate;
            else if (item is CandleChart)
                return CandleChartTemplate;

            return null;
        }
    }
}
