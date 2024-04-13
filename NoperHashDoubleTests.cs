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

        T("2a9", NoperHashDouble.GetDouble(s01).Approximately(0.37965870745378555, Eps));
        T("a1a", NoperHashDouble.GetDouble(s02).Approximately(0.49196617766541745, Eps));
        T("ea9", NoperHashDouble.GetDouble(s03).Approximately(0.6001859386298001, Eps));
        T("ab4", NoperHashDouble.GetDouble(s04).Approximately(0.5062227481033681, Eps));
        T("e0b", NoperHashDouble.GetDouble(s05).Approximately(0.5498883933485951, Eps));
        T("241", NoperHashDouble.GetDouble(s06).Approximately(0.6145925169648433, Eps));
        T("7f6", NoperHashDouble.GetDouble(s07).Approximately(0.0, Eps));

        // Leading zeroes tests

        var s08 = new double[] { 0,1429041290,1429041350,1429041410,1429041470,1429041530 };
        var s09 = new double[] { 1429041290,1429041350,1429041410,1429041470,1429041530 };
        var s10 = new double[] { 0,0,0,24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25 };
        var s11 = new double[] { 24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25 };

        T("510", !NoperHashDouble.GetDouble(s08).Approximately(0, Eps));
        T("c36", !NoperHashDouble.GetDouble(s09).Approximately(0, Eps));
        T("e1d", !NoperHashDouble.GetDouble(s08).Approximately(NoperHashDouble.GetDouble(s09), Eps));
        T("f2d", !NoperHashDouble.GetDouble(s10).Approximately(NoperHashDouble.GetDouble(s11), Eps));

        // Symmetric modifications tests

        var s12 = new double[] { 5, 62, 4.6, 6.2, 5, 4.3, 5.2 };
        var s13 = new double[] { 5, 6.2, 4.6, 62, 5, 4.3, 5.2 };

        T("413", NoperHashDouble.GetDouble(s12).Approximately(NoperHashDouble.GetDouble(s13), Eps));

        // Asymmetric modifications tests

        var s14 = new double[] { 5, 63, 4.6, 6.2, 5, 4.3, 5.2 };
        var s15 = new double[] { 5, 63, 4.6, 62, 5, 4.3, 5.2 };
        var s16 = new double[] { 5, 6.3, 46, 62, 5, 4.3, 5.2 };
        var s17 = new double[] { 5, 63, 4.6, 6.3, 5, 4.3, 5.2 };
        var s18 = new double[] { 50, 6.3, 46, 0.63, 500, 0.043, .52 };

        T("337", !NoperHashDouble.GetDouble(s14).Approximately(NoperHashDouble.GetDouble(s15), Eps));
        T("6a0", !NoperHashDouble.GetDouble(s14).Approximately(NoperHashDouble.GetDouble(s16), Eps));
        T("b0f", !NoperHashDouble.GetDouble(s14).Approximately(NoperHashDouble.GetDouble(s17), Eps));
        T("b7d", !NoperHashDouble.GetDouble(s14).Approximately(NoperHashDouble.GetDouble(s18), Eps));

        // Subtractive modifications tests

        var s19 = new double[] { 5, 63, 4.6, 6.3, 5, 4.3, 5.2 };
        var s20 = new double[] { 5, 63, 6.3, 4.6, 5, 4.3, 5.2 };
        var s21 = new double[] { 5, 6.3, 4.6, 63, 5, 4.3, 5.2 };

        T("dd0", !NoperHashDouble.GetDouble(s19).Approximately(NoperHashDouble.GetDouble(s20), Eps));
        T("642", NoperHashDouble.GetDouble(s19).Approximately(NoperHashDouble.GetDouble(s21), Eps));
    }
}

#endif