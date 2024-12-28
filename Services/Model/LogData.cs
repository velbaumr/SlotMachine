namespace Services.Model;

public class LogData
{
    public required SpinResult Result { get; set; }

    public long Bet { get; set; }

    public int SpinCount { get; set; }

    public long Balance { get; set; }
}