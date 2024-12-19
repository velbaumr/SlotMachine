using Services.Model;

namespace Services;

public interface ISlotMachineService
{
    Task<SpinResult> Spin(int? seed);
    long CalculatePayout(SpinResult spinResult);
}