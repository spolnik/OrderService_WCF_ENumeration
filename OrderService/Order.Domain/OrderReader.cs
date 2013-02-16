using System.IO;
using System.Xml.Serialization;

namespace Orders.Domain
{
    public class OrderReader
    {
        public OrderReader(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; private set; }

        public Order ReadIt()
        {
            var fileInfo = new FileInfo(FileName);
            using (var reader = fileInfo.OpenRead())
            {
                var serializer = new XmlSerializer(typeof (Order));
                return (Order) serializer.Deserialize(reader);
            }
        }
    }
}