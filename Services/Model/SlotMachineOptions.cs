namespace Services.Model;

public class SlotMachineOptions
{
    public IList<IList<Symbol>>? Reels { get; set; }
    
    public IList<Payout> Payouts { get; set; }
}