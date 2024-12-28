namespace Services.Model;

public class SpinResult
{
    public required List<Symbol> Symbols { get; set; }
    
    public Symbol? WinningSymbol { get; set; }
    
    public long Multiplier { get; set; }
}