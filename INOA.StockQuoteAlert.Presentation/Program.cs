using System.Net.Http;
using INOA.StockQuoteAlert.ApplicationWorker;
using INOA.StockQuoteAlert.Domain;
using INOA.StockQuoteAlert.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockQuoteAlert.ApplicationWorker;

internal class Program
{

    private static void Main(string[] args)
    {
      //dotnet run PETR4 22.5 24.9
        Console.WriteLine("Start App");
        Console.WriteLine();
        string quote = args[0];
        string sBuyPrice = args[1];
        string sSellPrice = args[2];

        if (String.IsNullOrEmpty(quote))
            Console.WriteLine("Entrada invalida! => quote");

        if (!decimal.TryParse(sBuyPrice, out decimal buyPrice))
            Console.WriteLine("Entrada invalida! => buyPrice");

        if (!decimal.TryParse(sSellPrice, out decimal sellPrice))
            Console.WriteLine("Entrada invalida! => sellPrice");

        Console.WriteLine("Quote: {0}", quote);
        Console.WriteLine("BuyPrice: {0}", buyPrice);
        Console.WriteLine("SellPrice: {0}", sellPrice);

        IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();



        var host = Host.CreateDefaultBuilder(args)
                   .ConfigureServices((hostContext, services) =>
                   {
                       // Registre o ILogger
                       services.AddLogging();

                       // Registre o HttpClient
                       services.AddTransient<HttpClient, HttpClient>();

                       // Registre o IQuoteService
                       services.AddTransient<IQuoteService, QuoteService>();

                       // Registre o IMailService
                       services.AddTransient<IMailService, MailService>();

                       // Registre o IAlphaVantageConnector
                       services.AddTransient<IAlphaVantageConnector, AlphaVantageConnector>();

                       var emailSettings = hostContext.Configuration.GetSection("EmailConfiguration").Get<EmailSettings>();
                       services.AddSingleton(emailSettings);

                       // Registre o ISMTPService
                       services.AddTransient<ISMTPService, SMTPService>();

                       // Registre o worker 
                       services.AddHostedService<Worker>(provider =>
                       {                        
                           var logger = provider.GetRequiredService<ILogger<Worker>>();
                           var quoteService = provider.GetRequiredService<IQuoteService>();
                           var mailService = provider.GetRequiredService<IMailService>();

                           return new Worker(logger, quoteService, quote , sellPrice, buyPrice);
                       });
                   })
                   .Build();

        host.Run();
        Console.WriteLine("Monitoramento Iniciado");
        
    }
}

