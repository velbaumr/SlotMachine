namespace Client;

public class App(Ilogger logger)
{
    private readonly ILogger _logger = logger;
        
    public readonly long CurrentBalance { get; private set; }
    public readonly long Spins { get; private set; }
    
    public readonly long Bet { get, private set; }

    public Task Run()
    {
        seed = Guid.NewGuid().GetHashCode();
        throw new NotImplementedException();
    }
}