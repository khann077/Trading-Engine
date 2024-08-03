using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngineServer.Orders
{
    class AskLimitComparer : IComparer<Limit>
    {

        public static IComparer<Limit> Comparer { get; } = new AskLimitComparer();
        public int Compare(Limit x, Limit y)
        { 
            if(x.Price == y.Price)
                return 0;
            else if (x.Price > y.Price)
                return 1;
            else return -1;
        }
    }

    class BidLimitComparer : IComparer<Limit>
    {
        public static IComparer<Limit> Comparer { get; } = new BidLimitComparer();
        public int Compare(Limit x, Limit y)
        { 
            if(x.Price == y.Price)
                return 0;
            else if (x.Price > y.Price)
                return -1;
            else return 1;
        }

    }

}
