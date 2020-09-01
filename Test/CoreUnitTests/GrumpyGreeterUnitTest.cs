using Xunit;
using Core;

namespace CoreUnitTests
{
    public class GrumpyGreeterUnitTest
    {
        [Fact]
        public void ReturnCannedResponse_ReturnsCorrectResponse()
        {
            var greeter = new GrumpyGreeter();
            var expected = "Don't call me.";
            var actual = greeter.ReturnCannedResponse();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SayHello_WithName_ReturnsCorrectGreeting()
        {
            var greeter = new GrumpyGreeter();
            var name = "Foo";
            var expected = $"Get lost, {name}.";
            var actual = greeter.SayHello(name);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SayHello_WithNoName_ReturnsCorrectGreeting(string name)
        {
            var greeter = new GrumpyGreeter();
            var actual = greeter.SayHello(name);
            var expected = "Get lost, loser.";
            Assert.Equal(expected, actual);
        }
    }
}
