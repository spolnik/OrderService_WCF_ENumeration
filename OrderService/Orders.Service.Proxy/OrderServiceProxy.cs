using System.Collections.Generic;
using Orders.Domain;

namespace Orders.Service.Proxy
{
    public class OrderServiceProxy
    {
        public IEnumerable<Order> GetOrders()
        {
            using (var client = new OrderServiceClient("NetTcpBinding_IOrderService"))
            {
                var hasElements = true;
                while (hasElements)
                {
                    hasElements = false;
                    foreach (var order in client.GetOrders())
                    {
                        yield return order;
                        hasElements = true;
                    }
                }
            }
        }
    }
}
