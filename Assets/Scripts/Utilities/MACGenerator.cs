using System;
using System.Linq;

public static class MACGenerator
{
    private static Random random = new Random();

    public static string Generate()
    {
        var buffer = new byte[6];
        random.NextBytes(buffer);
        var result = String.Concat(buffer.Select(x => string.Format("{0}:", x.ToString("X2"))).ToArray());
        return result.TrimEnd(':');
    }
}
