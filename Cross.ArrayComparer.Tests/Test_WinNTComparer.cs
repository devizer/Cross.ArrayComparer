using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.ArrayComparer.Tests
{
    using System.Diagnostics;

    using CodeJam.Collections;

    using NUnit.Framework;

    [TestFixture]
    public class Test_WinNTComparer
    {

        [Test]
        public void Asserts_Bytes()
        {
            Assert.IsTrue(WinNtComparer.IsSupported, "WinNtComparer supports windows only and requires full trust");

            for (int i = 1; i < 100; i++)
            {
                int length = 32 * 1024;
                var arr = GetRandomBytes(i, length);
                var same = GetRandomBytes(i, length);
                Assert.IsTrue(WinNtComparer.Equals(arr, same));

                var another = GetRandomBytes(i + 1, length);
                Assert.IsFalse(WinNtComparer.Equals(arr, another));
            }
        }

        [Test]
        public void Perfomance_Large_Arrays_of_Bytes()
        {
            Assert.IsTrue(WinNtComparer.IsSupported, "WinNtComparer supports windows only and requires full trust");

            foreach (var length in new[] { 1, 64 * 1024 * 1024 })
            {
                var arr = GetRandomBytes(1, length);
                var same = GetRandomBytes(1, length);
                var another = GetRandomBytes(2, length);
                const int n = 10;

                {
                    Stopwatch sw = Stopwatch.StartNew();
                    for (int i = 0; i < n; i++)
                    {
                        Assert.IsTrue(WinNtComparer.Equals(arr, same));
                        Assert.IsFalse(WinNtComparer.Equals(arr, another));
                    }
                    var elapsed = sw.Elapsed;
                    if (length != 1) Trace.WriteLine("WinNtComparer:" + elapsed);
                }

                {
                    Stopwatch sw = Stopwatch.StartNew();
                    for (int i = 0; i < n; i++)
                    {
                        Assert.IsTrue(ArrayExtensions.EqualsTo(arr, same));
                        Assert.IsFalse(ArrayExtensions.EqualsTo(arr, another));
                    }
                    var elapsed = sw.Elapsed;
                    if (length != 1) Trace.WriteLine("ArrayExtensions:" + elapsed);
                }
            }
        }

        [Test]
        public void Perfomance_Small_Arrays_of_Bytes()
        {
            Assert.IsTrue(WinNtComparer.IsSupported, "WinNtComparer supports windows only and requires full trust");

            foreach (var length in new[] { 1, 16 })
            {
                var arr = GetRandomBytes(1, length);
                var same = GetRandomBytes(1, length);
                var another = GetRandomBytes(2, length);
                const int n = 100000;

                {
                    Stopwatch sw = Stopwatch.StartNew();
                    for (int i = 0; i < n; i++)
                    {
                        Assert.IsTrue(WinNtComparer.Equals(arr, same));
                        Assert.IsFalse(WinNtComparer.Equals(arr, another));
                    }
                    var elapsed = sw.Elapsed;
                    if (length != 1) Trace.WriteLine("WinNtComparer:" + elapsed);
                }

                {
                    Stopwatch sw = Stopwatch.StartNew();
                    for (int i = 0; i < n; i++)
                    {
                        Assert.IsTrue(ArrayExtensions.EqualsTo(arr, same));
                        Assert.IsFalse(ArrayExtensions.EqualsTo(arr, another));
                    }
                    var elapsed = sw.Elapsed;
                    if (length != 1) Trace.WriteLine("ArrayExtensions:" + elapsed);
                }
            }
        }

        [Test]
        public void Asserts_Decimals()
        {
            Assert.IsTrue(WinNtComparer.IsSupported, "WinNtComparer supports windows only and requires full trust");

            for (int i = 1; i < 100; i++)
            {
                int length = 32 * 1024;
                var arr = GetRandomDecimals(i, length);
                var same = GetRandomDecimals(i, length);
                Assert.IsTrue(WinNtComparer.Equals(arr, same));

                var another = GetRandomDecimals(i + 1, length);
                Assert.IsFalse(WinNtComparer.Equals(arr, another));
            }
        }

        byte[] GetRandomBytes(int seed, int length)
        {
            var rand = new Random(seed);
            byte[] ret = new byte[length];
            for (int i = 0; i < length; i++)
                ret[i] = (byte)rand.Next(255);

            return ret;
        }

        decimal[] GetRandomDecimals(int seed, int length)
        {
            var rand = new Random(seed);
            decimal[] ret = new decimal[length];
            for (int i = 0; i < length; i++)
                ret[i] = (decimal)((rand.NextDouble() - 0.5) * 12345678);

            return ret;
        }


    }
}
