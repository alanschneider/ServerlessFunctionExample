using System;
using Xunit;
using Azure.GreeterApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreeterApiTest
{
    public class GreeterUnitTest
    {
        [Fact]
        public void ReturnCannedResponse_ReturnsCorrectResponse()
        {
            var greeter = new Greeter();
            var responseValue = ((OkObjectResult) greeter.ReturnCannedResponse()).Value;
            Assert.Equal(responseValue, Greeter.CANNED_MESSAGE);
        }

        [Fact]
        public void SayHello_WithName_ReturnsCorrectGreeting()
        {

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SayHello_WithNoName_ReturnsCorrectGreeting(string name)
        {

        }
    }
}
