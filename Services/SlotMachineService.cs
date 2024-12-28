using Microsoft.Extensions.Options;
using Services.Exceptions;
using Services.Model;

namespace Services;

public class SlotMachineService : ISlotMachineService
{
    private readonly IEnumerable<Reel> _reels;

    public SlotMachineService(IOptions<SlotMachineOptions> options)
    {
        _reels = (options.Value.Reels ?? throw new ConfigurationException())
            .Select(r => new Reel(r)).ToList();
        Configuration = (options.Value.Multipliers ?? throw new ConfigurationException())
            .Select(p => p).ToList();
    }

    public SpinResult Spin()
    {
        var result = _reels
            .AsParallel()
            .AsOrdered()
            .Select(x => x.Spin(Guid.NewGuid().GetHashCode()))
            .ToList();

        var isWin = IsWin(result);
        var filtered = result.Where(f => f != Symbol.Wild).ToList();
        var winningSymbol = filtered.Count == 0 ? Symbol.Wild : filtered[0];
        var multiplier = isWin ? GetMultiplier(winningSymbol) : 0;

        return new SpinResult
        {
            Symbols = result,
            Multiplier = multiplier,
            WinningSymbol = isWin ? winningSymbol : null
        };
    }

    public bool IsWin(IEnumerable<Symbol> symbols)
    {
        var filtered = symbols.Where(f => f != Symbol.Wild).ToList();

        return filtered.TrueForAll(f => f == filtered[0]) || filtered.Count == 0;
    }

    public IEnumerable<Multiplier> Configuration { get; }

    public long GetMultiplier(Symbol symbol)
    {
        return Configuration
            .First(y => y.Symbol == symbol).Amount;
    }
}