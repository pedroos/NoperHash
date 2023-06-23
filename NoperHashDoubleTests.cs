namespace PedroOs.NoperHash.Tests;

using static System.Console;

#if DEBUG

public static partial class Tests {
    public static void Run() {
        double Eps = Math.Pow(10, -9);

        // Sample lists tests

        var s01 = new double[] {
            1429041290,1429041350,1429041410,1429041470,1429041530,1429041590,
            1429041650,1429041710,1429041770,1429041830
        };
        var s02 = new double[] { 24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25,28.57 };
        var s03 = new double[] { 4054,0,237,2009,4001,4019,6368,10670,6340,1816 };
        var s04 = new double[] { 226,0,21,156,205,240,446,519,400,127 };
        var s05 = new double[] { 145,0,5,38,114,90,166,312,222,48 };
        var s06 = new double[] { 0.000000101467,0,0.0833333,0.633333,1.9,1.5,2.76667,5.2,3.7,0.8 };
        var s07 = new double[] { 0,0,0 };

        WriteLine(NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s01), 0.37965870745378555, Eps));
        WriteLine(NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s02), 0.49196617766541745, Eps));
        WriteLine(NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s03), 0.6001859386298001, Eps));
        WriteLine(NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s04), 0.5062227481033681, Eps));
        WriteLine(NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s05), 0.5498883933485951, Eps));
        WriteLine(NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s06), 0.6145925169648433, Eps));
        WriteLine(NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s07), 0.0, Eps));

        // Leading zeroes tests

        var s08 = new double[] { 0,1429041290,1429041350,1429041410,1429041470,1429041530 };
        var s09 = new double[] { 1429041290,1429041350,1429041410,1429041470,1429041530 };
        var s10 = new double[] { 0,0,0,24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25 };
        var s11 = new double[] { 24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25 };

        WriteLine(!NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s08), 0, Eps));
        WriteLine(!NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s09), 0, Eps));
        WriteLine(!NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s08), NoperHashDouble.CalcDouble(s09), Eps));
        WriteLine(!NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s10), NoperHashDouble.CalcDouble(s11), Eps));

        // Symmetric modifications tests

        var s12 = new double[] { 5, 62, 4.6, 6.2, 5, 4.3, 5.2 };
        var s13 = new double[] { 5, 6.2, 4.6, 62, 5, 4.3, 5.2 };

        WriteLine(NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s12), NoperHashDouble.CalcDouble(s13), Eps));

        // Asymmetric modifications tests

        var s14 = new double[] { 5, 63, 4.6, 6.2, 5, 4.3, 5.2 };
        var s15 = new double[] { 5, 63, 4.6, 62, 5, 4.3, 5.2 };
        var s16 = new double[] { 5, 6.3, 46, 62, 5, 4.3, 5.2 };
        var s17 = new double[] { 5, 63, 4.6, 6.3, 5, 4.3, 5.2 };
        var s18 = new double[] { 50, 6.3, 46, 0.63, 500, 0.043, .52 };

        WriteLine(!NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s14), NoperHashDouble.CalcDouble(s15), Eps));
        WriteLine(!NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s14), NoperHashDouble.CalcDouble(s16), Eps));
        WriteLine(!NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s14), NoperHashDouble.CalcDouble(s17), Eps));
        WriteLine(!NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s14), NoperHashDouble.CalcDouble(s18), Eps));

        // Subtractive modifications tests

        var s19 = new double[] { 5, 63, 4.6, 6.3, 5, 4.3, 5.2 };
        var s20 = new double[] { 5, 63, 6.3, 4.6, 5, 4.3, 5.2 };
        var s21 = new double[] { 5, 6.3, 4.6, 63, 5, 4.3, 5.2 };

        WriteLine(!NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s19), NoperHashDouble.CalcDouble(s20), Eps));
        WriteLine(NoperHashDouble.DoubleEquals(NoperHashDouble.CalcDouble(s19), NoperHashDouble.CalcDouble(s21), Eps));
    }
}

#endif