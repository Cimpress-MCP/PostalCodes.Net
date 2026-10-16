using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace PostalCodes.Benchmarks;

/// <summary>
/// Correctness guard. <c>dump</c> writes, for every zone-map row, everything observable about the parsed codes and
/// range (ToString, ToHumanReadableString, PostalCodeFormat.Name, Predecessor/Successor, Contains, CompareTo with the
/// previous row, exception type and message). Dumps from PostalCodes.Benchmarks.Baseline (released package) and
/// PostalCodes.Benchmarks (this build) are then checked with <c>compare</c>; any difference is a behaviour change.
/// </summary>
public static class Differential
{
    public static string LibraryVersion =>
        typeof(PostalCode).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "?";

    public static int Dump(string output, IEnumerable<string> directories)
    {
        using var file = File.Create(output);
        using var gz = new GZipStream(file, CompressionLevel.Fastest);
        using var w = new StreamWriter(gz, new UTF8Encoding(false), 1 << 16) { NewLine = "\n" };
        w.WriteLine($"# PostalCodes {LibraryVersion}");

        long rows = 0;
        foreach (var dir in directories)
        {
            foreach (var path in ZoneMapData.FilesIn(dir))
            {
                var any = false;
                PostalCode? previous = null;
                PostalCodeRange? previousRange = null;
                foreach (var row in ZoneMapData.Read(path))
                {
                    if (!any)
                    {
                        w.WriteLine($"## {Path.GetFileName(path)}");
                        any = true;
                    }
                    var sb = new StringBuilder();
                    sb.Append(row.Country).Append(',').Append(row.Start).Append(',').Append(row.End).Append(" => ");
                    DumpRow(sb, row, ref previous, ref previousRange);
                    w.WriteLine(sb.ToString());
                    rows++;
                }
            }
        }
        Console.WriteLine($"{output}: {rows} rows");
        return 0;
    }

    static void DumpRow(StringBuilder sb, ZoneMapRow row, ref PostalCode? previous, ref PostalCodeRange? previousRange)
    {
        Country country;
        try
        {
            country = CountryFactory.Instance.CreateCountry(row.Country);
        }
        catch (Exception e)
        {
            sb.Append("country ").Append(Error(e));
            return;
        }

        var start = Code(sb.Append("start "), country, row.Start);
        var end = Code(sb.Append(" | end "), country, row.End);
        if (start != null && previous != null)
        {
            var prev = previous;
            sb.Append(" | cmpPrev ").Append(Try(() => Math.Sign(start.CompareTo(prev)).ToString()));
        }
        previous = start ?? previous;

        if (start == null || end == null)
        {
            return;
        }

        PostalCodeRange range;
        try
        {
            range = new PostalCodeRange(start, end);
        }
        catch (Exception e)
        {
            sb.Append(" | range ").Append(Error(e));
            return;
        }

        sb.Append(" | range ").Append(range)
            .Append(" [").Append(range.Start).Append(';').Append(range.End).Append(']')
            .Append(" contains start ").Append(Try(() => B(range.Contains(start))))
            .Append(" end ").Append(Try(() => B(range.Contains(end))))
            .Append(" pred ").Append(Try(() => B(range.Contains(start.Predecessor))))
            .Append(" succ ").Append(Try(() => B(range.Contains(end.Successor))));
        if (previousRange != null)
        {
            var other = previousRange;
            sb.Append(" cmpPrev ").Append(Try(() => Math.Sign(range.CompareTo(other)).ToString()));
        }
        previousRange = range;
    }

    static PostalCode? Code(StringBuilder sb, Country country, string raw)
    {
        PostalCode code;
        try
        {
            code = PostalCodeFactory.Instance.CreatePostalCode(country, raw);
        }
        catch (Exception e)
        {
            sb.Append(Error(e));
            return null;
        }

        if (code == null)
        {
            sb.Append("null");
            return null;
        }

        sb.Append(code).Append(" '").Append(Try(code.ToHumanReadableString)).Append("' ")
            .Append(code.PostalCodeFormat?.Name ?? "-").Append(" pred ").Append(Try(() => code.Predecessor?.ToString() ?? "null"))
            .Append(" succ ").Append(Try(() => code.Successor?.ToString() ?? "null"));
        return code;
    }

    static string B(bool b) => b ? "1" : "0";

    static string Try(Func<string> f)
    {
        try
        {
            return f();
        }
        catch (Exception e)
        {
            return Error(e);
        }
    }

    static string Error(Exception e) => $"!{e.GetType().Name}: {e.Message.Replace('\n', ' ')}";

    /// <summary>Compares two dumps line by line, ignoring the version header. Exit code 1 on any difference.</summary>
    public static int Compare(string expectedPath, string actualPath, int maxShown = 20)
    {
        using var expected = Open(expectedPath);
        using var actual = Open(actualPath);
        Console.WriteLine($"expected: {expected.ReadLine()} ({expectedPath})");
        Console.WriteLine($"actual:   {actual.ReadLine()} ({actualPath})");

        long line = 1, differences = 0;
        var file = "";
        while (true)
        {
            var e = expected.ReadLine();
            var a = actual.ReadLine();
            line++;
            if (e == null && a == null)
            {
                break;
            }
            if (e != null && e.StartsWith("## "))
            {
                file = e[3..];
            }
            if (e != a)
            {
                if (++differences <= maxShown)
                {
                    Console.WriteLine($"{file} line {line}:");
                    Console.WriteLine($"  - {e ?? "<end of dump>"}");
                    Console.WriteLine($"  + {a ?? "<end of dump>"}");
                }
                if (e == null || a == null)
                {
                    break;
                }
            }
        }

        Console.WriteLine(differences == 0 ? $"identical ({line - 1} lines)" : $"{differences} differing lines");
        return differences == 0 ? 0 : 1;

        static StreamReader Open(string path) => new(new GZipStream(File.OpenRead(path), CompressionMode.Decompress));
    }
}
