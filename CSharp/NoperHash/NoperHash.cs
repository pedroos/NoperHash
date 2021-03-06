﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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

        static double Mean(double[] vec)
        {
            double sum = 0;
            for (int i = 0; i < vec.Count(); ++i)
                sum += vec[i];
            return (double)sum / vec.Count();
        }

        static double Mean(byte[] vec) {
            long sum = 0;
            for (int i = 0; i < vec.Count(); ++i)
                sum += vec[i];
            return (double)sum / vec.Count();
        }

        public static bool ListsEqual(double[] vec1, double[] vec2) => 
            DoubleEquals(Calc(vec1), Calc(vec2), Eps);

        /// <summary>Computes a hash</summary>
        /// <param name="vec">List of float values</param>
        /// <returns>The computed hash</returns>
        /// <remarks>Overload for use with float lists</remarks>
        public static double Calc(double[] vec)
        {
            double mean = Mean(vec);
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
            double mean = Mean(vec); // Mean
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

        /// <summary>Computes a hash</summary>
        /// <param name="vec">Byte array corresponding to a string</param>
        /// <returns>The computed hash</returns>
        /// <remarks>Overload for use with strings</remarks>
        public static double Calc(byte[] vec)
        {
            double mean = Mean(vec);
            if (DoubleEquals(mean, 0, Eps)) return default;
            double initValue = mean;
            double currValue = 0.0;

            double meanExp = Exp10(mean);
            double meanMag = mean * Math.Pow(10, meanExp * -1);

            for (int i = vec.Length - 1; i >= 0; --i)
            {
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
    }
}