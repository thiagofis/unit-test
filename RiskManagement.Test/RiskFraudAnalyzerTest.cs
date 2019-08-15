using System.Collections.Generic;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSubstitute;
using RiskManagement.Gateway;
using RiskManagement.Service;
using Xunit;
using MS = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiskManagement.Test
{
    [TestClass]
    public class RiskFraudAnalyzerMSTest_Moq
    {
        //1 - MSTest
        //  1.1 - TestClass
        //  1.2 - TestMethod
        //2 - Moq

        [TestMethod]
        public void Ex1_Analyze_ReceivedARedFlagRisk_ReturnsFraudLabel()
        {
            //Assemble
            var expected = Label.Fraud;

            var riskFraudClientServiceMock = new Mock<IRiskFraudClientService>();

            var sut = new RiskFraudAnalyzer(riskFraudClientServiceMock.Object);

            var order = new Order
            {
                Id = 64,
                UserName = "Fulano de Tal",
                BillingAddress = new Address { City = "Porto", Country = "Portugal", State = "Porto" },
                ShippingAddress = new Address { City = "Porto", Country = "Portugal", State = "Porto" },
                Items = new List<OrderItem> { new OrderItem { Amount = 10, Brand = "Nike", Description = "Shoes", Type = "Summer" } }
            };

            var riskResponse = new RiskResponse
            {
                Flag = RiskFlag.Red,
                Score = 1000,
                Description = "Blacklist"
            };

            riskFraudClientServiceMock.Setup(x => x.CheckRisk(It.IsAny<OrderInfoRequest>())).Returns(riskResponse);

            //Act
            var result = sut.Analyze(order);

            //Assert
            MS.Assert.AreEqual(expected, result);
        }
    }

    [TestClass]
    public class RiskFraudAnalyzerMSTest_Moq_AutoFixture
    {
        //1 - MSTest
        //  1.1 - TestClass
        //  1.2 - TestMethod
        //2 - Moq
        //3 - AutoFixture

        [TestMethod]
        public void Ex2_Analyze_ReceivedARedFlagRisk_ReturnsFraudLabel()
        {
            //Assemble
            var expected = Label.Fraud;

            var riskFraudClientServiceMock = new Mock<IRiskFraudClientService>();

            var sut = new RiskFraudAnalyzer(riskFraudClientServiceMock.Object);

            var fixture = new Fixture();

            var order = fixture.Create<Order>();

            var riskResponse = fixture.Create<RiskResponse>();
            riskResponse.Flag = RiskFlag.Red;

            riskFraudClientServiceMock.Setup(x => x.CheckRisk(It.IsAny<OrderInfoRequest>())).Returns(riskResponse);

            //Act
            var result = sut.Analyze(order);

            //Assert
            MS.Assert.AreEqual(expected, result);
        }
    }

    [TestClass]
    public class RiskFraudAnalyzerMSTest_NSubstitute_AutoFixture
    {
        //1 - MSTest
        //  1.1 - TestClass
        //  1.2 - TestMethod
        //2 - Moq ---> NSubstitute
        //3 - AutoFixture


        [TestMethod]
        public void Ex3_Analyze_ReceivedARedFlagRisk_ReturnsFraudLabel()
        {
            //Assemble
            var expected = Label.Fraud;

            var riskFraudClientService = Substitute.For<IRiskFraudClientService>();
            
            var sut = new RiskFraudAnalyzer(riskFraudClientService);

            var fixture = new Fixture();

            var order = fixture.Create<Order>();

            var riskResponse = fixture.Create<RiskResponse>();
            riskResponse.Flag = RiskFlag.Red;

            riskFraudClientService.CheckRisk(Arg.Any<OrderInfoRequest>()).Returns(riskResponse);

            //Act
            var result = sut.Analyze(order);

            //Assert
            MS.Assert.AreEqual(expected, result);
        }
    }

    //No Class annotation
    public class RiskFraudAnalyzerXUnitTest_AutoFixture_NSubstitute_FactAnnotation
    {
        //1 - MSTest --> XUnit
        //  1.1 - TestClass --> Nothing
        //  1.2 - TestMethod --> Fact
        //2 - Moq ---> NSubstitute
        //3 - AutoFixture       

        [Fact] // TestMethod = Fact
        public void Ex4_Analyze_ReceivedARedFlagRisk_ReturnsFraudLabel()
        {
            //Assemble
            var expected = Label.Fraud;

            var riskFraudClientService = Substitute.For<IRiskFraudClientService>();
            
            var sut = new RiskFraudAnalyzer(riskFraudClientService);

            var fixture = new Fixture();

            var order = fixture.Create<Order>();

            var riskResponse = fixture.Create<RiskResponse>();
            riskResponse.Flag = RiskFlag.Red;

            riskFraudClientService.CheckRisk(Arg.Any<OrderInfoRequest>()).Returns(riskResponse);

            //Act
            var result = sut.Analyze(order);

            //Assert
            Xunit.Assert.Equal(expected, result);
        }
    }


    //No Class annotation
    public class RiskFraudAnalyzerXUnitTest_AutoFixture_NSubstitute_TheoryAnnotation
    {
        //1 - MSTest --> XUnit
        //  1.1 - TestClass --> Nothing
        //  1.2 - TestMethod --> Fact
        //  1.3 - XUnit Theory
        //2 - Moq ---> NSubstitute
        //3 - AutoFixture


        [InlineData(Label.Suspicious, RiskFlag.Yellow)]
        [InlineData(Label.Fraud, RiskFlag.Red)]
        [Theory]
        public void Ex5_Analyze_ReceivedAFlagRisk_ReturnsLabel(Label expected, RiskFlag flag)
        {
            //Assemble           
            var riskFraudClientService = Substitute.For<IRiskFraudClientService>();
            
            var sut = new RiskFraudAnalyzer(riskFraudClientService);

            var fixture = new Fixture();

            var order = fixture.Create<Order>();

            var riskResponse = fixture.Create<RiskResponse>();
            riskResponse.Flag = flag;

            riskFraudClientService.CheckRisk(Arg.Any<OrderInfoRequest>()).Returns(riskResponse);

            //Act
            var result = sut.Analyze(order);

            //Assert
            Xunit.Assert.Equal(expected, result);
        }
    }


    //No Class annotation
    public class RiskFraudAnalyzerXUnitTest_AutoFixture_NSubstitute_TheoryAnnotation_AutoFixtureData
    {
        //1 - MSTest --> XUnit
        //  1.1 - TestClass --> Nothing
        //  1.2 - TestMethod --> Fact
        //  1.3 - XUnit Theory
        //2 - Moq ---> NSubstitute
        //3 - AutoFixture
        //4 - AutoFixtureData by autofixture.xunit2



        [AutoFixtureData]
        [Theory]
        public void Ex6_Analyze_ReceivedARedFlagRisk_ReturnsFraudLabel(Order order, RiskResponse riskResponse)
        {
            //Assemble       
            var expected = Label.Fraud;
            var riskFraudClientService = Substitute.For<IRiskFraudClientService>();
            
            var sut = new RiskFraudAnalyzer(riskFraudClientService);

            riskResponse.Flag = RiskFlag.Red;

            riskFraudClientService.CheckRisk(Arg.Any<OrderInfoRequest>()).Returns(riskResponse);

            //Act
            var result = sut.Analyze(order);

            //Assert
            Xunit.Assert.Equal(expected, result);
        }
    }

    //No Class annotation
    public class RiskFraudAnalyzerXUnitTest_AutoFixture_NSubstitute_TheoryAnnotation_AutoFixtureNSusbtituteData
    {
        //1 - MSTest --> XUnit
        //  1.1 - TestClass --> Nothing
        //  1.2 - TestMethod --> Fact
        //  1.3 - XUnit Theory
        //2 - Moq ---> NSubstitute
        //3 - AutoFixture
        //4 - AutoFixtureData by autofixture.xunit2
        //5 - AutoFixtureNSubstiteData by autofixture.autonsubstitute



        [AutoFixtureNSustituteData]
        [Theory]
        public void Ex7_Analyze_ReceivedARedFlagRisk_ReturnsFraudLabel(Order order, RiskResponse riskResponse, RiskFraudAnalyzer sut)
        {
            //Assemble       
            var expected = Label.Fraud;            

            riskResponse.Flag = RiskFlag.Red;

            sut.RiskFraudClientService.CheckRisk(Arg.Any<OrderInfoRequest>()).Returns(riskResponse);

            //Act
            var result = sut.Analyze(order);

            //Assert
            Xunit.Assert.Equal(expected, result);
        }
    }
}
