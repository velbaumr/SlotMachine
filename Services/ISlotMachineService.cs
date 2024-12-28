using Services.Model;

namespace Services;

public interface ISlotMachineService
{
    SpinResult Spin();
    
    long GetMultiplier(Symbol symbol);

    bool IsWin(IEnumerable<Symbol> symbols);
    
    IEnumerable<Multiplier> Configuration { get; }
}