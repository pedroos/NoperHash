namespace PedroOs.NoperHash.Tests;

using static System.Text.Encoding;
using static System.Console;

#if DEBUG

public static partial class Tests {
    public static void StringTests() {
        T("bbf", NoperHashDouble.Approximately(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")), 
            NoperHashDouble.Eps
        ));
        T("6e8", !NoperHashDouble.Approximately(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzy")), 
            NoperHashDouble.Eps
        ));
        T("417", !NoperHashDouble.Approximately(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyza")), 
            NoperHashDouble.Eps
        ));
        T("cb4", !NoperHashDouble.Approximately(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz ")), 
            NoperHashDouble.Eps
        ));
        T("bf0", !NoperHashDouble.Approximately(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes(" abcdefghijklmnopqrstuvwzyz")), 
            NoperHashDouble.Eps
        ));
        T("6be", !NoperHashDouble.Approximately(
            NoperHashDouble.GetStr(UTF8.GetBytes(" abcdefghijklmnopqrstuvwzyz")),
            NoperHashDouble.GetStr(UTF8.GetBytes("  abcdefghijklmnopqrstuvwzyz")), 
            NoperHashDouble.Eps
        ));
        T("9d2", !NoperHashDouble.Approximately(
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz ")),
            NoperHashDouble.GetStr(UTF8.GetBytes("abcdefghijklmnopqrstuvwzyz  ")), 
            NoperHashDouble.Eps
        ));
    }
}

#endif