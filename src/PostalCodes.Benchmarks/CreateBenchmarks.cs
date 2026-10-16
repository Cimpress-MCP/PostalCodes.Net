using BenchmarkDotNet.Attributes;

namespace PostalCodes.Benchmarks;

/// <summary>
/// P1: single create per format. P2: the same postcode created 1M times.
/// </summary>
[MemoryDiagnoser]
public class CreateBenchmarks
{
    public static IEnumerable<object[]> Cases()
    {
        yield return ["US", "12345"];      // ZIP5, short format, expanded on range creation
        yield return ["US", "123456789"];  // ZIP9, default format
        yield return ["US", "501"];        // left padding to 00501
        yield return ["GB", "ZC999"];      // short form of the last GB format: every other regex fails first
        yield return ["NL", "1234AB"];
        yield return ["CA", "K1A0B1"];
        yield return ["BR", "12345"];      // no generated class: DefaultPostalCode fallback
    }

    [Benchmark]
    [ArgumentsSource(nameof(Cases))]
    public PostalCode Create(string country, string code) =>
        PostalCodeFactory.Instance.CreatePostalCode(CountryFactory.Instance.CreateCountry(country), code);

    [Benchmark]
    [Arguments("US", "x1231s")]
    public PostalCode? CreateInvalid(string country, string code)
    {
        try
        {
            return PostalCodeFactory.Instance.CreatePostalCode(CountryFactory.Instance.CreateCountry(country), code);
        }
        catch (ArgumentException)
        {
            return null;
        }
    }

#if !POSTALCODES_BASELINE
    // L8: TryCreatePostalCode is not in the baseline package.
    [Benchmark]
    [Arguments("US", "x1231s")]
    public PostalCode? TryCreateInvalid(string country, string code) =>
        PostalCodeFactory.Instance.TryCreatePostalCode(CountryFactory.Instance.CreateCountry(country), code, out var result)
            ? result
            : null;
#endif

    const int Repeats = 1_000_000;

    [Benchmark(OperationsPerInvoke = Repeats)]
    [Arguments("US", "12345")]
    [Arguments("CA", "K1A0B1")]
    public PostalCode SameCode1M(string country, string code)
    {
        var c = CountryFactory.Instance.CreateCountry(country);
        PostalCode last = null!;
        for (var i = 0; i < Repeats; i++)
        {
            last = PostalCodeFactory.Instance.CreatePostalCode(c, code);
        }
        return last;
    }
}
