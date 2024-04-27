using ProcessamentoArquivos.Service.Interfaces;

namespace ProcessamentoArquivos.Application
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScoped;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScoped)
        {
            _logger = logger;
            _serviceScoped = serviceScoped;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                using(var scope = _serviceScoped.CreateScope())
                {
                    var fileService = scope.ServiceProvider.GetRequiredService<IFileHandlerService>();
                    await fileService.ReadFilesAsync();
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
