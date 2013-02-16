using System;
using System.ServiceModel;

namespace Orders.Service.Host
{
    class Program
    {
        static void Main()
        {
            using (var serviceHost = new ServiceHost(typeof (OrderService)))
            {
                serviceHost.Open();

                Console.WriteLine("Service is running under: {0}", serviceHost.BaseAddresses[0]);
                Console.WriteLine("Click enter to close.");
                Console.ReadLine();

                serviceHost.Close();
            }
        }
    }
}
