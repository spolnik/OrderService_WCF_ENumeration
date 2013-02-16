using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Orders.Domain
{
    public class OrdersReader
    {
        public OrdersReader(string directoryName)
        {
            DirectoryName = directoryName;
        }

        public string DirectoryName { get; private set; }

        public IEnumerable<Order> ReadAll()
        {
            var directoryInfo = new DirectoryInfo(DirectoryName);
            var files = directoryInfo.GetFiles();

            return files.Select(fileInfo => new OrderReader(fileInfo.FullName).ReadIt());
        }
    }
}