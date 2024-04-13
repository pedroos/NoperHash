namespace PedroOs.NoperHash;

using System.Numerics;

public static partial class NoperHashExtensions {
    public static double Get(double[] vec) => 
        NoperHash.Get<double>(vec);
    
    public static double Get(ReadOnlySpan<char> span) => 
        NoperHash.Get<double, char>(span);

    public static double GetNoperHash(this string str) => 
        GetNoperHash(str.AsMemory());
    
    public static double GetNoperHash(this ReadOnlyMemory<char> str) =>
        NoperHash.Get<double, char>(str.Span);
    
    public static bool ListsEqual<T>(
        T[] vec1, 
        T[] vec2, 
        T? epsilon = default
    ) where T : 
        notnull, 
        INumberBase<T>, 
        IDivisionOperators<T, T, T>, 
        IPowerFunctions<T>, 
        ILogarithmicFunctions<T>, 
        IFloatingPoint<T> 
    =>
        NoperHash.Get<T>(vec1.AsSpan()).Approximately(
            NoperHash.Get<T>(vec2.AsSpan()), 
            epsilon ?? NoperHash.DefaultEpsilon<T>()
        );
}