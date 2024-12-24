using Services.Model;

namespace Services;

public interface ISlotMachineService
{
    SpinResult Spin(int? seed);
    long CalculatePayout(Symbol symbol);

    bool IsWin(IEnumerable<Symbol> symbols);
}