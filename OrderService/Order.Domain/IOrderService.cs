using System.Collections.Generic;
using System.ServiceModel;

namespace Orders.Domain
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IOrderService
    {
        [OperationContract]
        IEnumerable<Order> GetOrders();
    }
}
