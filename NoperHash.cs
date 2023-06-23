namespace PedroOs.NoperHash;

using System.Numerics;
using System.Diagnostics;

public static class NoperHash {
    public static bool Approximately<T>(T a, T b, T eps) where T : INumber<T> => T.Abs(a - b) < eps;
    
    internal static T Exp10<T>(T n) where T : INumber<T>, ILogarithmicFunctions<T>, IFloatingPoint<T> => 
        n != T.Zero ? T.One + T.Floor(T.Log10(T.Abs(n))) : T.Zero;

    internal static T Mean<T>(T[] vec) where T : IAdditionOperators<T, T, T>, IDivisionOperators<T, T, T>, INumberBase<T> {
        T sum = T.Zero;
        for (int i = 0; i < vec.Length; ++i)
            sum += vec[i];
        return sum / T.CreateChecked(vec.Length);
    }

    internal static T MovingMean<T>(T[] vec) where T : IAdditionOperators<T, T, T>, IDivisionOperators<T, T, T>, INumberBase<T> {
        T sum = T.Zero;
        T two = Two<T>();
        for (int i = 0; i < vec.Length; ++i) 
            sum += (vec[i] - sum) / two;
        return sum;
    }
    
    internal static bool ListsEqual<T>(T[] vec1, T[] vec2, T? epsilon = default) where T : 
        INumber<T>, IDivisionOperators<T, T, T>, IPowerFunctions<T>, ILogarithmicFunctions<T>, IFloatingPoint<T> =>
        Approximately(Calc(vec1), Calc(vec2), epsilon ?? DefaultEpsilon<T>());

    internal static T DefaultEpsilon<T>() where T : INumber<T>, IPowerFunctions<T> => 
        T.Pow(Ten<T>(), T.CreateChecked(-9));

    static T Two<T>() where T : INumberBase<T> => T.CreateChecked(2);
    
    static T Ten<T>() where T : INumberBase<T> => T.CreateChecked(10);
    
    public static double Calc(double[] vec) => Calc<double>(vec);

    static T Calc<T>(T[] vec) where T : 
        INumber<T>, IDivisionOperators<T, T, T>, IPowerFunctions<T>, ILogarithmicFunctions<T>, IFloatingPoint<T> 
    {
        T mean = Mean(vec);

        T ten = Ten<T>();

        T epsilon = DefaultEpsilon<T>();
        
        if (Approximately<T>(mean, T.Zero, epsilon)) return T.Zero;
        
        T initValue = mean;
        T currValue = T.Zero;

        T meanExp = Exp10(mean);
        T meanMag = mean * T.Pow(ten, meanExp * T.NegativeOne);

        for (int i = vec.Length - 1; i >= 0; --i) {
            T x = T.Abs(vec[i]);
            
            T y = i == vec.Length - 1 ? 
                T.Abs(initValue) : 
                currValue;

            T xExp = Exp10(x);
            T xMag = !Approximately(x, T.Zero, epsilon) ?
                x * T.Pow(ten, xExp * T.NegativeOne) :
                meanMag;

            T yExp = Exp10(y);
            T yMag = y * T.Pow(ten, yExp * T.NegativeOne);

            currValue = T.Pow(xMag, yMag);
        }

        return currValue;
    }

#if DEBUG

    public static ProfileResult CalcProfile<T>(T[] vec, Stopwatch sw) 
        where T : INumber<T>, IDivisionOperators<T, T, T>, IPowerFunctions<T>, ILogarithmicFunctions<T>, IFloatingPoint<T> 
    {
        sw.Start();
        T mean = Mean(vec);
        sw.Stop();
        long timeMean = sw.ElapsedTicks;
        sw.Reset();

        T epsilon = DefaultEpsilon<T>();
        
        if (Approximately(mean, T.Zero, epsilon)) return default;

        T initValue = mean;
        T currValue = T.Zero;

        T ten = Ten<T>();
        
        T meanExp = Exp10(mean);
        T meanMag = mean * T.Pow(ten, meanExp * T.NegativeOne);

        long timeLoop2 = 0;
        long timeLoop2b = 0;
        long timeLoop3 = 0;
        long timeLoop4 = 0;
        long timeLoop5 = 0;

        for (int i = vec.Length - 1; i >= 0; --i) {
            sw.Start();
            // Loop2
            T x = T.Abs(vec[i]);
            sw.Stop();
            timeLoop2 += sw.ElapsedTicks;
            sw.Reset();
            T y;
            if (i == vec.Length - 1) {
                y = T.Abs(initValue);
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
            T xExp = Exp10(x);
            T xMag = !Approximately(x, T.Zero, epsilon) ?
                x * T.Pow(ten, xExp * T.NegativeOne) :
                meanMag;
            sw.Stop();
            timeLoop3 += sw.ElapsedTicks;
            sw.Reset();

            sw.Start();
            // Loop4
            T yExp = Exp10(y);
            T yMag = y * T.Pow(ten, yExp * T.NegativeOne);
            sw.Stop();
            timeLoop4 += sw.ElapsedTicks;
            sw.Reset();

            sw.Start();
            // Loop5
            currValue = T.Pow(xMag, yMag);
            sw.Stop();
            timeLoop5 += sw.ElapsedTicks;
            sw.Reset();
        }

        return new(timeMean, timeLoop2, timeLoop2b, timeLoop3, timeLoop4, timeLoop5);
    }
    
    public readonly record struct ProfileResult(long Mean, long Loop2, long Loop2b, long Loop3, long Loop4, long Loop5);

#endif
}