namespace PedroOs.NoperHash.Tests;

using static System.Console;

#if DEBUG

public static partial class Tests {
    public static void BasicTests() {
        // Non-generic values
        T("76b", NoperHashDouble.Approximately(1.234, 1.23451, 0.1));
        T("751", NoperHashDouble.Approximately(1.234, 1.2, 0.01));
        T("7f9", NoperHashDouble.Exp10(1.234) == 1);
        T("8b7", NoperHashDouble.Exp10(33.4567) == 2);
        T("c8c", NoperHashDouble.Exp10(3047) == 4);
        
        // Means
        T("f45", NoperHashDouble.MeanDouble(new double[] { 1.234, 4.41232, 54.212 })
            .Approximately(19.952, 0.1));
        T("13a", NoperHashDouble.MeanByte(new byte[] { 241, 3, 98 })
            .Approximately(114.0, 0.1));

        // Generic values
        T("e02", 1.234.Approximately(1.23451, 0.1));
        T("df2", 1.234.Approximately(1.2, 0.01));
        T("76c", NoperHash.Exp10(1.234) == 1);
        T("421", NoperHash.Exp10(33.4567) == 2);
        T("75d", NoperHash.Exp10(3047.0) == 4);
        
        // Moving means
        T("f28", NoperHash.MovingMean<double, double>(new double[] { 1.234, 4.41232, 54.212 })
            .Approximately(28.363, 0.1));
    }
}

#endif