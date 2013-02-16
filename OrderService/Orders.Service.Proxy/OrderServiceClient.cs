using System;
using System.Collections.Generic;
using System.ServiceModel;
using Orders.Domain;

namespace Orders.Service.Proxy
{
    public class OrderServiceClient : ClientBase<IOrderService>, IOrderService, IDisposable
    {
        public OrderServiceClient(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
        }

        public IEnumerable<Order> GetOrders()
        {
            return Channel.GetOrders();
        }

        void IDisposable.Dispose()
        {

            try
            {
                if (Channel == null)
                    return;

                if (State == CommunicationState.Faulted)
                    Abort();
                else
                    Close();
            }
            catch (FaultException fe)
            {
                Abort();
                Console.WriteLine(fe);
            }
            catch (CommunicationException)
            {
                Abort();
            }
            catch (TimeoutException)
            {
                Abort();
            }
            catch (Exception e)
            {
                Abort();
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
