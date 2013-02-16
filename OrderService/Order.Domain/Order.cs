using System;

namespace Orders.Domain
{
    [Serializable]
    public class Order
    {
        public Order()
        {
        }

        public Order(int id, string product, decimal price, string origin)
        {
            Id = id;
            Product = product;
            Price = price;
            Origin = origin;
        }

        public int Id { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
    }
}
