namespace PedroOs.NoperHash;

public class Program {
    public static void Main() {
#if DEBUG
        Tests.Tests.Run();
        Tests.Tests.RunGeneric();
        Tests.Tests.BasicTests();
        Tests.Tests.StringTests();
        Tests.Tests.StringGenericTests();

        Tests.Tests.ListSize(1000000);
        Tests.Tests.ListSize(1000000, generic: true);
        Tests.Tests.ListSize(10000000);
        Tests.Tests.ListSize(10000000, generic: true);
        Tests.Tests.ListSize(100000000);
        Tests.Tests.ListSize(100000000, generic: true);

        NoperHashProfile.Profile(Tests.Tests.RandomList(100000));
#endif
    }
}