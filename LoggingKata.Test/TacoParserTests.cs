using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldDoSomething()
        {
            var x = 5;
            var y = 5;
            var expected = 25;
            Assert.Equal(expected, x * y);
        }

        [Theory]
        [InlineData("-84.677017, 34.073638,\"Taco Bell Acwort... " +
                    "(Free trial * Add to Cart for a full POI info)")]
        [InlineData("-84.677017, 34.073638")]
        public void ShouldParse(string str)
        {
            var testParser = new TacoParser();
            var results = testParser.Parse(str);
            Assert.NotNull(results);
        }

        [Theory]
        [InlineData(null)]//Testing for Null
        [InlineData("")]//Testing for empty string
        [InlineData("181, 91")]//Testing for > Max Lon, Max Lat
        [InlineData("-181, -91")]//Testing for < Min Lon, Min Lat
        [InlineData("c,s")]//Testing for incorrect input values
        [InlineData("abc,abc,abc")] // further testing of incorrect input values
        [InlineData("1234,1234,1234")] //furthest tesing of incorrect input values
        public void ShouldFailParse(string str)
        {
            var testParser = new TacoParser();
            var results = testParser.Parse(str);
            Assert.Null(results);
        }
    }
}