namespace PedroOs.NoperHash.Tests;

using static System.Console;

public static partial class Tests {
    public static void BasicTests() {
        WriteLine("Non-generic values");
        WriteLine();
        WriteLine(NoperHash.DoubleEquals(1.234, 1.23451, 0.1));
        WriteLine(NoperHash.DoubleEquals(1.234, 1.2, 0.01));
        WriteLine(NoperHash.Exp10(1.234));
        WriteLine(NoperHash.Exp10(33.4567));
        WriteLine(NoperHash.Exp10(3047));
        WriteLine();
        WriteLine("Means:");
        WriteLine(NoperHash.MeanDouble(new double[] { 1.234, 4.41232, 54.212 }));
        WriteLine(NoperHash.MeanByte(new byte[] { 241, 3, 98 }));
        WriteLine();

        WriteLine("Generic values");
        WriteLine();
        WriteLine(NoperHashGeneric.Approximately(1.234, 1.23451, 0.1));
        WriteLine(NoperHashGeneric.Approximately(1.234, 1.2, 0.01));
        WriteLine(NoperHashGeneric.Exp10(1.234));
        WriteLine(NoperHashGeneric.Exp10(33.4567));
        WriteLine(NoperHashGeneric.Exp10(3047.0));
        WriteLine();
        WriteLine("Means:");
        WriteLine(NoperHashGeneric.Mean(new double[] { 1.234, 4.41232, 54.212 }));
        WriteLine(NoperHashGeneric.Mean(new long[] { 241, 3, 98 }));
        WriteLine(NoperHashGeneric.Mean(new long[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine(NoperHashGeneric.Mean(new int[] { 241, 3, 98 }));
        WriteLine(NoperHashGeneric.Mean(new int[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine(NoperHashGeneric.Mean(new short[] { 241, 3, 98 }));
        WriteLine(NoperHashGeneric.Mean(new short[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine(NoperHashGeneric.Mean(new byte[] { 241, 3, 98 }));
        WriteLine(NoperHashGeneric.Mean(new byte[] { 241, 3, 98 }.Select(x => (long)x).ToArray()));
        WriteLine(NoperHashGeneric.Mean(new byte[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine();
        WriteLine("Moving means:");
        WriteLine(NoperHashGeneric.MovingMean(new double[] { 1.234, 4.41232, 54.212 }));
        WriteLine(NoperHashGeneric.MovingMean(new long[] { 241, 3, 98 }));
        WriteLine(NoperHashGeneric.MovingMean(new long[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine(NoperHashGeneric.MovingMean(new int[] { 241, 3, 98 }));
        WriteLine(NoperHashGeneric.MovingMean(new int[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine(NoperHashGeneric.MovingMean(new short[] { 241, 3, 98 }));
        WriteLine(NoperHashGeneric.MovingMean(new short[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine(NoperHashGeneric.MovingMean(new byte[] { 241, 3, 98 }));
        WriteLine(NoperHashGeneric.MovingMean(new byte[] { 241, 3, 98 }.Select(x => (long)x).ToArray()));
        WriteLine(NoperHashGeneric.MovingMean(new byte[] { 241, 3, 98 }.Select(x => (double)x).ToArray()));
        WriteLine();
    }
}