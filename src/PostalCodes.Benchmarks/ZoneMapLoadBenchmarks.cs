using BenchmarkDotNet.Attributes;

namespace PostalCodes.Benchmarks;

/// <summary>
/// Library-level stand-in for P7: turn already-read zone-map rows into ranges (2 creates + 1 range per row),
/// skipping invalid rows. File reading is done in setup so only library work is measured.
/// </summary>
[MemoryDiagnoser]
[SimpleJob(warmupCount: 1, iterationCount: 5)]
public class ZoneMapLoadBenchmarks
{
    [Params("us-unique", "us-dup", "gb-unique", "nlca-unique", "mixed-unique", "mixed-dup", "mixed-invalid5")]
    public string Variant { get; set; } = "";

    [Params(100_000, 1_000_000)]
    public int Rows { get; set; }

    ZoneMapRow[] _rows = null!;

    [GlobalSetup]
    public void Setup() => _rows = ZoneMapData.Read(ZoneMapData.Generated(Variant, Rows)).ToArray();

    [Benchmark]
    public int Load() => ZoneMapData.Load(_rows).Count;
}
