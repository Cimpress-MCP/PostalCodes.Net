using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using PostalCodes.Benchmarks;

// bench    [--server-gc] [BDN args]       P1-P4, P6 and the zone-map load benchmark
// measure  [p5|m1|m2|m3|m4|m5|all]         stopwatch/GC measurements, each in a fresh process
// dump     <out.gz> [dir...]               differential dump of the generated zone maps (and extra dirs)
// compare  <expected.gz> <actual.gz>       differential check of two dumps
switch (args.FirstOrDefault())
{
    case "measure":
        return Measurements.Run(args[1..]);
    case "measure-child":
        return Measurements.RunChild(args[1..]);
    case "dump" when args.Length >= 2:
        return Differential.Dump(args[1], new[] { ZoneMapData.Directory }.Concat(args[2..]));
    case "compare" when args.Length == 3:
        return Differential.Compare(args[1], args[2]);
    case "bench":
        // --server-gc: run every job with Server GC, as production does (BenchmarkDotNet 0.15 has no CLI switch for it).
        var config = DefaultConfig.Instance;
        if (args.Contains("--server-gc"))
        {
            config = config.AddJob(Job.Default.WithGcServer(true).AsMutator());
        }
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args[1..].Where(a => a != "--server-gc").ToArray(), config);
        return 0;
    default:
        Console.Error.WriteLine("Usage: bench [BenchmarkDotNet args] | measure [p5|m1|m2|m3|m4|m5|all] | dump <out.gz> [dir...] | compare <expected.gz> <actual.gz>");
        return 1;
}
