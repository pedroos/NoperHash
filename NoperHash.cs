public static class NoperHash 
{
    public static double Eps = Math.Pow(10, -9);

    public static bool DoubleEquals(double a, double b, double eps) => Math.Abs(a - b) < eps;

    static double Exp10(double n) => n != 0 ? 1 + Math.Floor(Math.Log10(Math.Abs(n))) : 0;

    static double MeanDouble(double[] vec)
    {
        double sum = 0;
        for (int i = 0; i < vec.Count(); ++i)
            sum += vec[i];
        return (double)sum / vec.Count();
    }

    static double MeanByte(byte[] vec) {
        long sum = 0;
        for (int i = 0; i < vec.Count(); ++i)
            sum += vec[i];
        return (double)sum / vec.Count();
    }

    static bool ListsEqual(double[] vec1, double[] vec2) =>
        DoubleEquals(CalcDouble(vec1), CalcDouble(vec2), Eps);

    // For strings
    public static double CalcStr(byte[] vec) {
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

    public static double CalcDouble(double[] vec)
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

    public static (long Mean, long Loop2, long Loop2b, long Loop3, long Loop4, long Loop5) 
        CalcProfile(double[] vec, Stopwatch sw)
    {
        sw.Start();
        double mean = MeanDouble(vec);
        sw.Stop();
        long timeMean = sw.ElapsedTicks;
        sw.Reset();

        if (DoubleEquals(mean, 0, Eps)) return default;

        double initValue = mean;
        double currValue = 0.0;

        double meanExp = Exp10(mean);
        double meanMag = mean * Math.Pow(10, meanExp * -1);

        long timeLoop2 = 0;
        long timeLoop2b = 0;
        long timeLoop3 = 0;
        long timeLoop4 = 0;
        long timeLoop5 = 0;

        for (int i = vec.Count() - 1; i >= 0; --i)
        {
            sw.Start();
            double x = Math.Abs(vec[i]); // Loop2
            sw.Stop();
            timeLoop2 += sw.ElapsedTicks;
            sw.Reset();
            double y;
            if (i == vec.Count() - 1)
            {
                y = Math.Abs(initValue);
            }
            else
            {
                sw.Start();
                y = currValue; // Loop2b
                sw.Stop();
                timeLoop2b += sw.ElapsedTicks;
                sw.Reset();
            }

            sw.Start();
            double xExp = Exp10(x); // Loop3
            double xMag = !DoubleEquals(x, 0.0, Eps) ?
                x * Math.Pow(10, xExp * -1) :
                meanMag;
            sw.Stop();
            timeLoop3 += sw.ElapsedTicks;
            sw.Reset();

            sw.Start();
            double yExp = Exp10(y); // Loop4
            double yMag = y * Math.Pow(10, yExp * -1);
            sw.Stop();
            timeLoop4 += sw.ElapsedTicks;
            sw.Reset();

            sw.Start();
            currValue = Math.Pow(xMag, yMag); // Loop5
            sw.Stop();
            timeLoop5 += sw.ElapsedTicks;
            sw.Reset();
        }

        return (timeMean, timeLoop2, timeLoop2b, timeLoop3, timeLoop4, timeLoop5);
    }
}