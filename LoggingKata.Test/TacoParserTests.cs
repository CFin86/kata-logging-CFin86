using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldDoSomething()
        {
            int x = 5;
            int y = 5;
            int expected = 25;
            Assert.Equal(expected, x * y);
        }

        [Theory]
        [InlineData("-84.677017, 34.073638,\"Taco Bell Acwort... " +
                    "(Free trial * Add to Cart for a full POI info)")]
        public void ShouldParse(string str)
        {
            var testParser = new TacoParser();
            var results = testParser.Parse(str);
            Assert.NotNull(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldFailParse(string str)
        {
            var testParser = new TacoParser();
            var results = testParser.Parse(str);
            Assert.Null(results);
        }
    }
}