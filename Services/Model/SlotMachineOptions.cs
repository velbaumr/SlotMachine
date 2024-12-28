namespace Services.Model;

public class SlotMachineOptions
{
    public IEnumerable<IEnumerable<Symbol>>? Reels { get; set; }
    
    public IEnumerable<Payout>? Payouts { get; set; }
}