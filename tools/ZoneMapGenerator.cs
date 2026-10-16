// Writes synthetic zone-map CSVs (Col_0_ZoneID,Country,PostalCodeStart,PostalCodeEnd)
// for the performance benchmarks in src/PostalCodes.Benchmarks.
//
// Usage: dotnet run tools/ZoneMapGenerator.cs -- [--out <dir>] [--sizes 10000,100000,1000000] [--seed 42]
//
// Output is deterministic for a given seed. Files: <shape>-<unique|dup>-<rows>.csv, plus mixed-invalid5-<rows>.csv.
// Shapes:
//   us    US ZIP5 ranges (short format, expanded on load); some codes without leading zero ("1002"), some ZIP9
//   gb    GB, all six formats, short (outward + sector) and full codes
//   nlca  NL 4-digit short and 4+2 full codes, CA full codes
//   mixed proportions seen in production zone maps: ~57% CA, ~37% US, rest 4-7 digit codes from other countries
//         (BR 5-digit hits the DefaultPostalCode fallback, as in production), ~77% single-code ranges,
//         occasional lowercase / separators
// "unique" rows never repeat a (country, start, end) triple, so no row can be served from a parse cache.
// Short-format pools are small (100k ZIP5s), so large unique files lean towards wider ranges, ZIP9 and CA.
// "dup" draws rows from a pool of 10% of the row count (about 27% of rows are unique in production zone maps).
// The invalid variant replaces 5% of mixed rows with values seen in real files ("", "NULL") or codes that
// match no format.

using System.Globalization;
using System.Text;

var outDir = "zonemaps";
var sizes = new[] { 10_000, 100_000, 1_000_000 };
var seed = 42;
for (var i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "--out": outDir = args[++i]; break;
        case "--sizes": sizes = args[++i].Split(',').Select(int.Parse).ToArray(); break;
        case "--seed": seed = int.Parse(args[++i]); break;
        default: Console.Error.WriteLine($"Unknown argument: {args[i]}"); return 1;
    }
}

Directory.CreateDirectory(outDir);

var shapes = new (string Name, Func<Random, Row> Make)[]
{
    ("us", Shapes.Us),
    ("gb", Shapes.Gb),
    ("nlca", Shapes.NlCa),
    ("mixed", Shapes.Mixed),
};

foreach (var rows in sizes)
{
    foreach (var (name, make) in shapes)
    {
        Write(Path.Combine(outDir, $"{name}-unique-{rows}.csv"), Unique(make, rows, new Random(seed)));
        Write(Path.Combine(outDir, $"{name}-dup-{rows}.csv"), Duplicated(make, rows, new Random(seed)));
    }
    Write(Path.Combine(outDir, $"mixed-invalid5-{rows}.csv"), WithInvalid(Shapes.Mixed, rows, 0.05, new Random(seed)));
}
return 0;

static IEnumerable<Row> Unique(Func<Random, Row> make, int count, Random rnd)
{
    var seen = new HashSet<(string, string, string)>();
    var attempts = 0;
    while (seen.Count < count)
    {
        var row = make(rnd);
        if (seen.Add((row.Country, row.Start, row.End)))
        {
            attempts = 0;
            yield return row;
        }
        else if (++attempts > 1000)
        {
            throw new InvalidOperationException($"Shape cannot produce {count} unique rows (stopped at {seen.Count}).");
        }
    }
}

static IEnumerable<Row> Duplicated(Func<Random, Row> make, int count, Random rnd)
{
    var pool = Unique(make, Math.Max(1, count / 10), rnd).ToArray();
    for (var i = 0; i < count; i++)
    {
        yield return pool[rnd.Next(pool.Length)];
    }
}

static IEnumerable<Row> WithInvalid(Func<Random, Row> make, int count, double rate, Random rnd)
{
    string[] bad = ["", "NULL", "x1231s", "1010101010", "12A5AA", "ABCDEFG"];
    foreach (var row in Unique(make, count, rnd))
    {
        if (rnd.NextDouble() < rate)
        {
            var code = bad[rnd.Next(bad.Length)];
            // Mostly both ends invalid (as in real files), sometimes only the end.
            yield return rnd.Next(4) == 0 ? row with { End = code } : row with { Start = code, End = code };
        }
        else
        {
            yield return row;
        }
    }
}

static void Write(string path, IEnumerable<Row> rows)
{
    using var w = new StreamWriter(path, false, new UTF8Encoding(false), 1 << 16);
    w.NewLine = "\n";
    w.WriteLine("Col_0_ZoneID,Country,PostalCodeStart,PostalCodeEnd");
    var n = 0;
    foreach (var r in rows)
    {
        w.Write(r.Zone); w.Write(','); w.Write(r.Country); w.Write(',');
        w.Write(r.Start); w.Write(','); w.WriteLine(r.End);
        n++;
    }
    Console.WriteLine($"{path}: {n} rows");
}

record struct Row(string Zone, string Country, string Start, string End);

static class Shapes
{
    const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string CaLetters = "ABCEGHJKLMNPRSTVXY"; // letters used by Canada Post in the first position
    static readonly string[] Zones = ["Z01", "Z02", "Z03", "Z04", "Z05", "Z06", "Z07", "Z08", "REMOTE", "EXTENDED"];

    static string Zone(Random r) => Zones[r.Next(Zones.Length)];
    static char L(Random r) => Letters[r.Next(Letters.Length)];
    static char D(Random r) => (char)('0' + r.Next(10));
    static string Digits(int value, int width) => value.ToString(CultureInfo.InvariantCulture).PadLeft(width, '0');

    // Range width as seen in production: ~77% single code, ~22% 1-9 wide, rest wider.
    static int Width(Random r)
    {
        var p = r.NextDouble();
        return p < 0.77 ? 0 : p < 0.99 ? r.Next(1, 10) : r.Next(10, 200);
    }

    // Numeric range [start, start + width] of a fixed-width code, clamped to the format maximum.
    static (string, string) NumericRange(Random r, int digits, int min = 0)
    {
        var max = (int)Math.Pow(10, digits) - 1;
        var s = r.Next(min, max + 1);
        var e = Math.Min(max, s + Width(r));
        return (Digits(s, digits), Digits(e, digits));
    }

    public static Row Us(Random r)
    {
        var p = r.NextDouble();
        if (p < 0.05)
        {
            // ZIP9 (default format, no expansion)
            var zip = Digits(r.Next(100_000), 5);
            var (s4, e4) = NumericRange(r, 4);
            return new Row(Zone(r), "US", zip + s4, zip + e4);
        }
        var (s, e) = NumericRange(r, 5);
        if (p < 0.15)
        {
            // Leading zero dropped, as in real files ("1002" -> padded to "01002" by the library)
            s = s.TrimStart('0'); e = e.TrimStart('0');
            if (s.Length == 0) s = "0";
            if (e.Length == 0) e = "0";
        }
        return new Row(Zone(r), "US", s, e);
    }

    // GB formats: pattern of the short form (outward code + sector digit), A = letter, 9 = digit.
    static readonly string[] GbShort = ["AA9A9", "A9A9", "A99", "A999", "AA99", "AA999"];

    public static Row Gb(Random r)
    {
        var pattern = GbShort[r.Next(GbShort.Length)];
        var sb = new StringBuilder();
        foreach (var c in pattern) sb.Append(c == 'A' ? L(r) : D(r));
        var shortCode = sb.ToString();
        if (r.NextDouble() < 0.5)
        {
            return new Row(Zone(r), "GB", shortCode, shortCode);
        }
        // Full code: short + two letters; range over the unit letters.
        var a = r.Next(26 * 26);
        var b = Math.Min(26 * 26 - 1, a + Width(r));
        return new Row(Zone(r), "GB", shortCode + Unit(a), shortCode + Unit(b));

        static string Unit(int i) => $"{Letters[i / 26]}{Letters[i % 26]}";
    }

    public static Row NlCa(Random r) => r.Next(2) == 0 ? Nl(r) : Ca(r);

    static Row Nl(Random r)
    {
        var (s, e) = NumericRange(r, 4, 1000);
        if (r.NextDouble() < 0.5)
        {
            return new Row(Zone(r), "NL", s, e);
        }
        // Full code; SA, SD and SS are not valid letter pairs.
        string Letters2()
        {
            while (true)
            {
                var x = $"{L(r)}{L(r)}";
                if (x is not ("SA" or "SD" or "SS")) return x;
            }
        }
        var start = s + Letters2();
        if (r.NextDouble() < 0.77)
        {
            return new Row(Zone(r), "NL", start, start);
        }
        var end = e + Letters2();
        return string.CompareOrdinal(start, end) <= 0 ? new Row(Zone(r), "NL", start, end) : new Row(Zone(r), "NL", end, start);
    }

    static string CaCode(Random r) =>
        $"{CaLetters[r.Next(CaLetters.Length)]}{D(r)}{L(r)}{D(r)}{L(r)}{D(r)}";

    static Row Ca(Random r)
    {
        var s = CaCode(r);
        if (r.NextDouble() < 0.77)
        {
            return new Row(Zone(r), "CA", s, s);
        }
        // Range within the same forward sortation area, end >= start.
        var tail = $"{D(r)}{L(r)}{D(r)}";
        var e = s[..3] + (string.CompareOrdinal(tail, s[3..]) >= 0 ? tail : s[3..]);
        return new Row(Zone(r), "CA", s, e);
    }

    // Other countries seen in production zone maps, with their code length.
    static readonly (string Country, int Digits)[] Others =
    [
        ("MX", 5), ("BR", 5), ("IN", 6), ("RU", 6), ("CN", 6), ("ES", 5), ("AU", 4), ("ZA", 4),
        ("TR", 5), ("NO", 4), ("AR", 4), ("IT", 5), ("FR", 5), ("SE", 5), ("CZ", 5), ("DE", 5),
        ("AT", 4), ("CH", 4), ("DK", 4), ("JP", 7),
    ];

    public static Row Mixed(Random r)
    {
        var p = r.NextDouble();
        Row row;
        if (p < 0.57)
        {
            row = Ca(r);
        }
        else if (p < 0.94)
        {
            row = Us(r);
        }
        else
        {
            var (country, digits) = Others[r.Next(Others.Length)];
            var (s, e) = NumericRange(r, digits);
            row = new Row(Zone(r), country, s, e);
        }

        // Rare formatting noise seen in real files: lowercase letters, separators.
        var q = r.NextDouble();
        if (q < 0.001)
        {
            row = row with { Start = row.Start.ToLowerInvariant(), End = row.End.ToLowerInvariant() };
        }
        else if (q < 0.002 && row.Country == "CA")
        {
            row = row with { Start = row.Start.Insert(3, " "), End = row.End.Insert(3, " ") };
        }
        return row;
    }
}
