namespace PedroOs.NoperHash.Tests;

using static System.Console;

#if DEBUG

public static partial class Tests {
    public static void BasicTests() {
        WriteLine("Non-generic values");
        WriteLine();
        WriteLine(NoperHashDouble.DoubleEquals(1.234, 1.23451, 0.1));
        WriteLine(NoperHashDouble.DoubleEquals(1.234, 1.2, 0.01));
        WriteLine(NoperHashDouble.Exp10(1.234));
        WriteLine(NoperHashDouble.Exp10(33.4567));
        WriteLine(NoperHashDouble.Exp10(3047));
        WriteLine();
        WriteLine("Means:");
        WriteLine(NoperHashDouble.MeanDouble(new double[] { 1.234, 4.41232, 54.212 }));
        WriteLine(NoperHashDouble.MeanByte(new byte[] { 241, 3, 98 }));
        WriteLine();

        WriteLine("Generic values");
        WriteLine();
        WriteLine(1.234.Approximately(1.23451, 0.1));
        WriteLine(1.234.Approximately(1.2, 0.01));
        WriteLine(NoperHash.Exp10(1.234));
        WriteLine(NoperHash.Exp10(33.4567));
        WriteLine(NoperHash.Exp10(3047.0));
        WriteLine();
        WriteLine("Moving means:");
        WriteLine(NoperHash.MovingMean(new double[] { 1.234, 4.41232, 54.212 }));
        WriteLine(NoperHash.MovingMean(new long[] { 241, 3, 98 }));
        WriteLine(NoperHash.MovingMean(new long[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine(NoperHash.MovingMean(new int[] { 241, 3, 98 }));
        WriteLine(NoperHash.MovingMean(new int[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine(NoperHash.MovingMean(new short[] { 241, 3, 98 }));
        WriteLine(NoperHash.MovingMean(new short[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine(NoperHash.MovingMean(new byte[] { 241, 3, 98 }));
        WriteLine(NoperHash.MovingMean(new byte[] { 241, 3, 98 }.Select(x => (long)x).ToArray()));
        WriteLine(NoperHash.MovingMean(new byte[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine();
    }
}

#endif