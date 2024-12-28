using System.Text;
using Services.Model;

namespace Services;

public class LoggingService : ILoggingService
{
    public void LogSpinResult(LogData logData)
    {
        var builder = new StringBuilder();
        var win = logData.Result.Multiplier * logData.Bet;

        var winString = win == 0 ? string.Empty : $"({ToSymbolString(logData.Result.WinningSymbol ?? throw new InvalidDataException())})";
        var resultText = $"Spin: {logData.SpinCount}, Win: {win} {winString}, Balance: {logData.Balance}";
        
        foreach (var symbolString in logData.Result.Symbols.Select(ToSymbolString))
        {
            builder.Append($"| {symbolString} ");
        }
        
        builder.Append('|');
        
        Console.WriteLine(builder.ToString());
        Console.WriteLine(resultText);
        Console.WriteLine();
    }

    private static string ToSymbolString(Symbol symbol)
    {
        return symbol == Symbol.Sevens ? "777" : symbol.ToString();
    }

    public void LogSummary(IEnumerable<SpinResult> spinResults, IEnumerable<Multiplier> payouts, long totalWin, long totalBet, long totalSpins, long bet)
    {
        var rtp = (int)(totalBet / (double)totalWin * 100);
        Console.WriteLine($"RTP: {rtp}%, Spins: {totalSpins}, Total bet: {totalBet}, Total win: {totalWin}");
        Console.WriteLine();
   
        var groupedWins = spinResults
            .Where(x => x.WinningSymbol != null)
            .GroupBy(x => x.WinningSymbol)
            .ToList();

        foreach (var payout in payouts)
        {
            var data = groupedWins.SingleOrDefault(x => x.Key == payout.Symbol);
            var count = data?.Count() ?? 0;
            var win = count * payout.Amount * bet;
            var plural = count > 1 ? "s" : "";
            
            Console.WriteLine($"{ToSymbolString(payout.Symbol)} - {count} hit{plural}, Total win: {win}");
        }
    }
}