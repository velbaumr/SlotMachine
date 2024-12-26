using Services.Model;

namespace Services;

public interface ISlotMachineService
{
    SpinResult Spin();
    long CalculatePayout(Symbol symbol);

    bool IsWin(IEnumerable<Symbol> symbols);
}