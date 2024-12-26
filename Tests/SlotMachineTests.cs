using Microsoft.Extensions.Options;
using Services;
using Services.Model;

namespace Tests;

public class SlotMachineTests
{
    private static SlotMachineService CreateSlotMachine()
    {
        var settings = new SlotMachineOptions
        {
            Reels = 
            [
                new List<Symbol>
                {
                    Symbol.Bar, Symbol.Cherry, Symbol.Bar, Symbol.Bar, Symbol.Bar, Symbol.Bar,
                    Symbol.Bar, Symbol.Cherry, Symbol.Bar, Symbol.Bar, Symbol.Sevens, Symbol.Cherry,
                    Symbol.Cherry, Symbol.Cherry, Symbol.Bar, Symbol.Bar, Symbol.Cherry, Symbol.Bar,
                    Symbol.Cherry, Symbol.Bar, Symbol.Wild
                },
                new List<Symbol>
                {
                    Symbol.Bar, Symbol.Bar, Symbol.Bar, Symbol.Bar, Symbol.Cherry, Symbol.Bar,
                    Symbol.Bar,  Symbol.Sevens, Symbol.Bar, Symbol.Bar, Symbol.Cherry, Symbol.Cherry,
                    Symbol.Sevens, Symbol.Bar, Symbol.Bar, Symbol.Bar, Symbol.Bar, Symbol.Cherry,
                    Symbol.Bar, Symbol.Cherry, Symbol.Wild
                    
                },
                new List<Symbol>
                {
                    Symbol.Sevens, Symbol.Cherry, Symbol.Bar, Symbol.Cherry, Symbol.Bar, Symbol.Bar,
                    Symbol.Bar, Symbol.Cherry, Symbol.Bar, Symbol.Bar, Symbol.Cherry, Symbol.Bar,
                    Symbol.Bar, Symbol.Cherry, Symbol.Cherry, Symbol.Bar, Symbol.Bar, Symbol.Bar,
                    Symbol.Cherry, Symbol.Bar, Symbol.Wild
                }
            ],
            Payouts = [
                new Payout
                {
                    Symbol   = Symbol.Bar,
                    Amount = 1
                },
                new Payout
                {
                    Symbol = Symbol.Cherry,
                    Amount = 18
                },
                new Payout
                {
                    Symbol = Symbol.Sevens,
                    Amount = 50
                },
                new Payout
                {
                    Symbol = Symbol.Wild,
                    Amount = 100
                }
            ]
                
        };
        
        var options  = Options.Create(settings);
        
        return new SlotMachineService(options);
    }
    
    [Theory]
    [

        InlineData(Symbol.Bar, 1),
        InlineData(Symbol.Cherry, 18),
        InlineData(Symbol.Sevens, 50),
        InlineData(Symbol.Wild, 100)
    ]
    public void ItShouldCalculatePayout(Symbol symbol, long excepted)
    {
        var slotMachine = CreateSlotMachine();
        
        var result =slotMachine.CalculatePayout(symbol);
        Assert.Equal(excepted, result);
    }

    [Theory]
    [
        InlineData(new[] { Symbol.Wild, Symbol.Wild, Symbol.Wild }, true),
        InlineData(new[] { Symbol.Bar, Symbol.Wild, Symbol.Wild }, true),
        InlineData(new[] { Symbol.Bar, Symbol.Bar, Symbol.Wild }, true),
        InlineData(new[] { Symbol.Bar, Symbol.Bar, Symbol.Bar }, true),
        InlineData(new[] { Symbol.Cherry, Symbol.Cherry, Symbol.Cherry }, true),
        InlineData(new[] { Symbol.Sevens, Symbol.Sevens, Symbol.Sevens }, true),
        InlineData(new[] { Symbol.Bar, Symbol.Sevens, Symbol.Wild }, false),
        InlineData(new[] { Symbol.Bar, Symbol.Sevens, Symbol.Cherry }, false),
        InlineData(new[] { Symbol.Bar, Symbol.Bar, Symbol.Sevens }, false)
    ]
    public void ItShouldCheckWinningSituation(IList<Symbol> symbols, bool excepted)
    {
        var slotMachine = CreateSlotMachine();
        var result = slotMachine.IsWin(symbols.ToList());
        
        Assert.Equal(excepted, result);
    }
}