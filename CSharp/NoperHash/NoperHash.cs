using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Psobo.NoperHash
{
    /// <summary>
    /// Computes flat list or byte array hashes
    /// </summary>
    public static class NoperHash
    {
        /// <summary>
        /// Epsilon value adjusted for NoperHash
        /// </summary>
        public static double Eps { get; private set; } = Math.Pow(10, -9);

        /// <summary>
        /// Compares two float values with respect to an epsilon difference
        /// </summary>
        /// <returns>True if the floats are within epsilon range; false otherwise</returns>
        public static bool DoubleEquals(double a, double b, double eps) => Math.Abs(a - b) < eps;

        static double Exp10(double n) => n != 0 ? 1 + Math.Floor(Math.Log10(Math.Abs(n))) : 0;

        static double Exp10(int n) => n != 0 ? 1 + Math.Floor(Math.Log10(Math.Abs(n))) : 0;

        static double Mean(IEnumerable<double> vec) => vec.Sum() / vec.Count();

        static double Mean(byte[] vec) {
            long sum = 0;
            for (int i = 0; i < vec.Count(); ++i)
                sum += vec.ElementAt(i);
            return (double)sum / vec.Count();
        }

        public static bool ListsEqual(IEnumerable<double> vec1, IEnumerable<double> vec2) => 
            DoubleEquals(Calc(vec1), Calc(vec2), Eps);

        /// <summary>Computes a hash</summary>
        /// <param name="vec">List of float values</param>
        /// <returns>The computed hash</returns>
        /// <remarks>Overload for use with float lists</remarks>
        public static double Calc(IEnumerable<double> vec) 
        {
            double mean = Mean(vec);
            double initValue = mean;
            double currValue = 0.0;

            double meanExp = Exp10(mean);
            double meanMag = mean * Math.Pow(10, meanExp * -1);

            if (vec.All(v => DoubleEquals(v, 0.0, Eps))) return 0.0;

            for (int i = vec.Count() - 1; i >= 0; --i)
            {
                double x = Math.Abs(vec.ElementAt(i));
                double y = i == vec.Count() - 1 ? Math.Abs(initValue) : currValue;

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

        /// <summary>Computes a hash</summary>
        /// <param name="vec">Byte array corresponding to a string</param>
        /// <returns>The computed hash</returns>
        /// <remarks>Overload for use with strings</remarks>
        public static double Calc(byte[] vec)
        {
            double mean = Mean(vec);
            double initValue = mean;
            double currValue = 0.0;

            double meanExp = Exp10(mean);
            double meanMag = mean * Math.Pow(10, meanExp * -1);

            bool anyNonZero = false;

            for (int i = vec.Length - 1; i >= 0; --i)
            {
                int x = vec[i] > 0 ? vec[i] : vec[i] * -1;
                if (x != 0)
                    anyNonZero = true;
                if (i == vec.Length - 1 && !anyNonZero) return 0.0;

                double y = i == vec.Count() - 1 ? Math.Abs(initValue) : currValue;

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
    }
}