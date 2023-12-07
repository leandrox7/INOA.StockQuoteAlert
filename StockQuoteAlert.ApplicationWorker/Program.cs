using INOA.StockQuoteAlert.ApplicationWorker;
using StockQuoteAlert.ApplicationWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<IQuoteService>();
        services.AddTransient<IMailService>();

    })
    .Build();

await host.RunAsync();

