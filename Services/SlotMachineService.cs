using System.Net.Sockets;
using Microsoft.Extensions.Options;
using Services.Exceptions;
using Services.Model;

namespace Services;

public class SlotMachineService: ISlotMachineService
{
    private static Lazy<List<Reel>> _reels;
    private static Lazy<List<Payout>> _payouts;
    public SlotMachineService(IOptions<SlotMachineOptions> options)
    {
 
        try
        {
            foreach (var reel in options.Value.Reels)
            {
     
                    var symbols = reel.Select(x => x).ToList();
                    _reels.Value.Add(new Reel(symbols));
                
            }

            _payouts.Value.AddRange(options.Value.Payouts.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ConfigurationException();
        }
    }
    public async Task<SpinResult> Spin(int? seed)
    {
        throw new NotImplementedException();
    }

    public long CalculatePayout(SpinResult spinResult)
    {
        throw new NotImplementedException();
    }

    private SpinResult SpinReels()
    {
        throw new NotImplementedException();  
    }
}