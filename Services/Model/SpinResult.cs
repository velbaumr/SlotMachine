namespace Services.Model;

public class SpinResult
{
    public required List<Symbol> Symbols { get; set; }
    
    public long Payout { get; set; }
}