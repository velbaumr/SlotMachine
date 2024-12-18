using Microsoft.Extensions.Options;
using Services.Model;

namespace Services;

public class SlotMachineService: ISlotMachineService
{
    private readonly IOptions<SlotMachineOptions> _configuration;
    public SlotMachineService(IOptions<SlotMachineOptions> options)
    {
        _configuration = options;
    }
    public async Task<SpinResult> Spin()
    {
        throw new NotImplementedException();
    }

    public long CalculatePayout(SpinResult spinResult)
    {
        throw new NotImplementedException();
    }
}