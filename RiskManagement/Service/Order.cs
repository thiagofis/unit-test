using System.Collections.Generic;

namespace RiskManagement.Service
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}