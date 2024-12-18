namespace Services.Model;

public class SpinResult
{
    public IList<Symbol>? Symbols { get; set; }
    
    public long Payout { get; set; }
}