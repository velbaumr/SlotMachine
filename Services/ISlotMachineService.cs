using Services.Model;

namespace Services;

public interface ISlotMachineService
{
    IEnumerable<Multiplier> Configuration { get; }
    SpinResult Spin();

    long GetMultiplier(Symbol symbol);

    bool IsWin(IEnumerable<Symbol> symbols);
}