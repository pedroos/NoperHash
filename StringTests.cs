namespace PedroOs.NoperHash.Tests;

using static System.Text.Encoding;
using static System.Console;

#if DEBUG

public static partial class Tests {
    public static void StringGenericTests() {
        double epsilon = NoperHash.DefaultEpsilon<double>();
        
        WriteLine(
            NoperHash.Get(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles())
            .Approximately(
                NoperHash.Get(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles()),
                epsilon
            ));
        WriteLine(!
            NoperHash.Get(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles())
            .Approximately(
                NoperHash.Get(UTF8.GetBytes("abcdefghijklmnopqrstuvwzy").ToDoubles()),
                epsilon
            ));
        WriteLine(!
            NoperHash.Get(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles())
            .Approximately(
                NoperHash.Get(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyza").ToDoubles()),
                epsilon
            ));
        WriteLine(!
            NoperHash.Get(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles())
            .Approximately(
                NoperHash.Get(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz ").ToDoubles()),
                epsilon
            ));
        WriteLine(!
            NoperHash.Get(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz").ToDoubles())
            .Approximately(
                NoperHash.Get(UTF8.GetBytes(" abcdefghijklmnopqrstuvwzyz").ToDoubles()),
                epsilon
            ));
        WriteLine(!
            NoperHash.Get(UTF8.GetBytes(" abcdefghijklmnopqrstuvwzyz").ToDoubles())
            .Approximately(
                NoperHash.Get(UTF8.GetBytes("  abcdefghijklmnopqrstuvwzyz").ToDoubles()),
                epsilon
            ));
        WriteLine(!
            NoperHash.Get(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz ").ToDoubles())
            .Approximately(
                NoperHash.Get(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz  ").ToDoubles()),
                epsilon
            ));
    }
}

#endif