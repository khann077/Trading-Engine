using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngineServer.Orders
{
    public class Rejection : IOrderCore
    {
        public Rejection(IOrderCore rejectedOrder, RejectionReason rejectionReason)
        {
            _orderCore = rejectedOrder;
        }

        private readonly IOrderCore _orderCore;

        public long OrderId => _orderCore.OrderId;

        public string Username => _orderCore.Username;

        public int SecurityId => _orderCore.SecurityId;
    }
}
