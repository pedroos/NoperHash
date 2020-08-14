using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Psobo.NoperHash.Tests
{
    using Psobo.NoperHash;
    using xxHashSharp;
    using HashDepot;

    static class Extensions
    {
        internal static byte[] Bytes(this string str) => System.Text.Encoding.UTF8.GetBytes(str);
        internal static string String(this byte[] bytes) => System.Text.Encoding.UTF8.GetString(bytes);
    }

    [TestClass]
    public class NoperHashTests
    {
        double testEps;
        string wordsPath;

        [TestInitialize]
        [DeploymentItem("words.txt")]
        public void Init()
        {
            testEps = Math.Pow(10, -6);

            wordsPath = "words.txt";
        }

        [TestMethod]
        public void IncreaseValueWithoutProportionatelyDecreasingAnother()
        {
            var s1 = new List<double>() { 5,6,4.6,6.3,5,4.3,5.2 };
            var s2 = new List<double>() { 5,5.2,4.6,6.3,5,4.3,5.2 };

            Assert.IsFalse(NoperHash.ListsEqual(s1, s2));
        }

        [TestMethod]
        public void NonSymmetricSwapOfValues() 
        {
            var s1 = new List<double>() { 5,6,4.6,6.3,5,4.3,5.2 };
            var s2 = new List<double>() { 5,5.2,5.4,6.3,5,4.3,5.2 };
            var s3 = new List<double>() { 5,6,6.3,4.6,5,4.3,5.2 };

            Assert.IsFalse(NoperHash.ListsEqual(s1, s2));
            Assert.IsFalse(NoperHash.ListsEqual(s1, s3));
        }

        [TestMethod]
        public void NonMultipleOfTenSwapOfValues() 
        {
            var s1 = new List<double>() { 5,63,4.6,6.3,5,4.3,5.2 };
            var s2 = new List<double>() { 5,63,6.3,4.6,5,4.3,5.2 };
            var s3 = new List<double>() { 5,6.3,4.6,63,5,4.3,5.2 };

            Assert.IsFalse(NoperHash.ListsEqual(s1, s2));
            Assert.IsTrue(NoperHash.ListsEqual(s1, s3));
        }

        [TestMethod]
        public void AssymetricModificationsIncludingOrdersOf10() 
        {
            var s1 = new List<double>() { 5,63,4.6,6.2,5,4.3,5.2 };
            var s2 = new List<double>() { 5,63,4.6,62,5,4.3,5.2 };
            var s3 = new List<double>() { 5,6.3,46,62,5,4.3,5.2 };
            var s4 = new List<double>() { 5,63,4.6,6.3,5,4.3,5.2 };
            var s5 = new List<double>() { 50,6.3,46,0.63,500,0.043,.52 };

            Assert.IsFalse(NoperHash.ListsEqual(s1, s2));
            Assert.IsFalse(NoperHash.ListsEqual(s2, s3));
            Assert.IsFalse(NoperHash.ListsEqual(s4, s5));
        }

        [TestMethod]
        public void ChangesInSign() {
            var s1 = new List<double>() { 5,6,4.6,6.3,5,4.3,5.2 };
            var s2 = new List<double>() { 5,-6,4.6,6.3,5,4.3,5.2 };

            Assert.IsFalse(NoperHash.ListsEqual(s1, s2));
        }

        [TestMethod]
        public void SymmetricOrdersOf10SwapBetweenMultipleOf10Elements() 
        {
            var s1 = new List<double>() { 5,62,4.6,6.2,5,4.3,5.2 };
            var s2 = new List<double>() { 5,6.2,4.6,62,5,4.3,5.2 };

            Assert.IsTrue(NoperHash.ListsEqual(s1, s2));
        }

        [TestMethod]
        public void SampleLists() 
        {
            var s1 = new List<double>() { 1429041290,1429041350,1429041410,1429041470,1429041530,1429041590,1429041650,
                1429041710,1429041770,1429041830 };
            var s2 = new List<double>() { 24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25,28.57 };
            var s3 = new List<double>() { 4054,0,237,2009,4001,4019,6368,10670,6340,1816 };
            var s4 = new List<double>() { 226,0,21,156,205,240,446,519,400,127 };
            var s5 = new List<double>() { 145,0,5,38,114,90,166,312,222,48 };
            var s6 = new List<double>() { 0.000000101467,0.0,0.0833333,0.633333,1.9,1.5,2.76667,5.2,3.7,0.8 };
            var s7 = new List<double>() { 0.0, 0.0, 0.0  };
            
            Assert.IsTrue(NoperHash.DoubleEquals(NoperHash.Calc(s1), 0.379659, testEps));
            Assert.IsTrue(NoperHash.DoubleEquals(NoperHash.Calc(s2), 0.491966, testEps));
            Assert.IsTrue(NoperHash.DoubleEquals(NoperHash.Calc(s3), 0.600186, testEps));
            Assert.IsTrue(NoperHash.DoubleEquals(NoperHash.Calc(s4), 0.506223, testEps));
            Assert.IsTrue(NoperHash.DoubleEquals(NoperHash.Calc(s5), 0.549888, testEps));
            Assert.IsTrue(NoperHash.DoubleEquals(NoperHash.Calc(s6), 0.614593, testEps));
            Assert.IsTrue(NoperHash.DoubleEquals(NoperHash.Calc(s7), 0.0, testEps));
        }

        [TestMethod]
        public void BeginWithZero() 
        {
            var s1 = new List<double>() { 0,1429041290,1429041350,1429041410,1429041470,1429041530 };
            var s2 = new List<double>() { 1429041290,1429041350,1429041410,1429041470,1429041530 }; ;
            var s3 = new List<double>() { 0,0,0,24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25,28.57 };
            var s4 = new List<double>() { 24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25,28.57 };

            Assert.IsFalse(NoperHash.DoubleEquals(NoperHash.Calc(s1), 0.0, NoperHash.Eps));
            Assert.IsFalse(NoperHash.DoubleEquals(NoperHash.Calc(s3), 0.0, NoperHash.Eps));
            Assert.IsFalse(NoperHash.ListsEqual(s1, s2));
            Assert.IsFalse(NoperHash.ListsEqual(s3, s4));
        }

        [TestMethod]
        public void SimilarLists()
        {
            var s1 = new List<double>() { 145, 0, 5, 38, 114, 90, 166, 312, 222, 48 };
            var s2 = new List<double>() { 145, 0, 5, 38, 114, 90, 166, 312, 222, 48, 1 };
            var s3 = new List<double>() { 145, 0, 5, 38, 114, 90, 166, 312, 222, 48, 0, 0 };

            Assert.IsFalse(NoperHash.ListsEqual(s1, s2));
            Assert.IsFalse(NoperHash.ListsEqual(s2, s3));
        }

        // https://cp-algorithms.com/string/string-hashing.html
        static double PRHCalc(byte[] vec)
        {
            const int p = 31;
            const double m = 1e9 + 9;
            double hash_value = 0;
            double p_pow = 1;
            foreach (byte c in vec)
            {
                hash_value = (hash_value + (c - 'a' + 1) * p_pow) % m;
                p_pow = (p_pow * p) % m;
            }
            return hash_value;
        }

        [TestMethod]
        public void String1Noper()
        {
            Assert.IsTrue(NoperHash.DoubleEquals(NoperHash.Calc("abcdefghijklmnopqrstuvwzyz".Bytes()),
                NoperHash.Calc("abcdefghijklmnopqrstuvwzyz".Bytes()), NoperHash.Eps));
            Assert.IsFalse(NoperHash.DoubleEquals(NoperHash.Calc("abcdefghijklmnopqrstuvwzyz".Bytes()),
                NoperHash.Calc("abcdefghijklmnopqrstuvwzy".Bytes()), NoperHash.Eps));
            Assert.IsFalse(NoperHash.DoubleEquals(NoperHash.Calc("abcdefghijklmnopqrstuvwzyz".Bytes()),
                NoperHash.Calc("abcdefghijklmnopqrstuvwzyza".Bytes()), NoperHash.Eps));
            Assert.IsFalse(NoperHash.DoubleEquals(NoperHash.Calc("abcdefghijklmnopqrstuvwzyz".Bytes()),
                NoperHash.Calc("abcdefghijklmnopqrstuvwzyz ".Bytes()), NoperHash.Eps));
            Assert.IsFalse(NoperHash.DoubleEquals(NoperHash.Calc("abcdefghijklmnopqrstuvwzyz".Bytes()),
                NoperHash.Calc(" abcdefghijklmnopqrstuvwzyz".Bytes()), NoperHash.Eps));
            Assert.IsFalse(NoperHash.DoubleEquals(NoperHash.Calc(" abcdefghijklmnopqrstuvwzyz".Bytes()),
                NoperHash.Calc("  abcdefghijklmnopqrstuvwzyz".Bytes()), NoperHash.Eps));
            Assert.IsFalse(NoperHash.DoubleEquals(NoperHash.Calc("abcdefghijklmnopqrstuvwzyz ".Bytes()),
                NoperHash.Calc("abcdefghijklmnopqrstuvwzyz  ".Bytes()), NoperHash.Eps));
        }

        [TestMethod]
        public void String1XX()
        {
            Assert.IsTrue(xxHash.CalculateHash("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                xxHash.CalculateHash("abcdefghijklmnopqrstuvwzyz".Bytes()));
            Assert.IsFalse(xxHash.CalculateHash("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                xxHash.CalculateHash("abcdefghijklmnopqrstuvwzy".Bytes()));
            Assert.IsFalse(xxHash.CalculateHash("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                xxHash.CalculateHash("abcdefghijklmnopqrstuvwzyza".Bytes()));
            Assert.IsFalse(xxHash.CalculateHash("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                xxHash.CalculateHash("abcdefghijklmnopqrstuvwzyz ".Bytes()));
            Assert.IsFalse(xxHash.CalculateHash("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                xxHash.CalculateHash(" abcdefghijklmnopqrstuvwzyz".Bytes()));
            Assert.IsFalse(xxHash.CalculateHash(" abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                xxHash.CalculateHash("  abcdefghijklmnopqrstuvwzyz".Bytes()));
            Assert.IsFalse(xxHash.CalculateHash("abcdefghijklmnopqrstuvwzyz ".Bytes()) ==
                xxHash.CalculateHash("abcdefghijklmnopqrstuvwzyz  ".Bytes()));
        }

        [TestMethod]
        public void String1Fnv1()
        {
            Assert.IsTrue(Fnv1a.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                Fnv1a.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes()));
            Assert.IsFalse(Fnv1a.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                Fnv1a.Hash32("abcdefghijklmnopqrstuvwzy".Bytes()));
            Assert.IsFalse(Fnv1a.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                Fnv1a.Hash32("abcdefghijklmnopqrstuvwzyza".Bytes()));
            Assert.IsFalse(Fnv1a.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                Fnv1a.Hash32("abcdefghijklmnopqrstuvwzyz ".Bytes()));
            Assert.IsFalse(Fnv1a.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                Fnv1a.Hash32(" abcdefghijklmnopqrstuvwzyz".Bytes()));
            Assert.IsFalse(Fnv1a.Hash32(" abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                Fnv1a.Hash32("  abcdefghijklmnopqrstuvwzyz".Bytes()));
            Assert.IsFalse(Fnv1a.Hash32("abcdefghijklmnopqrstuvwzyz ".Bytes()) ==
                Fnv1a.Hash32("abcdefghijklmnopqrstuvwzyz  ".Bytes()));
        }

        [TestMethod]
        public void String1Murmur()
        {
            Assert.IsTrue(MurmurHash3.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes(), 0) ==
                MurmurHash3.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes(), 0));
            Assert.IsFalse(MurmurHash3.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes(), 0) ==
                MurmurHash3.Hash32("abcdefghijklmnopqrstuvwzy".Bytes(), 0));
            Assert.IsFalse(MurmurHash3.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes(), 0) ==
                MurmurHash3.Hash32("abcdefghijklmnopqrstuvwzyza".Bytes(), 0));
            Assert.IsFalse(MurmurHash3.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes(), 0) ==
                MurmurHash3.Hash32("abcdefghijklmnopqrstuvwzyz ".Bytes(), 0));
            Assert.IsFalse(MurmurHash3.Hash32("abcdefghijklmnopqrstuvwzyz".Bytes(), 0) ==
                MurmurHash3.Hash32(" abcdefghijklmnopqrstuvwzyz".Bytes(), 0));
            Assert.IsFalse(MurmurHash3.Hash32(" abcdefghijklmnopqrstuvwzyz".Bytes(), 0) ==
                MurmurHash3.Hash32("  abcdefghijklmnopqrstuvwzyz".Bytes(), 0));
            Assert.IsFalse(MurmurHash3.Hash32("abcdefghijklmnopqrstuvwzyz ".Bytes(), 0) ==
                MurmurHash3.Hash32("abcdefghijklmnopqrstuvwzyz  ".Bytes(), 0));
        }

        [TestMethod]
        public void String1PRH()
        {
            Assert.IsTrue(PRHCalc("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                PRHCalc("abcdefghijklmnopqrstuvwzyz".Bytes()));
            Assert.IsFalse(PRHCalc("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                PRHCalc("abcdefghijklmnopqrstuvwzy".Bytes()));
            Assert.IsFalse(PRHCalc("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                PRHCalc("abcdefghijklmnopqrstuvwzyza".Bytes()));
            Assert.IsFalse(PRHCalc("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                PRHCalc("abcdefghijklmnopqrstuvwzyz ".Bytes()));
            Assert.IsFalse(PRHCalc("abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                PRHCalc(" abcdefghijklmnopqrstuvwzyz".Bytes()));
            Assert.IsFalse(PRHCalc(" abcdefghijklmnopqrstuvwzyz".Bytes()) ==
                PRHCalc("  abcdefghijklmnopqrstuvwzyz".Bytes()));
            Assert.IsFalse(PRHCalc("abcdefghijklmnopqrstuvwzyz ".Bytes()) ==
                PRHCalc("abcdefghijklmnopqrstuvwzyz  ".Bytes()));
        }

        [TestMethod]
        public void StringWordsNoper()
        {
            var lines = System.IO.File.ReadAllLines(wordsPath);
            var lineBytes = new byte[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
                lineBytes[i] = lines[i].Bytes();

            var sw = new Stopwatch();
            sw.Start();

            for (int h = 1; h <= 1; ++h) 
                for (int i = 0; i < lineBytes.Length; ++i)
                    NoperHash.Calc(lineBytes[i]);

            sw.Stop();
            Console.WriteLine($"\nElapsed ms: {sw.ElapsedMilliseconds}");
        }

        [TestMethod]
        public void StringWordsXX()
        {
            var lines = System.IO.File.ReadAllLines(wordsPath);
            var lineBytes = new byte[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
                lineBytes[i] = lines[i].Bytes();

            var sw = new Stopwatch();
            sw.Start();

            for (int h = 1; h <= 1; ++h)
                for (int i = 0; i < lineBytes.Length; ++i)
                    xxHash.CalculateHash(lineBytes[i], seed: (uint)h);

            sw.Stop();
            Console.WriteLine($"\nElapsed ms: {sw.ElapsedMilliseconds}");
        }

        [TestMethod]
        public void StringWordsFnv1()
        {
            var lines = System.IO.File.ReadAllLines(wordsPath);
            var lineBytes = new byte[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
                lineBytes[i] = lines[i].Bytes();

            var sw = new Stopwatch();
            sw.Start();

            for (int h = 1; h <= 1; ++h)
                for (int i = 0; i < lineBytes.Length; ++i)
                    Fnv1a.Hash32(lineBytes[i]);

            sw.Stop();
            Console.WriteLine($"\nElapsed ms: {sw.ElapsedMilliseconds}");
        }

        [TestMethod]
        public void StringWordsMurmur()
        {
            var lines = System.IO.File.ReadAllLines(wordsPath);
            var lineBytes = new byte[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
                lineBytes[i] = lines[i].Bytes();

            var sw = new Stopwatch();
            sw.Start();

            for (int h = 1; h <= 1; ++h)
                for (int i = 0; i < lineBytes.Length; ++i)
                    MurmurHash3.Hash32(lineBytes[i], seed: (uint)h);

            sw.Stop();
            Console.WriteLine($"\nElapsed ms: {sw.ElapsedMilliseconds}");
        }

        [TestMethod]
        public void StringWordsPRH()
        {
            var lines = System.IO.File.ReadAllLines(wordsPath);
            var lineBytes = new byte[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
                lineBytes[i] = lines[i].Bytes();

            var sw = new Stopwatch();
            sw.Start();

            for (int h = 1; h <= 1; ++h)
                for (int i = 0; i < lineBytes.Length; ++i) 
                    PRHCalc(lineBytes[i]);

            sw.Stop();
            Console.WriteLine($"\nElapsed ms: {sw.ElapsedMilliseconds}");
        }

        [TestMethod]
        public void StringCollisionsXX() => StringCollisions(new Func<byte[], uint>(lineBytes => 
            xxHash.CalculateHash(lineBytes)));

        [TestMethod]
        public void StringCollisionsFnv1() => StringCollisions(Fnv1a.Hash32);

        [TestMethod]
        public void StringCollisionsMurmur() => StringCollisions(new Func<byte[], uint>(lineBytes =>
            MurmurHash3.Hash32(lineBytes, 0)));

        [TestMethod]
        public void StringCollisionsNoper() => StringCollisions(NoperHash.Calc);

        [TestMethod]
        public void StringCollisionsPRH() => StringCollisions(PRHCalc);

        void StringCollisions<T>(Func<byte[], T> hashFunc)
        {
            var lines = System.IO.File.ReadAllLines(wordsPath);
            var lineBytes = new byte[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
                lineBytes[i] = lines[i].Bytes();

            var ress = new SortedSet<T>();
            var ressStrs = new Dictionary<T, List<string>>();
            int collCount = 0;

            for (int i = 0; i < lineBytes.Length; ++i)
            {
                T res = hashFunc(lineBytes[i]);
                if (ress.Contains(res))
                {
                    string collStrs = string.Join(", ", ressStrs[res].Select(rs => $"'{rs}'"));
                    Console.WriteLine($"Collision '{lineBytes[i].String()}' with '{collStrs}' result {res}");
                    collCount++;
                }
                else
                {
                    ress.Add(res);
                }
                if (!ressStrs.ContainsKey(res)) ressStrs.Add(res, new List<string>());
                ressStrs[res].Add(lineBytes[i].String());
            }
            Console.WriteLine($"Total collisions: {collCount}");
        }
    }
}
