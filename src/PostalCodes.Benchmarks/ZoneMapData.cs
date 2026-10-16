using System.Text.Json;

namespace PostalCodes.Benchmarks;

public readonly record struct ZoneMapRow(string Country, string Start, string End);

/// <summary>
/// Reads zone-map rows from the files written by tools/ZoneMapGenerator.cs and from real zone-map exports
/// (CSV with Country/PostalCodeStart/PostalCodeEnd columns, JSON arrays with countryCode/postalCodeStart/postalCodeEnd).
/// </summary>
public static class ZoneMapData
{
    /// <summary>
    /// Directory with generated zone maps: $POSTALCODES_ZONEMAPS, or zonemaps/ next to PostalCodes.sln.
    /// </summary>
    public static string Directory
    {
        get
        {
            var fromEnv = Environment.GetEnvironmentVariable("POSTALCODES_ZONEMAPS");
            if (!string.IsNullOrEmpty(fromEnv))
            {
                return fromEnv;
            }

            for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir != null; dir = dir.Parent)
            {
                if (File.Exists(Path.Combine(dir.FullName, "PostalCodes.sln")))
                {
                    return Path.Combine(dir.FullName, "zonemaps");
                }
            }

            throw new InvalidOperationException("Cannot find PostalCodes.sln; set POSTALCODES_ZONEMAPS.");
        }
    }

    /// <summary>
    /// Path of a generated file, e.g. <c>Generated("mixed-unique", 100_000)</c>.
    /// </summary>
    public static string Generated(string variant, int rows)
    {
        var path = Path.Combine(Directory, $"{variant}-{rows}.csv");
        if (!File.Exists(path))
        {
            throw new FileNotFoundException(
                $"{path} not found. Generate it with: dotnet run tools/ZoneMapGenerator.cs -- --out {Directory}", path);
        }
        return path;
    }

    /// <summary>
    /// All files in a directory that contain zone-map rows, in a stable order.
    /// </summary>
    public static IEnumerable<string> FilesIn(string directory) =>
        System.IO.Directory.EnumerateFiles(directory)
            .Where(f => f.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            .Order(StringComparer.Ordinal);

    /// <summary>
    /// Rows of a CSV or JSON zone map. Files without postal code columns yield nothing.
    /// </summary>
    public static IEnumerable<ZoneMapRow> Read(string path) =>
        path.EndsWith(".json", StringComparison.OrdinalIgnoreCase) ? ReadJson(path) : ReadCsv(path);

    static IEnumerable<ZoneMapRow> ReadCsv(string path)
    {
        using var reader = new StreamReader(path);
        var header = reader.ReadLine()?.TrimStart('﻿').Split(',');
        if (header == null)
        {
            yield break;
        }

        var country = Array.IndexOf(header, "Country");
        var start = Array.IndexOf(header, "PostalCodeStart");
        var end = Array.IndexOf(header, "PostalCodeEnd");
        if (country < 0 || start < 0 || end < 0)
        {
            yield break;
        }

        var needed = Math.Max(country, Math.Max(start, end));
        while (reader.ReadLine() is { } line)
        {
            var cols = line.Split(',');
            if (cols.Length > needed)
            {
                yield return new ZoneMapRow(cols[country], cols[start], cols[end]);
            }
        }
    }

    static IEnumerable<ZoneMapRow> ReadJson(string path)
    {
        using var stream = File.OpenRead(path);
        JsonDocument doc;
        try
        {
            doc = JsonDocument.Parse(stream);
        }
        catch (JsonException)
        {
            yield break;
        }

        using (doc)
        {
            if (doc.RootElement.ValueKind != JsonValueKind.Array)
            {
                yield break;
            }

            foreach (var item in doc.RootElement.EnumerateArray())
            {
                if (item.ValueKind != JsonValueKind.Object
                    || !item.TryGetProperty("countryCode", out var country)
                    || !item.TryGetProperty("postalCodeStart", out var start))
                {
                    yield break;
                }

                item.TryGetProperty("postalCodeEnd", out var end);
                yield return new ZoneMapRow(country.GetString() ?? "", start.GetString() ?? "",
                    end.ValueKind == JsonValueKind.String ? end.GetString()! : "");
            }
        }
    }

    /// <summary>
    /// Builds ranges the way a zone-map loader does: two creates plus a range per row, invalid rows skipped.
    /// </summary>
    public static List<PostalCodeRange> Load(IEnumerable<ZoneMapRow> rows)
    {
        var ranges = new List<PostalCodeRange>();
        foreach (var row in rows)
        {
            if (TryCreateRange(row, out var range))
            {
                ranges.Add(range);
            }
        }
        return ranges;
    }

    public static bool TryCreateRange(ZoneMapRow row, out PostalCodeRange range)
    {
        try
        {
            var country = CountryFactory.Instance.CreateCountry(row.Country);
            var start = PostalCodeFactory.Instance.CreatePostalCode(country, row.Start);
            var end = PostalCodeFactory.Instance.CreatePostalCode(country, row.End);
            range = new PostalCodeRange(start, end);
            return true;
        }
        catch (ArgumentException)
        {
            range = null!;
            return false;
        }
    }
}
