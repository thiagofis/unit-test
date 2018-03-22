using AutoFixture;
using AutoFixture.Xunit2;

namespace RiskManagement.Test
{
    public class AutoFixtureDataAttribute : AutoDataAttribute
    {
        public AutoFixtureDataAttribute() : base(new Fixture())
        {
        }
    }
}