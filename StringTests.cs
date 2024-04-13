namespace PedroOs.NoperHash.Tests;

using static System.Text.Encoding;
using static System.Console;

#if DEBUG

public static partial class Tests {
    public static void StringGenericTests() {
        double epsilon = NoperHash.DefaultEpsilon<double>();
        T("48f", 
            NoperHash.Get("abcdefghijklmnopqrstuvwzyz")
            .Approximately(
                NoperHash.Get("abcdefghijklmnopqrstuvwzyz"),
                epsilon
        ));
        T("72d", !
            NoperHash.Get<double, char>("abcdefghijklmnopqrstuvwzyz")
            .Approximately(
                NoperHash.Get<double, char>("abcdefghijklmnopqrstuvwzy"),
                epsilon
        ));
        T("a3d", !
            NoperHash.Get("abcdefghijklmnopqrstuvwzyz")
            .Approximately(
                NoperHash.Get("abcdefghijklmnopqrstuvwzyza"),
                epsilon
        ));
        T("f2d", !
            NoperHash.Get("abcdefghijklmnopqrstuvwzyz")
            .Approximately(
                NoperHash.Get("abcdefghijklmnopqrstuvwzyz "),
                epsilon
        ));
        T("44b", !
            NoperHash.Get("abcdefghijklmnopqrstuvwzyz")
            .Approximately(
                NoperHash.Get(" abcdefghijklmnopqrstuvwzyz"),
                epsilon
        ));
        T("fb3", !
            NoperHash.Get(" abcdefghijklmnopqrstuvwzyz")
            .Approximately(
                NoperHash.Get("  abcdefghijklmnopqrstuvwzyz"),
                epsilon
        ));
        T("b76", !
            NoperHash.Get("abcdefghijklmnopqrstuvwzyz ")
            .Approximately(
                NoperHash.Get("abcdefghijklmnopqrstuvwzyz  "),
                epsilon
        ));
    }
}

#endif