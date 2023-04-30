namespace PedroOs.NoperHash.Tests;

using static System.Console;
using System.Diagnostics;

public static partial class Tests {
    public static void ListSize(int listSize, bool generic = false) {
        var sw = new Stopwatch();
        sw.Start();
        if (!generic)
            _ = NoperHash.CalcDouble(RandomList(listSize));
        else
            _ = NoperHashGeneric.Calc(RandomList(listSize));
        sw.Stop();
        long elapsedMs = sw.ElapsedMilliseconds;
        WriteLine($"List size: {listSize}, elapsed: {elapsedMs} ms");
    }
    
    static double[] RandomList(int quant) {
        var rnd = new Random();
        return Enumerable.Range(1, quant).Select(i => rnd.NextDouble()).ToArray();
    }
}