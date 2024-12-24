using Services;

namespace Client;

public class App(ILoggingService logService, ISlotMachineService slotMachineService)
{
    private readonly ILoggingService _logService = logService;
    private readonly ISlotMachineService _slotMachineService = slotMachineService;
    
    public void Run()
    {
        var seed = Guid.NewGuid().GetHashCode();
        
        Console.WriteLine(seed);
    }

    private void GetUserInput()
    {
        
    }
}