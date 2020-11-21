using System;
using System.Buffers;
using System.Collections.Generic;

namespace Helpers
{
    public static class Int64Helpers
    {
        /// <summary>
        /// Counts sum numbers 1 + 2 + 3 + 4 + ... n.
        /// <seealso cref="https://en.wikipedia.org/wiki/Triangular_number"/>
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long TriangularNumberSum(this long n)
        {
            return n * (n + 1) / 2;
        }

        public static bool IsPrimeSieve(this long n)
        {
            return Primes.Instance.IsPrime((ulong)n);
        }

        public static IReadOnlyList<long> GetFactors(this long n)
        {
            var i = (ulong)Math.Abs(n);

            var factors = i.GetFactors();

            var res = new long[factors.Count];

            for (var j = 0; j < factors.Count; j++) 
                res[j] = (long) factors[j];

            return res;
        }

        /// <summary>
        /// Get List of factors, unordered
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IReadOnlyList<ulong> GetFactors(this ulong n)
        {
            var max = (ulong)Math.Sqrt(n);

            var step = n % 2 == 0 ? 1UL : 2UL;

            var maxNumberOfFactors = (int)Math.Min(1000, max);

            var list = ArrayPool<ulong>.Shared.Rent(maxNumberOfFactors);

            var count = 0;
            
            for (ulong i = 1; i <= max; i+=step)
            {
                if (n % i == 0)
                {
                    list[count++] = i;
                    var other = n / i;
                    if(i != other)
                        list[count++] = other;
                }
            }

            var res = new ulong[count];
            
            Array.Copy(list, res, count);
            
            ArrayPool<ulong>.Shared.Return(list);

            return res;
        }

        public static bool IsPrimeSieve(this ulong n)
        {
            return Primes.Instance.IsPrime(n);
        }

        public static bool IsPrime(this ulong n)
        {
            if (n == 1) return false; // 1 != Prime
            if (n < 4) return true; // 2 & 3 = Prime
            if (n % 2 == 0) return false; // Even numbers are not prime
            if (n < 9) return true; // We have excluded 2,6,8 so left with 5,7
            if (n % 3 == 0) return false;

            var limit = (ulong)Math.Floor(Math.Sqrt(n));

            var f = 5UL;
            while (f <= limit)
            {
                if (n % f == 0) return false;
                if (n % (f + 2) == 0) return false;
                f += 6;
            }

            return true;
        }

        public static bool IsPrime(this long n)
        {
            return ((ulong) n).IsPrime();
        }
        
        public static IReadOnlyList<ulong> GetPrimeFactors(this ulong n)
        {
            var factors = n.GetFactors();

            var pool = ArrayPool<ulong>.Shared.Rent(factors.Count);
            var count = 0;

            for (int i = 0; i < factors.Count; i++)
            {
                var factor = factors[i];

                if (factor == n) 
                    continue;

                if (factor.IsPrime())
                    pool[count++] = factor;
            }

            if (count == 0)
            {
                ArrayPool<ulong>.Shared.Return(pool);
                return Array.Empty<ulong>();
            }
            
            var res = new ulong[count];
            
            Array.Copy(pool, res, count);

            ArrayPool<ulong>.Shared.Return(pool);

            return res;
        }

        public static int NumberOfDigits(this long n)
        {
            if (n >= 0)
            {
                if (n < 10L) return 1;
                if (n < 100L) return 2;
                if (n < 1000L) return 3;
                if (n < 10000L) return 4;
                if (n < 100000L) return 5;
                if (n < 1000000L) return 6;
                if (n < 10000000L) return 7;
                if (n < 100000000L) return 8;
                if (n < 1000000000L) return 9;
                if (n < 10000000000L) return 10;
                if (n < 100000000000L) return 11;
                if (n < 1000000000000L) return 12;
                if (n < 10000000000000L) return 13;
                if (n < 100000000000000L) return 14;
                if (n < 1000000000000000L) return 15;
                if (n < 10000000000000000L) return 16;
                if (n < 100000000000000000L) return 17;
                if (n < 1000000000000000000L) return 18;
                return 19;
            }
            else
            {
                if (n > -10L) return 2;
                if (n > -100L) return 3;
                if (n > -1000L) return 4;
                if (n > -10000L) return 5;
                if (n > -100000L) return 6;
                if (n > -1000000L) return 7;
                if (n > -10000000L) return 8;
                if (n > -100000000L) return 9;
                if (n > -1000000000L) return 10;
                if (n > -10000000000L) return 11;
                if (n > -100000000000L) return 12;
                if (n > -1000000000000L) return 13;
                if (n > -10000000000000L) return 14;
                if (n > -100000000000000L) return 15;
                if (n > -1000000000000000L) return 16;
                if (n > -10000000000000000L) return 17;
                if (n > -100000000000000000L) return 18;
                if (n > -1000000000000000000L) return 19;
                return 20;
            }
        }
        
        public static int NumberOfDigits(this ulong n)
        {
                if (n < 10UL) return 1;
                if (n < 100UL) return 2;
                if (n < 1000UL) return 3;
                if (n < 10000UL) return 4;
                if (n < 100000UL) return 5;
                if (n < 1000000UL) return 6;
                if (n < 10000000UL) return 7;
                if (n < 100000000UL) return 8;
                if (n < 1000000000UL) return 9;
                if (n < 10000000000UL) return 10;
                if (n < 100000000000UL) return 11;
                if (n < 1000000000000UL) return 12;
                if (n < 10000000000000UL) return 13;
                if (n < 100000000000000UL) return 14;
                if (n < 1000000000000000UL) return 15;
                if (n < 10000000000000000UL) return 16;
                if (n < 100000000000000000UL) return 17;
                if (n < 1000000000000000000UL) return 18;
                if (n < 10000000000000000000UL) return 19;
                return 20;
        }

    }
}