namespace RiskManagement.Gateway
{
    public interface IRiskFraudClientService
    {
        RiskResponse CheckRisk(OrderInfoRequest orderInfo);
    }
}