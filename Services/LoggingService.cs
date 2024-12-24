using Microsoft.Extensions.Logging;
using Services.Model;

namespace Services;

public class LoggingService: ILoggingService
{
    private readonly ILogger _logger;

    public LoggingService(ILogger<LoggingService> logger)
    {
        _logger = logger;
    }

    public void LogSpinResult(LogData logData)
    {

    }

    public void LogSummary(IEnumerable<SpinResult> spinResults)
    {

    }
}