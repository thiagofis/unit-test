namespace RiskManagement.Service
{
    public interface IRiskFraudAnalyzer
    {
        Label Analyze(Order order);
    }
}
