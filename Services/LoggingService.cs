using System.Text;
using Services.Model;

namespace Services;

public class LoggingService : ILoggingService
{
    public void LogSpinResult(LogData logData)
    {
        var builder = new StringBuilder();
        var win = logData.Result.Payout * logData.Bet;

        var winString = win == 0 ? string.Empty : $"({ToSymbolString(GetWinningSymbol(logData.Result.Symbols))})";
        var resultText = $"Spin: {logData.SpinCount} Win: {win} {winString} Balance: {logData.Balance}";
        
        foreach (var symbolString in logData.Result.Symbols.Select(ToSymbolString))
        {
            builder.Append($"| {symbolString} ");
        }
        
        builder.Append('|');
        
        Console.WriteLine(builder.ToString());
        Console.WriteLine(resultText);
        Console.WriteLine(string.Empty);
    }

    private static Symbol GetWinningSymbol(IEnumerable<Symbol> symbols)
    {
        var filtered = symbols.Where(f => f != Symbol.Wild).ToList();
        return filtered.Count == 0 ? Symbol.Wild : filtered.First();
    }
    private static string ToSymbolString(Symbol symbol)
    {
        return symbol == Symbol.Sevens ? "777" : symbol.ToString();
    }

    public void LogSummary(IEnumerable<SpinResult> spinResults)
    {
    }
}