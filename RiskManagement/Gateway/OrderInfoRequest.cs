using System.Collections.Generic;

namespace RiskManagement.Gateway
{
    public class OrderInfoRequest
    {
        public OrderInfoRequest(string fullUserName)
        {
            FullUserName = fullUserName ?? throw new System.ArgumentNullException(nameof(fullUserName));
        }

        public string FullUserName { get; }
        public IEnumerable<ItemInfo> Items { get; set; }
        public AddressInfo BillingAddress { get; set; }
        public AddressInfo ShippingAddress { get; set; }
    }

    public class ItemInfo
    {
        public ItemInfo(string description, string brand, string type, decimal amount)
        {


            Description = description ?? throw new System.ArgumentNullException(nameof(description));
            Brand = brand ?? throw new System.ArgumentNullException(nameof(brand));
            Type = type ?? throw new System.ArgumentNullException(nameof(type));
            Amount = amount;
        }

        public string Description { get; }
        public string Brand { get; }
        public string Type { get; }
        public decimal Amount { get; }
    }

    public class AddressInfo
    {
        public AddressInfo(string city, string state, string country)
        {
            City = city ?? throw new System.ArgumentNullException(nameof(city));
            State = state ?? throw new System.ArgumentNullException(nameof(state));
            Country = country ?? throw new System.ArgumentNullException(nameof(country));
        }

        public string City { get; }
        public string State { get; }
        public string Country { get; }
    }
}