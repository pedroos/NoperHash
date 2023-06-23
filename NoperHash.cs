namespace PedroOs.NoperHash;

using System.Numerics;

public static class NoperHash {
    public static bool Approximately<T>(this T a, T b) where T : 
        INumber<T>, IPowerFunctions<T> => T.Abs(a - b) < DefaultEpsilon<T>();
    
    public static bool Approximately<T>(this T a, T b, T eps) where T : INumber<T> => T.Abs(a - b) < eps;
    
    internal static T Exp10<T>(T n) where T : INumber<T>, ILogarithmicFunctions<T>, IFloatingPoint<T> => 
        n != T.Zero ? T.One + T.Floor(T.Log10(T.Abs(n))) : T.Zero;

    internal static T MovingMean<T>(T[] vec) where T : IAdditionOperators<T, T, T>, IDivisionOperators<T, T, T>, INumberBase<T> {
        T sum = T.Zero;
        T two = Two<T>();
        for (int i = 0; i < vec.Length; ++i) 
            sum += (vec[i] - sum) / two;
        return sum;
    }
    
    internal static bool ListsEqual<T>(T[] vec1, T[] vec2, T? epsilon = default) where T : 
        INumber<T>, IDivisionOperators<T, T, T>, IPowerFunctions<T>, ILogarithmicFunctions<T>, IFloatingPoint<T> =>
        Approximately(Get(vec1), Get(vec2), epsilon ?? DefaultEpsilon<T>());

    internal static T DefaultEpsilon<T>() where T : INumber<T>, IPowerFunctions<T> => 
        T.Pow(Ten<T>(), T.CreateChecked(-9));

    internal static T Two<T>() where T : INumberBase<T> => T.CreateChecked(2);
    
    internal static T Ten<T>() where T : INumberBase<T> => T.CreateChecked(10);
    
    public static double Get(double[] vec) => Get<double>(vec);

    static T Get<T>(T[] vec) where T : 
        INumber<T>, IDivisionOperators<T, T, T>, IPowerFunctions<T>, ILogarithmicFunctions<T>, IFloatingPoint<T> 
    {
        T mean = MovingMean(vec);

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
}

public static partial class NoperHashExtensions {
    public static double[] ToDoubles(this byte[] bytes) => bytes.Select(x => (double)x).ToArray();
}