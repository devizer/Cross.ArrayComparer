using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.ArrayComparer.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class Test_WinNTComparer
    {

        [Test]
        public void Test_Bytes()
        {
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
