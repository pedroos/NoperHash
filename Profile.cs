namespace PedroOs.NoperHash;

using System.Diagnostics;

#if DEBUG

public static class NoperHashProfile {
    /// <summary>Profiles the calculation of an input list</summary>
    public static ProfileResult Profile(double[] vec) {
        var sw = new Stopwatch();

        sw.Start();
        double mean = NoperHash.MovingMean<double, double>(vec);
        sw.Stop();
        long timeMean = sw.ElapsedTicks;
        sw.Reset();

        double epsilon = NoperHash.DefaultEpsilon<double>();
        
        if (mean.Approximately(0.0, epsilon)) return default;

        double initValue = mean;
        double currValue = 0.0;

        double ten = double.CreateChecked(10.0);
        
        double meanExp = NoperHash.Exp10(mean);
        double meanMag = mean * double.Pow(ten, meanExp * -1.0);

        long timeLoop2 = 0;
        long timeLoop2b = 0;
        long timeLoop3 = 0;
        long timeLoop4 = 0;
        long timeLoop5 = 0;

        for (int i = vec.Length - 1; i >= 0; --i) {
            sw.Start();
            // Loop2
            double x = double.Abs(vec[i]);
            sw.Stop();
            timeLoop2 += sw.ElapsedTicks;
            sw.Reset();
            double y;
            if (i == vec.Length - 1) {
                y = double.Abs(initValue);
            }
            else {
                sw.Start();
                // Loop2b
                y = currValue;
                sw.Stop();
                timeLoop2b += sw.ElapsedTicks;
                sw.Reset();
            }

            sw.Start();
            // Loop3
            double xExp = NoperHash.Exp10(x);
            double xMag = !x.Approximately(0.0, epsilon) ?
                x * double.Pow(ten, xExp * -1.0) :
                meanMag;
            sw.Stop();
            timeLoop3 += sw.ElapsedTicks;
            sw.Reset();

            sw.Start();
            // Loop4
            double yExp = NoperHash.Exp10(y);
            double yMag = y * double.Pow(ten, yExp * -1.0);
            sw.Stop();
            timeLoop4 += sw.ElapsedTicks;
            sw.Reset();

            sw.Start();
            // Loop5
            currValue = double.Pow(xMag, yMag);
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