using System.Diagnostics;
using System.Globalization;

namespace PostalCodes.Benchmarks;

/// <summary>
/// Stopwatch / GC measurements that BenchmarkDotNet cannot express. Every run happens in a fresh child
/// process, so the regex cache and the string intern pool start empty.
///
/// P5  cold start: first create for every country in a fresh process
/// M1  retained bytes per parsed code / range
/// M2  allocations, GC counts, peak working set and LOH while loading a zone map
/// M3  reload loop (library level): load 10 times, drop the previous result, heap after each full GC
/// M4  same postcode created N times and kept: retained memory as a function of N
/// M5  string intern pool: memory still retained after all parsed codes are dropped, interning on vs off
/// </summary>
public static class Measurements
{
    static readonly string[] All = ["p5", "m1", "m2", "m3", "m4", "m5"];

    public static int Run(string[] args)
    {
        var tests = args.Length == 0 || args.Contains("all") ? All : args;
        foreach (var test in tests)
        {
            switch (test)
            {
                case "p5": P5(); break;
                case "m1": M1(); break;
                case "m2": M2(); break;
                case "m3": M3(); break;
                case "m4": M4(); break;
                case "m5": M5(); break;
                default: Console.Error.WriteLine($"Unknown measurement: {test}. Known: {string.Join(' ', All)}"); return 1;
            }
            Console.WriteLine();
        }
        return 0;
    }

    /// <summary>Entry point of a child process: <c>measure-child &lt;name&gt; [args]</c>. Prints one result line.</summary>
    public static int RunChild(string[] args)
    {
        var a = args.Skip(1).ToArray();
        Console.WriteLine(args[0] switch
        {
            "p5" => ChildP5(),
            "m1" => ChildM1(a[0], bool.Parse(a[1])),
            "m2" => ChildM2(a[0]),
            "m3" => ChildM3(a[0], bool.Parse(a[1])),
            "m4" => ChildM4(int.Parse(a[0], CultureInfo.InvariantCulture)),
            "m5" => ChildM5(a[0], bool.Parse(a[1])),
            _ => throw new ArgumentException($"Unknown child measurement {args[0]}"),
        });
        return 0;
    }

    // ---- P5 -------------------------------------------------------------------------------------------

    // One valid code per generated country class, plus BR for the DefaultPostalCode fallback (134 in total).
    static readonly (string Country, string Code)[] CountrySamples =
    [
        ("AF", "1234"), ("AL", "1234"), ("AM", "1234"), ("AR", "1234"), ("AS", "12345"), ("AT", "1234"), ("AU", "1234"),
        ("BA", "12345"), ("BB", "12345"), ("BD", "1234"), ("BE", "1234"), ("BG", "1234"), ("BO", "1234"), ("BT", "12345"),
        ("BY", "12345"), ("CA", "A1A1A1"), ("CC", "1234"), ("CH", "1234"), ("CN", "12345"), ("CO", "12345"), ("CR", "12345"),
        ("CU", "12345"), ("CV", "1234"), ("CX", "1234"), ("CY", "1234"), ("CZ", "12345"), ("DE", "12345"), ("DK", "1234"),
        ("DO", "12345"), ("DZ", "12345"), ("EC", "12345"), ("EE", "12345"), ("EG", "12345"), ("ES", "12345"), ("ET", "1234"),
        ("FI", "12345"), ("FM", "12345"), ("FO", "123"), ("FR", "12345"), ("GB", "AD100"), ("GE", "1234"), ("GL", "1234"),
        ("GR", "12345"), ("GT", "12345"), ("GU", "12345"), ("GW", "1234"), ("HM", "1234"), ("HN", "12345"), ("HR", "12345"),
        ("HT", "1234"), ("HU", "1234"), ("ID", "12345"), ("IL", "12345"), ("IN", "12345"), ("IQ", "12345"), ("IS", "123"),
        ("IT", "12345"), ("JO", "12345"), ("JP", "12345"), ("KE", "12345"), ("KG", "12345"), ("KH", "12345"), ("KW", "12345"),
        ("KZ", "12345"), ("LA", "12345"), ("LI", "1234"), ("LK", "12345"), ("LR", "1234"), ("LS", "123"), ("LU", "1234"),
        ("LY", "12345"), ("MA", "12345"), ("MC", "12345"), ("ME", "12345"), ("MG", "123"), ("MH", "12345"), ("MK", "1234"),
        ("MM", "12345"), ("MN", "12345"), ("MP", "12345"), ("MQ", "12345"), ("MT", "NSW2000"), ("MU", "12345"), ("MX", "12345"),
        ("MY", "12345"), ("MZ", "1234"), ("NA", "12345"), ("NE", "1234"), ("NF", "1234"), ("NG", "12345"), ("NI", "12345"),
        ("NL", "1234"), ("NO", "1234"), ("NP", "12345"), ("NZ", "1234"), ("OM", "123"), ("PA", "12345"), ("PE", "12345"),
        ("PG", "123"), ("PH", "1234"), ("PK", "12345"), ("PL", "12345"), ("PR", "12345"), ("PS", "123"), ("PT", "12345"),
        ("PW", "12345"), ("PY", "1234"), ("RO", "12345"), ("RS", "12345"), ("RU", "12345"), ("SD", "12345"), ("SE", "12345"),
        ("SG", "12345"), ("SI", "1234"), ("SJ", "1234"), ("SK", "12345"), ("SN", "12345"), ("TD", "12345"), ("TH", "12345"),
        ("TJ", "12345"), ("TM", "12345"), ("TN", "1234"), ("TR", "12345"), ("TT", "12345"), ("TW", "12345"), ("UA", "12345"),
        ("US", "12345"), ("UY", "12345"), ("VI", "12345"), ("VN", "12345"), ("YT", "12345"), ("ZA", "1234"), ("ZM", "12345"),
        ("BR", "12345"),
    ];

    const int ColdRuns = 10;

    static void P5()
    {
        Console.WriteLine($"## P5 cold start: first create for all {CountrySamples.Length} countries, {ColdRuns} fresh processes");
        var runs = Enumerable.Range(0, ColdRuns).Select(_ => Child("p5").Split(' ').Select(s => Ms(s)).ToArray()).ToArray();
        var first = Median(runs.Select(r => r[0]));
        var total = Median(runs.Select(r => r[1]));
        var perCountry = CountrySamples.Select((s, i) => (s.Country, Ms: Median(runs.Select(r => r[i + 2])))).ToArray();
        Console.WriteLine("| metric | median ms |");
        Console.WriteLine("|---|---:|");
        Console.WriteLine($"| first create (US, includes factory init) | {first:F2} |");
        Console.WriteLine($"| all countries | {total:F2} |");
        foreach (var (country, ms) in perCountry.OrderByDescending(p => p.Ms).Take(5))
        {
            Console.WriteLine($"| slowest: {country} | {ms:F2} |");
        }

        static double Ms(string s) => double.Parse(s, CultureInfo.InvariantCulture);
    }

    static string ChildP5()
    {
        // Factory and country lookup are initialised by the first create; time it on its own.
        var sw = Stopwatch.StartNew();
        PostalCodeFactory.Instance.CreatePostalCode(CountryFactory.Instance.CreateCountry("US"), "12345");
        var first = sw.Elapsed.TotalMilliseconds;

        var each = new double[CountrySamples.Length];
        var total = Stopwatch.StartNew();
        for (var i = 0; i < CountrySamples.Length; i++)
        {
            var (country, code) = CountrySamples[i];
            var t = Stopwatch.GetTimestamp();
            PostalCodeFactory.Instance.CreatePostalCode(CountryFactory.Instance.CreateCountry(country), code);
            each[i] = Stopwatch.GetElapsedTime(t).TotalMilliseconds;
        }
        var all = total.Elapsed.TotalMilliseconds;
        return string.Join(' ', new[] { first, all }.Concat(each).Select(d => F(d)));
    }

    // ---- M1 -------------------------------------------------------------------------------------------

    static readonly string[] M1Variants = ["us-unique", "gb-unique", "nlca-unique", "mixed-unique"];
    const int M1Rows = 1_000_000;

    static void M1()
    {
        Console.WriteLine($"## M1 retained bytes per object, {M1Rows:N0} rows, interning on (default)");
        Console.WriteLine("| file | codes (start only) B/obj | ranges B/range |");
        Console.WriteLine("|---|---:|---:|");
        foreach (var v in M1Variants)
        {
            var path = ZoneMapData.Generated(v, M1Rows);
            Console.WriteLine($"| {v} | {Child("m1", path, "false")} | {Child("m1", path, "true")} |");
        }
    }

    static string ChildM1(string path, bool ranges)
    {
        var before = FullGcHeap();
        object kept;
        int n;
        if (ranges)
        {
            var list = ZoneMapData.Load(ZoneMapData.Read(path));
            n = list.Count;
            kept = list;
        }
        else
        {
            var list = new List<PostalCode>();
            foreach (var row in ZoneMapData.Read(path))
            {
                list.Add(PostalCodeFactory.Instance.CreatePostalCode(CountryFactory.Instance.CreateCountry(row.Country), row.Start));
            }
            n = list.Count;
            kept = list;
        }
        var after = FullGcHeap();
        // The list's backing array is not part of the per-object cost.
        var listBytes = (long)IntPtr.Size * Capacity(kept);
        GC.KeepAlive(kept);
        return F((after - before - listBytes) / (double)n, "F1");

        static int Capacity(object list) => list switch
        {
            List<PostalCodeRange> r => r.Capacity,
            List<PostalCode> c => c.Capacity,
            _ => 0,
        };
    }

    // ---- M2 -------------------------------------------------------------------------------------------

    static readonly string[] M2Variants = ["us-unique", "mixed-unique", "mixed-dup", "mixed-invalid5"];

    static void M2()
    {
        Console.WriteLine("## M2 load of a 1M-row zone map (read + parse, library only)");
        Console.WriteLine("| file | time ms | allocated MB | gen0 | gen1 | gen2 | heap after MB | LOH after MB | peak working set MB |");
        Console.WriteLine("|---|---:|---:|---:|---:|---:|---:|---:|---:|");
        foreach (var v in M2Variants)
        {
            Console.WriteLine($"| {v} | {Child("m2", ZoneMapData.Generated(v, 1_000_000)).Replace(" ", " | ")} |");
        }
    }

    static string ChildM2(string path)
    {
        var allocated = GC.GetTotalAllocatedBytes(true);
        int g0 = GC.CollectionCount(0), g1 = GC.CollectionCount(1), g2 = GC.CollectionCount(2);
        var sw = Stopwatch.StartNew();
        var ranges = ZoneMapData.Load(ZoneMapData.Read(path));
        var ms = sw.Elapsed.TotalMilliseconds;
        allocated = GC.GetTotalAllocatedBytes(true) - allocated;
        g0 = GC.CollectionCount(0) - g0; g1 = GC.CollectionCount(1) - g1; g2 = GC.CollectionCount(2) - g2;

        var heap = FullGcHeap();
        var loh = GC.GetGCMemoryInfo(GCKind.FullBlocking).GenerationInfo[3].SizeAfterBytes;
        GC.KeepAlive(ranges);
        using var process = Process.GetCurrentProcess();
        return string.Join(' ', F(ms, "F0"), Mb(allocated), g0, g1, g2, Mb(heap), Mb(loh), Mb(process.PeakWorkingSet64));
    }

    // ---- M3 -------------------------------------------------------------------------------------------

    const int Reloads = 10;

    static void M3()
    {
        Console.WriteLine($"## M3 reload loop (library level): mixed-unique 1M loaded {Reloads} times, previous result dropped");
        Console.WriteLine("Pass if the heap at iteration 10 is within ±5% of iteration 2.");
        Console.WriteLine($"| content | {string.Join(" | ", Enumerable.Range(1, Reloads).Select(i => $"#{i} MB"))} | #10 vs #2 |");
        Console.WriteLine($"|---|{string.Concat(Enumerable.Repeat("---:|", Reloads))}---:|");
        var path = ZoneMapData.Generated("mixed-unique", 1_000_000);
        foreach (var changed in new[] { false, true })
        {
            var heaps = Child("m3", path, changed.ToString()).Split(' ').Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
            var delta = (heaps[^1] - heaps[1]) / heaps[1] * 100;
            Console.WriteLine($"| {(changed ? "changed" : "same")} | {string.Join(" | ", heaps.Select(h => F(h, "F1")))} | {delta:+0.0;-0.0}% {(Math.Abs(delta) <= 5 ? "pass" : "FAIL")} |");
        }
    }

    static string ChildM3(string path, bool changed)
    {
        var rows = ZoneMapData.Read(path).ToArray();
        var heaps = new List<string>();
        List<PostalCodeRange>? current = null;
        for (var i = 1; i <= Reloads; i++)
        {
            var iteration = i;
            current = ZoneMapData.Load(changed ? rows.Select(r => Shift(r, iteration)) : rows);
            heaps.Add(F(FullGcHeap() / 1024.0 / 1024.0, "F1"));
        }
        GC.KeepAlive(current);
        GC.KeepAlive(rows);
        return string.Join(' ', heaps);
    }

    /// <summary>
    /// Changes the content of a single-code row so every iteration produces new, still valid codes:
    /// the last digit is shifted by the iteration number (iterations 1..10 give 10 distinct values).
    /// </summary>
    static ZoneMapRow Shift(ZoneMapRow row, int iteration)
    {
        if (row.Start != row.End)
        {
            return row;
        }
        var i = row.Start.LastIndexOfAny("0123456789".ToCharArray());
        if (i < 0)
        {
            return row;
        }
        var chars = row.Start.ToCharArray();
        chars[i] = (char)('0' + (chars[i] - '0' + iteration) % 10);
        var code = new string(chars);
        return row with { Start = code, End = code };
    }

    // ---- M4 -------------------------------------------------------------------------------------------

    static void M4()
    {
        Console.WriteLine("## M4 one postcode (US 12345) created N times, all results kept");
        Console.WriteLine("| N | retained MB | B/create |");
        Console.WriteLine("|---:|---:|---:|");
        foreach (var n in new[] { 1_000, 10_000, 100_000, 1_000_000 })
        {
            var bytes = double.Parse(Child("m4", n.ToString(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
            Console.WriteLine($"| {n:N0} | {bytes / 1024 / 1024:F2} | {bytes / n:F1} |");
        }
    }

    static string ChildM4(int n)
    {
        var us = CountryFactory.Instance.CreateCountry("US");
        PostalCodeFactory.Instance.CreatePostalCode(us, "12345"); // warm up type initialisation
        var kept = new PostalCode[n];
        var before = FullGcHeap();
        for (var i = 0; i < n; i++)
        {
            kept[i] = PostalCodeFactory.Instance.CreatePostalCode(us, "12345");
        }
        var after = FullGcHeap();
        GC.KeepAlive(kept);
        return F(after - before);
    }

    // ---- M5 -------------------------------------------------------------------------------------------

    static void M5()
    {
        Console.WriteLine("## M5 intern pool: heap still retained after 1M parsed start codes are dropped");
        Console.WriteLine("| file | interning on MB | interning off MB |");
        Console.WriteLine("|---|---:|---:|");
        foreach (var v in new[] { "mixed-unique", "mixed-dup" })
        {
            var path = ZoneMapData.Generated(v, 1_000_000);
            Console.WriteLine($"| {v} | {Child("m5", path, "true")} | {Child("m5", path, "false")} |");
        }
    }

    static string ChildM5(string path, bool interning)
    {
        PostalCode.AllowPostalCodeStringInterning = interning;
        var us = CountryFactory.Instance.CreateCountry("US");
        PostalCodeFactory.Instance.CreatePostalCode(us, "12345"); // warm up type initialisation
        var before = FullGcHeap();
        Parse(path);
        var after = FullGcHeap();
        return Mb(after - before);

        static void Parse(string path)
        {
            var list = new List<PostalCode>();
            foreach (var row in ZoneMapData.Read(path))
            {
                try
                {
                    list.Add(PostalCodeFactory.Instance.CreatePostalCode(CountryFactory.Instance.CreateCountry(row.Country), row.Start));
                }
                catch (ArgumentException)
                {
                }
            }
            GC.KeepAlive(list);
        }
    }

    // ---- helpers --------------------------------------------------------------------------------------

    static long FullGcHeap()
    {
        GC.Collect(2, GCCollectionMode.Forced, true, true);
        GC.WaitForPendingFinalizers();
        GC.Collect(2, GCCollectionMode.Forced, true, true);
        return GC.GetTotalMemory(true);
    }

    /// <summary>Runs one measurement in a fresh process and returns its result line.</summary>
    static string Child(params string[] args)
    {
        var self = Environment.ProcessPath!;
        var psi = new ProcessStartInfo(self) { RedirectStandardOutput = true, UseShellExecute = false };
        if (Path.GetFileNameWithoutExtension(self) == "dotnet")
        {
            psi.ArgumentList.Add(typeof(Measurements).Assembly.Location);
        }
        psi.ArgumentList.Add("measure-child");
        foreach (var a in args)
        {
            psi.ArgumentList.Add(a);
        }

        using var p = Process.Start(psi)!;
        var output = p.StandardOutput.ReadToEnd().Trim();
        p.WaitForExit();
        if (p.ExitCode != 0)
        {
            throw new InvalidOperationException($"measure-child {string.Join(' ', args)} exited with {p.ExitCode}");
        }
        return output.Split('\n')[^1].Trim();
    }

    static double Median(IEnumerable<double> values)
    {
        var sorted = values.Order().ToArray();
        return sorted.Length % 2 == 1 ? sorted[sorted.Length / 2] : (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]) / 2;
    }

    static string F(double d, string format = "R") => d.ToString(format, CultureInfo.InvariantCulture);
    static string F(long l) => l.ToString(CultureInfo.InvariantCulture);
    static string Mb(long bytes) => F(bytes / 1024.0 / 1024.0, "F1");
}
