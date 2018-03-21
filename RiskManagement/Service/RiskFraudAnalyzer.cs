using System.Linq;
using RiskManagement.Gateway;

namespace RiskManagement.Service
{
    public class RiskFraudAnalyzer : IRiskFraudAnalyzer
    {
        public RiskFraudAnalyzer(IRiskFraudClientService riskFraudClientService)
        {
            RiskFraudClientService = riskFraudClientService;
        }

        public IRiskFraudClientService RiskFraudClientService { get; private set; }

        public Label Analize(Order order)
        {
            var request = GenerateRequest(order);

            var response = RiskFraudClientService.CheckRisk(request);

            var label = Classify(response.Flag);

            return label;
        }

        private Label Classify(RiskFlag flag)
        {
            switch (flag)
            {
                case RiskFlag.Black:
                case RiskFlag.Red:
                    return Label.Fraud;

                case RiskFlag.Yellow:
                    return Label.Suspicious;

                default:
                    return Label.Safe;
            }
        }

        private OrderInfoRequest GenerateRequest(Order order)
        {
            return new OrderInfoRequest
            {
                TotalAmount = order.Amount,
                BillingAddress = new AddressInfo(order.BillingAddress.City, order.BillingAddress.State, order.BillingAddress.Country),
                ShippingAddress = new AddressInfo(order.ShippingAddress.City, order.ShippingAddress.State, order.ShippingAddress.Country),
                Items = order.Items.Select(x => new ItemInfo(x.Description, x.Brand, x.Type, x.Amount))
            };
        }
    }
}