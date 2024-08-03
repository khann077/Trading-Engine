using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngineServer.Orders
{
    public sealed class OrderStatusCreator
    {
        public static CancelOrderStatus GenerateCancelOrderStatus(CancelOrder co)
        {
            return new CancelOrderStatus();
        }

        public static NewOrderStatus GenerateNewOrderStatus(Order no)
        {
            return new NewOrderStatus();
        }

        public static ModifyOrderStatus GenerateModifyOrderStatus(ModifyOrder mo)
        {
            return new ModifyOrderStatus();
        }

    }

}
