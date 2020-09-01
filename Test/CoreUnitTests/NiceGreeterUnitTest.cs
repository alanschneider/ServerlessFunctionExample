using Xunit;
using Core;

namespace CoreUnitTests
{
    public class NiceGreeterUnitTest
    {
        [Fact]
        public void ReturnCannedResponse_ReturnsCorrectResponse()
        {
            var greeter = new NiceGreeter();
            var expected = "Call me with your name";
            var actual = greeter.ReturnCannedResponse();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SayHello_WithName_ReturnsCorrectGreeting()
        {
            var greeter = new NiceGreeter();
            var name = "Foo";
            var expected = $"Hello, {name}!";
            var actual = greeter.SayHello(name);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SayHello_WithNoName_ReturnsCorrectGreeting(string name)
        {
            var greeter = new NiceGreeter();
            var actual = greeter.SayHello(name);
            var expected = "Hello, noname!";
            Assert.Equal(expected, actual);
        }
    }
}
