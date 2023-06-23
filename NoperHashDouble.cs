namespace PedroOs.NoperHash;

public static class NoperHashDouble {
    public static double Eps = Math.Pow(10, -9);

    public static bool DoubleEquals(double a, double b, double eps) => Math.Abs(a - b) < eps;

    internal static double Exp10(double n) => n != 0 ? 1 + Math.Floor(Math.Log10(Math.Abs(n))) : 0;

    internal static double MeanDouble(double[] vec) {
        double sum = 0;
        for (int i = 0; i < vec.Count(); ++i)
            sum += vec[i];
        return (double)sum / vec.Count();
    }

    internal static double MeanByte(byte[] vec) {
        long sum = 0;
        for (int i = 0; i < vec.Count(); ++i)
            sum += vec[i];
        return (double)sum / vec.Count();
    }

    static bool ListsEqual(double[] vec1, double[] vec2) =>
        DoubleEquals(GetDouble(vec1), GetDouble(vec2), Eps);

    // For strings
    public static double GetStr(byte[] vec) {
        double mean = MeanByte(vec);
        if (DoubleEquals(mean, 0, Eps)) return default;
        double initValue = mean;
        double currValue = 0.0;

        double meanExp = Exp10(mean);
        double meanMag = mean * Math.Pow(10, meanExp * -1);

        for (int i = vec.Length - 1; i >= 0; --i) {
            int x = vec[i] > 0 ? vec[i] : vec[i] * -1;

            double y;
            if (i == vec.Count() - 1)
                y = Math.Abs(initValue);
            else
                y = currValue;

            double xExp = Exp10(x);
            double xMag = x != 0 ?
                x * Math.Pow(10, xExp * -1) :
                meanMag;

            double yExp = Exp10(y);
            double yMag = y * Math.Pow(10, yExp * -1);

            currValue = Math.Pow(xMag, yMag);
        }

        return currValue;
    }

    public static double GetDouble(double[] vec)
    {
        double mean = MeanDouble(vec);
        if (DoubleEquals(mean, 0, Eps)) return 0.0;
        double initValue = mean;
        double currValue = 0.0;

        double meanExp = Exp10(mean);
        double meanMag = mean * Math.Pow(10, meanExp * -1);

        for (int i = vec.Count() - 1; i >= 0; --i)
        {
            double x = Math.Abs(vec[i]);
            double y;
            if (i == vec.Count() - 1) 
                y = Math.Abs(initValue);
            else 
                y = currValue;

            double xExp = Exp10(x);
            double xMag = !DoubleEquals(x, 0.0, Eps) ?
                x * Math.Pow(10, xExp * -1) :
                meanMag;

            double yExp = Exp10(y);
            double yMag = y * Math.Pow(10, yExp * -1);

            currValue = Math.Pow(xMag, yMag);
        }

        return currValue;
    }
}