using INOA.StockQuoteAlert.ApplicationWorker;

namespace StockQuoteAlert.ApplicationWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IQuoteService _quoteService;
    private readonly string _qouteCode;
    private readonly decimal _sellPrice;
    private readonly decimal _buyPrice;


    public Worker(ILogger<Worker> logger, IQuoteService quoteService, string quoteCode, decimal sellPrice, decimal buyPrice)
    {
        _logger = logger;
        _quoteService = quoteService;
        _qouteCode = quoteCode;
        _sellPrice = sellPrice;
        _buyPrice = buyPrice;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker start at: {time}", DateTimeOffset.Now);
        while (!stoppingToken.IsCancellationRequested)
        {
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            _quoteService.Monitoring(_qouteCode, _sellPrice, _buyPrice);
            await Task.Delay(1000, stoppingToken);
        }
        _logger.LogInformation("Worker stop running at: {time}", DateTimeOffset.Now);
    }
}

