namespace Services.Model;

public class Reel(IEnumerable<Symbol> symbols)
{
    private readonly List<Symbol> _symbols = symbols.ToList();

    public Symbol Spin(int? seed)
    {
        var maxPos = _symbols.Count;
        var threadLocalRandom = new ThreadLocal<Random>(() => seed.HasValue ? new Random(seed.Value): new Random());
        if (threadLocalRandom.Value == null) throw new ApplicationException();
        var pos = threadLocalRandom.Value.Next(maxPos);

        return _symbols[pos];
    }
}
