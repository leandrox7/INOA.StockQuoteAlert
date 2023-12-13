using System;
using System.Net.Http;
using INOA.StockQuoteAlert.Domain;
using INOA.StockQuoteAlert.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace INOA.StockQuoteAlert.Test
{
	public static class Utils
	{
		public static IConfiguration GetConfiguration()
		{
            Mock<IConfiguration> _configurationMock;
            _configurationMock = new Mock<IConfiguration>();

            _configurationMock.Setup(config => config.GetSection("AlphaSettings:API_KEY"))
                             .Returns(new Mock<IConfigurationSection>().Object);

            _configurationMock.SetupGet(config => config.GetSection("AlphaSettings:API_KEY").Value)
                             .Returns("SDFSFUEWR234W2");

            _configurationMock.Setup(config => config.GetSection("AlphaSettings:BaseUrl"))
                           .Returns(new Mock<IConfigurationSection>().Object);

            _configurationMock.SetupGet(config => config.GetSection("AlphaSettings:BaseUrl").Value)
                             .Returns("www.google.com");

            return _configurationMock.Object;

        }


        public static IAlphaVantageConnector MakeAlphaVantageConnector(GlobalQuote globalQuote, string quoteCode)
        {

            Mock<IAlphaVantageConnector> _alphaVantageConnector = new Mock<IAlphaVantageConnector>();

            _alphaVantageConnector.Setup(x => x.GetGlobalQuoteAsync(quoteCode))
                                     .ReturnsAsync(globalQuote);

            return _alphaVantageConnector.Object;


        }
    }
}

