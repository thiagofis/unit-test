namespace RiskManagement.Gateway
{
    public class RiskResponse
    {
        public int Score { get; set; }
        public RiskFlag Flag { get; set; }
        public string Description { get; set; }
    }

    public enum RiskFlag
    {
        Black, Red, Yellow, Green, White
    }
}