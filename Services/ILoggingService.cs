using Services.Model;

namespace Services;

public interface ILoggingService
{
    void LogSpinResult(LogData logData);
    
    void LogSummary(IEnumerable<SpinResult> spinResults, IEnumerable<Payout> payouts, long totalWin, long totalBet, long totalSpins, long bet);
}