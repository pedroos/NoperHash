namespace PedroOs.NoperHash;

using System.Numerics;

public static class NoperHash {
    public static bool Approximately<T>(this T a, T b) where T : 
        notnull, 
        IComparisonOperators<T, T, bool>,
        INumberBase<T>,
        IPowerFunctions<T> => 
        Approximately(a, b, NoperHash.DefaultEpsilon<T>());

    public static bool Approximately<T>(this T a, T b, T eps) where T : 
        notnull, 
        IComparisonOperators<T, T, bool>,
        INumberBase<T> => 
        T.Abs(a - b) < eps;

    internal static T Exp10<T>(T n) where T : 
        notnull, 
        INumberBase<T>, 
        ILogarithmicFunctions<T>, 
        IFloatingPoint<T> => 
        n != T.Zero ? T.One + T.Floor(T.Log10(T.Abs(n))) : T.Zero;

    internal static T MovingMean<T, TO>(ReadOnlySpan<TO> vec) 
        where T : 
            notnull, 
            IAdditionOperators<T, T, T>, 
            IDivisionOperators<T, T, T>, 
            INumberBase<T>
        where TO :
            notnull,
            INumberBase<TO> 
    {
        T sum = T.Zero;
        T two = Two<T>();
        for (int i = 0; i < vec.Length; ++i) 
            sum += (T.CreateChecked(vec[i]) - sum) / two;
        return sum;
    }

    internal static T DefaultEpsilon<T>() where T : 
        notnull, 
        IComparisonOperators<T, T, bool>,
        INumberBase<T>, 
        IPowerFunctions<T> => 
        T.Pow(Ten<T>(), T.CreateChecked(-9));

    static T Two<T>() where T : notnull, INumberBase<T> => T.CreateChecked(2);
    
    static T Ten<T>() where T : notnull, INumberBase<T> => T.CreateChecked(10);

    internal static T Get<T, TO>(ReadOnlySpan<TO> vec, int? vecLength = null) 
        where T : 
            notnull,
            INumberBase<T>, 
            IDivisionOperators<T, T, T>, 
            IPowerFunctions<T>, 
            ILogarithmicFunctions<T>, 
            IFloatingPoint<T>
        where TO :
            notnull,
            INumberBase<TO>
    {
        T mean = MovingMean<T, TO>(vec);

        T ten = Ten<T>();

        T epsilon = DefaultEpsilon<T>();
        
        if (mean.Approximately(T.Zero, epsilon)) return T.Zero;
        
        T initValue = mean;
        T currValue = T.Zero;

        T meanExp = Exp10(mean);
        T meanMag = mean * T.Pow(ten, meanExp * T.NegativeOne);

        int startingPos = vecLength ?? vec.Length - 1;

        for (int i = startingPos; i >= 0; --i) {
            T x = T.Abs(T.CreateChecked(vec[i]));
            
            T y = i == vec.Length - 1 ? 
                T.Abs(initValue) : 
                currValue;

            T xExp = Exp10(x);
            T xMag = !x.Approximately(T.Zero, epsilon) ?
                x * T.Pow(ten, xExp * T.NegativeOne) :
                meanMag;

            T yExp = Exp10(y);
            T yMag = y * T.Pow(ten, yExp * T.NegativeOne);

            currValue = T.Pow(xMag, yMag);
        }

        return currValue;
    }
    
    internal static T Get<T>(ReadOnlySpan<T> vec, int? vecLength = null) where T : 
        notnull,
        INumberBase<T>, 
        IDivisionOperators<T, T, T>, 
        IPowerFunctions<T>, 
        ILogarithmicFunctions<T>, 
        IFloatingPoint<T> 
        => Get<T, T>(vec, vecLength);
    
    internal static double Get(string str, int? vecLength = null) => 
        Get<double, char>(str.AsSpan(), vecLength);

    internal static double Get(double[] vec, int? vecLength = null) => 
        Get<double, double>(vec.AsSpan(), vecLength);

    internal static double Get(float[] vec, int? vecLength = null) => 
        Get<double, float>(vec.AsSpan(), vecLength);
}