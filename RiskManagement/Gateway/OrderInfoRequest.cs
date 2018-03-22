using System.Collections.Generic;

namespace RiskManagement.Gateway
{
    public class OrderInfoRequest
    {
        public string FullName { get; set; }
        public IEnumerable<ItemInfo> Items { get; set; }
        public AddressInfo BillingAddress { get; set; }
        public AddressInfo ShippingAddress { get; set; }
    }

    public class ItemInfo
    {
        public ItemInfo(string description, string brand, string type, decimal amount)
        {
            Description = description;
            Brand = brand;
            Type = type;
            Amount = amount;
        }

        public string Description { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }

    public class AddressInfo
    {
        public AddressInfo(string city, string state, string country)
        {
            City = city;
            State = state;
            Country = country;
        }

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}