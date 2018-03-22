using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace RiskManagement.Test
{
    public class AutoFixtureNSustituteDataAttribute : AutoDataAttribute
    {
        public AutoFixtureNSustituteDataAttribute() : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}