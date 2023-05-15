namespace PedroOs.NoperHash.Tests;

using static System.Text.Encoding;
using static System.Console;

public static partial class Tests {
    public static void StringTests() {
        WriteLine(NoperHash.DoubleEquals(
            NoperHash.CalcStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHash.CalcStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")), 
            NoperHash.Eps
        ));
        WriteLine(!NoperHash.DoubleEquals(
            NoperHash.CalcStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHash.CalcStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzy")), 
            NoperHash.Eps
        ));
        WriteLine(!NoperHash.DoubleEquals(
            NoperHash.CalcStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHash.CalcStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyza")), 
            NoperHash.Eps
        ));
        WriteLine(!NoperHash.DoubleEquals(
            NoperHash.CalcStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHash.CalcStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz ")), 
            NoperHash.Eps
        ));
        WriteLine(!NoperHash.DoubleEquals(
            NoperHash.CalcStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHash.CalcStr(UTF8.GetBytes(" abcdefghijklmnopqrstuvwzyz")), 
            NoperHash.Eps
        ));
        WriteLine(!NoperHash.DoubleEquals(
            NoperHash.CalcStr(UTF8.GetBytes(" abcdefghijklmnopqrstuvwzyz")),
            NoperHash.CalcStr(UTF8.GetBytes("  abcdefghijklmnopqrstuvwzyz")), 
            NoperHash.Eps
        ));
        WriteLine(!NoperHash.DoubleEquals(
            NoperHash.CalcStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz ")),
            NoperHash.CalcStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz  ")), 
            NoperHash.Eps
        ));
    }
}