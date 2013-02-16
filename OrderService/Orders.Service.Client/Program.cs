using System;
using System.Diagnostics;
using Orders.Service.Proxy;

namespace Orders.Service.Client
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter to start");
            Console.ReadLine();

            var proxy = new OrderServiceProxy();
            var orders = proxy.GetOrders();

            var id = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var order in orders)
                Console.WriteLine("Order id: {0}, element id: {1}", order.Id, ++id);
            stopwatch.Stop();

            Console.WriteLine("Ellapsed time: {0} ms, press enter to finish ...", stopwatch.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
