namespace PedroOs.NoperHash.Tests;

using static System.Text.Encoding;
using static System.Console;

public static partial class Tests {
    public static void StringGenericTests() {
        double epsilon = NoperHashGeneric.DefaultEpsilon<double>();
        
        WriteLine(NoperHashGeneric.Approximately(
            NoperHashGeneric.Calc(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles()),
            NoperHashGeneric.Calc(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles()),
            epsilon
        ));
        WriteLine(!NoperHashGeneric.Approximately(
            NoperHashGeneric.Calc(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles()),
            NoperHashGeneric.Calc(UTF8.GetBytes("abcdefghijklmnopqrstuvwzy").ToDoubles()),
            epsilon
        ));
        WriteLine(!NoperHashGeneric.Approximately(
            NoperHashGeneric.Calc(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles()),
            NoperHashGeneric.Calc(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyza").ToDoubles()),
            epsilon
        ));
        WriteLine(!NoperHashGeneric.Approximately(
            NoperHashGeneric.Calc(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles()),
            NoperHashGeneric.Calc(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz ").ToDoubles()),
            epsilon
        ));
        WriteLine(!NoperHashGeneric.Approximately(
            NoperHashGeneric.Calc(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles()),
            NoperHashGeneric.Calc(UTF8.GetBytes(" abcdefghijklmnopqrstuvwzyz").ToDoubles()),
            epsilon
        ));
        WriteLine(!NoperHashGeneric.Approximately(
            NoperHashGeneric.Calc(UTF8.GetBytes(" abcdefghijklmnopqrstuvwzyz").ToDoubles()),
            NoperHashGeneric.Calc(UTF8.GetBytes("  abcdefghijklmnopqrstuvwzyz").ToDoubles()),
            epsilon
        ));
        WriteLine(!NoperHashGeneric.Approximately(
            NoperHashGeneric.Calc(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz ").ToDoubles()),
            NoperHashGeneric.Calc(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz  ").ToDoubles()),
            epsilon
        ));
    }
}

public static partial class NoperExtensions {
    public static double[] ToDoubles(this byte[] bytes) => bytes.Select(x => (double)x).ToArray();
}