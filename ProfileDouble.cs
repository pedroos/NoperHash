namespace PedroOs.NoperHash;

using System.Diagnostics;

#if DEBUG

public static class NoperHashProfileDouble {
    /// <summary>Profiles the calculation of an input list</summary>
    public static ProfileResult Profile(double[] vec)
    {
        var sw = new Stopwatch();

        sw.Start();
        double mean = NoperHashDouble.MeanDouble(vec);
        sw.Stop();
        long timeMean = sw.ElapsedTicks;
        sw.Reset();

        if (mean.Approximately(0, NoperHashDouble.Eps)) return default;

        double initValue = mean;
        double currValue = 0.0;

        double meanExp = NoperHashDouble.Exp10(mean);
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
            double xExp = NoperHashDouble.Exp10(x); // Loop3
            double xMag = !x.Approximately(0.0, NoperHashDouble.Eps) ?
                x * Math.Pow(10, xExp * -1) :
                meanMag;
            sw.Stop();
            timeLoop3 += sw.ElapsedTicks;
            sw.Reset();

            sw.Start();
            double yExp = NoperHashDouble.Exp10(y); // Loop4
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

        return new(timeMean, timeLoop2, timeLoop2b, timeLoop3, timeLoop4, timeLoop5);
    }

    public readonly record struct ProfileResult(
        long Mean, 
        long Loop2, 
        long Loop2b, 
        long Loop3, 
        long Loop4, 
        long Loop5
    );
}

#endif