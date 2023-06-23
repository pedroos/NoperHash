namespace PedroOs.NoperHash.Tests;

using static System.Text.Encoding;
using static System.Console;

#if DEBUG

public static partial class Tests {
    public static void StringTests() {
        WriteLine(NoperHashDouble.DoubleEquals(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")), 
            NoperHashDouble.Eps
        ));
        WriteLine(!NoperHashDouble.DoubleEquals(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzy")), 
            NoperHashDouble.Eps
        ));
        WriteLine(!NoperHashDouble.DoubleEquals(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyza")), 
            NoperHashDouble.Eps
        ));
        WriteLine(!NoperHashDouble.DoubleEquals(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz ")), 
            NoperHashDouble.Eps
        ));
        WriteLine(!NoperHashDouble.DoubleEquals(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes(" abcdefghijklmnopqrstuvwzyz")), 
            NoperHashDouble.Eps
        ));
        WriteLine(!NoperHashDouble.DoubleEquals(
            NoperHashDouble.GetStr(UTF8.GetBytes(" abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes("  abcdefghijklmnopqrstuvwzyz")), 
            NoperHashDouble.Eps
        ));
        WriteLine(!NoperHashDouble.DoubleEquals(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz ")),
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz  ")), 
            NoperHashDouble.Eps
        ));
    }
}

#endif