namespace RiskManagement.Service
{
    public interface IRiskFraudAnalyzer
    {
        Label Analize(Order order);
    }
}