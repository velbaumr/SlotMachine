using Services.Model;

namespace Services;

public interface ILoggingService
{
    void LogSpinResult(LogData logData);
    
    void LogSummary(IEnumerable<SpinResult> spinResults);
}