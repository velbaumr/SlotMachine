using Services.Model;

namespace Services;

public interface ISlotMachineService
{
    Task<SpinResult> Spin();
    long CalculatePayout(SpinResult spinResult);
}