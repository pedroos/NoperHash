namespace PedroOs.NoperHash.Tests;

#if DEBUG

public static partial class Tests {
    public static void T(string id, bool result) =>
        WriteLine($"{id} {result}");
}

#endif