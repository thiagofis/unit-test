# Clean unit test (powered by xUnit)

## Description
The target of this project is to demonstrate how to create a clean unit test suit project. The test method focus should be the relevant data.

## Example

The example below will illustrate the basic principle of Clean method test:  It focuses on relevant test types and data without the need to explicitly define each value object properties, mock dependencies or objects initializations. Meaning that you should only apply your effort on what really matters.

### Test scenario:

```
Given: a order sent to risk fraud gateway client
When: receive a red flag
Then: returns a fraud label 
```

### Sequance Diagram

![Risk Management Sequence Diagram](https://github.com/thiagofis/xunit/blob/master/risk%20management%20sequence%20diagram.png)

### With default MSTest method

* [MSTest](https://www.nuget.org/packages/MSTest.TestFramework/) -  Native unit testing frameworks for .Net framework.
* [Moq](https://github.com/moq/moq) - The most popular and ~~friendly~~ mocking framework for .NET.
```c#
[TestMethod]
public void Analyze_ReceivedARedFlagRisk_ReturnsFraudLabel()
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
        Items = new List<OrderItem> 
        { 
          new OrderItem { Amount = 10, Brand = "Nike", Description = "Shoes", Type = "Summer" }            
        }
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
```


 ### With clean test method (powered by xUnit)

* [xunit](https://xunit.github.io/) - Open source, community-focused unit testing tool for the .NET Framework.
* [xunit.runner.visualstudio](https://www.nuget.org/packages/xunit.runner.visualstudio/) - Test Explorer runner for the xUnit.net framework.
* [AutoFixture](https://github.com/AutoFixture/AutoFixture) - Minimize the 'Arrange' phase of your unit tests in order to maximize maintainability.
* [Nsubstitute](http://nsubstitute.github.io/) - Written with less noise and fewer lambdas.
* [AutoFixture.Xunit2](https://www.nuget.org/packages/AutoFixture.Xunit2/) - By leveraging the data theory feature of xUnit.net.
* [AutoFixture.AutoNSubstitute](https://www.nuget.org/packages/AutoFixture.AutoNSubstitute/) - Turns AutoFixture into an Auto-Mocking Container.

```c#
[AutoFixtureNSustituteData] //Custom inline data attribute
[Theory]
public void Analyze_ReceivedARedFlagRisk_ReturnsFraudLabel(Order order, RiskResponse riskResponse, 
RiskFraudAnalyzer sut)
{
	//Assemble       
	var expected = Label.Fraud;            

	riskResponse.Flag = RiskFlag.Red;

	sut.RiskFraudClientService.CheckRisk(Arg.Any<OrderInfoRequest>()).Returns(riskResponse);

	//Act
	var result = sut.Analize(order);

	//Assert
	Xunit.Assert.Equal(expected, result);
}
```
