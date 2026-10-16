using BenchmarkDotNet.Attributes;

namespace PostalCodes.Benchmarks;

/// <summary>
/// P6: 1M creates from the generated mixed zone map with 100% / 10% / one unique code,
/// on 1 and 8 threads, with string interning on and off.
/// </summary>
[MemoryDiagnoser]
[SimpleJob(warmupCount: 2, iterationCount: 10)]
public class BulkParseBenchmarks
{
    const int Count = 1_000_000;

    [Params(100, 10, 0)]
    public int UniquePercent { get; set; }

    [Params(1, 8)]
    public int Threads { get; set; }

    [Params(true, false)]
    public bool Interning { get; set; }

    (Country Country, string Code)[] _codes = null!;

    [GlobalSetup]
    public void Setup()
    {
        PostalCode.AllowPostalCodeStringInterning = Interning;

        var pool = ZoneMapData.Read(ZoneMapData.Generated("mixed-unique", Count))
            .Select(r => (Country: CountryFactory.Instance.CreateCountry(r.Country), Code: r.Start))
            .ToArray();
        var distinct = UniquePercent == 0 ? 1 : Count * UniquePercent / 100;
        _codes = Enumerable.Range(0, Count).Select(i => pool[i % distinct]).ToArray();
    }

    [GlobalCleanup]
    public void Cleanup() => PostalCode.AllowPostalCodeStringInterning = true;

    [Benchmark(OperationsPerInvoke = Count)]
    public void Parse()
    {
        if (Threads == 1)
        {
            ParseSlice(0, _codes.Length);
            return;
        }

        var slice = (_codes.Length + Threads - 1) / Threads;
        Parallel.For(0, Threads, new ParallelOptions { MaxDegreeOfParallelism = Threads },
            t => ParseSlice(t * slice, Math.Min(_codes.Length, (t + 1) * slice)));
    }

    void ParseSlice(int from, int to)
    {
        var factory = PostalCodeFactory.Instance;
        for (var i = from; i < to; i++)
        {
            var (country, code) = _codes[i];
            factory.CreatePostalCode(country, code);
        }
    }
}
