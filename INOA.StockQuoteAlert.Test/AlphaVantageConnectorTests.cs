using INOA.StockQuoteAlert.Domain;
using INOA.StockQuoteAlert.Infra;
using NUnit.Framework;

namespace INOA.StockQuoteAlert.Test
{
    public class AlphaVantageConnectorTests
    {
        private IAlphaVantageConnector _alphaVantageConnector;

        [SetUp]
        public void Setup()
        {
   
            _alphaVantageConnector = Utils.MakeAlphaVantageConnector(new() { Price = "150,00", Symbol = "AAPL"}, "AAPL");
        }

        [Test]
        public async Task GetGlobalQuote_SuccessAsync()
        {
            
            // Arrange
            const string quoteCode = "AAPL";

            // Act
            GlobalQuoteData result = await _alphaVantageConnector.GetGlobalQuoteAsync(quoteCode);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Symbol, Is.EqualTo("AAPL"));
                Assert.That(Convert.ToDecimal(result?.Price), Is.EqualTo(150.00M));
            });
            
        }

    }
}
