using BenchmarkDotNet.Attributes;

namespace PostalCodes.Benchmarks;

/// <summary>
/// P3: range construction (short endpoints get expanded, default ones do not).
/// P4: Contains / CompareTo, the request hot path.
/// </summary>
[MemoryDiagnoser]
public class RangeBenchmarks
{
    PostalCode _usShortStart = null!, _usShortEnd = null!, _usLongStart = null!, _usLongEnd = null!;
    PostalCode _inside = null!, _outside = null!, _other = null!;
    PostalCodeRange _range = null!, _otherRange = null!;

    [GlobalSetup]
    public void Setup()
    {
        var us = CountryFactory.Instance.CreateCountry("US");
        PostalCode Create(string code) => PostalCodeFactory.Instance.CreatePostalCode(us, code);

        _usShortStart = Create("12345");
        _usShortEnd = Create("12399");
        _usLongStart = Create("123450000");
        _usLongEnd = Create("123999999");

        _range = new PostalCodeRange(_usShortStart, _usShortEnd);
        _otherRange = new PostalCodeRange(Create("12400"), Create("12499"));
        _inside = Create("123561234");
        _outside = Create("12400");
        _other = Create("12346");
    }

    [Benchmark]
    public PostalCodeRange NewRangeShort() => new(_usShortStart, _usShortEnd);

    [Benchmark]
    public PostalCodeRange NewRangeDefault() => new(_usLongStart, _usLongEnd);

    [Benchmark]
    public bool ContainsInside() => _range.Contains(_inside);

    [Benchmark]
    public bool ContainsOutside() => _range.Contains(_outside);

    [Benchmark]
    public int CompareCodes() => _inside.CompareTo(_other);

    [Benchmark]
    public int CompareRanges() => _range.CompareTo(_otherRange);
}
