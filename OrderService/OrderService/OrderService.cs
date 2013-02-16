using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using Orders.Domain;

namespace Orders.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    [SanitizingErrorHandler]
    public class OrderService : IOrderService
    {
        private const string PageSizeKey = "pageSize";

        private IEnumerator<Order> _enumerator;
        private readonly int _size;

        public OrderService()
        {
            var pageSize = ConfigurationManager.AppSettings[PageSizeKey];
            _size = Convert.ToInt32(pageSize);
        }

        #region IOrderService Members

        public IEnumerable<Order> GetOrders()
        {
            var enumerator = GetEnumerator();

            var result = new List<Order>(_size);

            for (var i = 0; i < _size; i++)
            {
                if (enumerator.MoveNext())
                {
                    result.Add(enumerator.Current);
                }
                else
                {
                    if (result.Count == 0)
                        _enumerator.Dispose();

                    Console.WriteLine("[{0}]Returned {1} orders for context {2}", DateTime.Now.ToString("HH:mm:ss"),
                                      result.Count, OperationContext.Current.SessionId);

                    return result;
                }
            }

            Console.WriteLine("[{0}]Returned {1} orders for context {2}", DateTime.Now.ToString("HH:mm:ss"),
                              result.Count, OperationContext.Current.SessionId);

            return result;
        }

        #endregion

        private IEnumerator<Order> GetEnumerator()
        {
            return _enumerator ?? (_enumerator = new OrdersReader("C:\\Temp\\Data").ReadAll().GetEnumerator());
        }
    }
}