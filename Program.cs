namespace PedroOs.NoperHash;

public class Program {
    public static void Main() {
        Tests.Tests.Run();
        Tests.Tests.RunGeneric();
        Tests.Tests.BasicTests();
        Tests.Tests.ListSize(1000000);
        Tests.Tests.ListSize(1000000, generic: true);
        Tests.Tests.ListSize(10000000);
        Tests.Tests.ListSize(10000000, generic: true);
        Tests.Tests.ListSize(100000000);
        Tests.Tests.ListSize(100000000, generic: true);
    }
}