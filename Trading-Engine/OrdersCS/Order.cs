using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngineServer.Orders
{
    public class Order : IOrderCore
    {
        public Order(IOrderCore orderCore, long price, uint quantity, bool isBuySide)
        {
            Price = price;
            IsBuySide = isBuySide;
            _orderCore = orderCore;
            CurrentQuantity = quantity;
            InitialQuantity = quantity;
        }

        public Order(ModifyOrder modifyOrder) : this(modifyOrder, modifyOrder.Price, modifyOrder.Quantity, modifyOrder.IsBuyside)
        {
            
        }
        public void IncreaseQuantity(uint quantityDelta)
        { 
            CurrentQuantity += quantityDelta;
        }

        public void DecreaseQuantity(uint quantityDelta)
        {
            if (quantityDelta > CurrentQuantity)
                throw new InvalidOperationException($"Decrease Delta greater than current quantity for OrderId={OrderId}");
            CurrentQuantity -= quantityDelta;
        }

        private readonly IOrderCore _orderCore;
        public long Price { get; private set; }
        public uint Quantity { get; private set; }
        public uint InitialQuantity { get; private set; }
        public uint CurrentQuantity { get; private set; }
        public bool IsBuySide { get; private set; } 
        public long OrderId => _orderCore.OrderId;
        public string Username => _orderCore.Username;
        public int SecurityId => _orderCore.SecurityId;
    }
}
