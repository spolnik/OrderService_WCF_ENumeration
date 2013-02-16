using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Orders.Domain;

namespace Orders.Creator
{
    class Program
    {
        private static readonly List<string> Origins = new List<string> {"Krakow", "Warszawa", "London", "Barcelona", "Wroclaw", "Bejing", "Sydney", "Singapure", "Monako", "Zurich", "Poznan", "Gdansk", "Madryt", "Bruksela", "Roma"};
        private static readonly List<string> Product = new List<string> {"FX Forward", "FX Option", "FX Swap", "FICC Forward", "FICC Option", "FICC Swap", "Commodities Forward", "Commodities Option", "Commodities Swap", "EQ Option", "EQ Forward", "EQ Swap"};

        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Invalid number of orders to create", "args");

            var count = Convert.ToInt32(args[0]);

            for (var i = 0; i < count; i++)
            {
                var order = GetRandomOrder(i+1);
                SaveOrder(order);
            }
        }

        private static void SaveOrder(Order order)
        {
            var dataDirectory = new DirectoryInfo("Data");
            if (!dataDirectory.Exists)
                dataDirectory.Create();

            var fileInfo = new FileInfo(Path.Combine("Data", string.Format("order_{0}_{1}.xml", DateTime.Now.ToFileTime(), order.Id)));

            using (var writer = fileInfo.OpenWrite())
            {
                var xmlSerializer = new XmlSerializer(typeof (Order));
                xmlSerializer.Serialize(writer, order);
            }
        }

        private static Order GetRandomOrder(int id)
        {
            var random = new Random();
            var notional = (decimal)random.NextDouble()*random.Next(1000000);
            var originId = random.Next(Origins.Count);
            var productId = random.Next(Product.Count);

            return new Order(id, Product[productId], notional, Origins[originId]);
        }
    }
}
