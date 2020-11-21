using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;

namespace Helpers
{
    public static class Int32Helpers
    {
        /// <summary>
        /// Counts sum numbers 1 + 2 + 3 + 4 + ... n.
        /// <seealso cref="https://en.wikipedia.org/wiki/Triangular_number"/>
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long TriangularNumberSum(this int n)
        {
            return n * ((long)n + 1) / 2;
        }


        /// <summary>
        /// Get List of factors, unordered
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IReadOnlyList<int> GetFactors(this int n)
        {
            var max = (int)Math.Sqrt(n);

            var step = n % 2 == 0 ? 1 : 2;

            var list = ArrayPool<int>.Shared.Rent(max);

            var count = 0;
            
            for (int i = 1; i <= max; i+=step)
            {
                if (n % i == 0)
                {
                    list[count++] = i;
                    var other = n / i;
                    if(i != other)
                        list[count++] = other;
                }
            }

            var res = new int[count];
            
            Array.Copy(list, res, count);
            
            ArrayPool<int>.Shared.Return(list);

            return res;
        }
        


        /// <summary>
        /// Get List of factors, unordered
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IReadOnlyList<int> GetPrimeFactors(this int n)
        {
            var factors = n.GetFactors();

            var pool = ArrayPool<int>.Shared.Rent(factors.Count);
            var count = 0;

            for (int i = 0; i < factors.Count; i++)
            {
                var factor = factors[i];

                if (factor == n) 
                    continue;

                if (factor.IsPrimeSieve())
                    pool[count++] = factor;
            }

            if (count == 0)
            {
                ArrayPool<int>.Shared.Return(pool);
                return Array.Empty<int>();
            }
            
            var res = new int[count];
            
            Array.Copy(pool, res, count);

            ArrayPool<int>.Shared.Return(pool);

            return res;
        }

        public static bool IsPrimeSieve(this int n)
        {
            return Primes.Instance.IsPrime((ulong)n);
        }

        public static bool IsPrime(this int n)
        {
            return ((ulong) n).IsPrime();
        }

        public static bool IsProbablyPrime_RabinMiller(this ulong n, int certainty)
        {
            if ((n < 2) || (n % 2 == 0)) return (n == 2);
 
            ulong s = n - 1;
            while (s % 2 == 0)  s >>= 1;

            var maxInt = (int)Math.Min(int.MaxValue, n);
            Random r = new Random();
            for (int i = 0; i < certainty; i++)
            {
                ulong a = (ulong)r.Next(maxInt - 1) + 1;
                ulong temp = s;
                ulong mod = 1;
                for (ulong j = 0; j < temp; ++j)  mod = (mod * a) % n;
                while (temp != n - 1 && mod != 1 && mod != n - 1)
                {
                    mod = (mod * mod) % n;
                    temp *= 2;
                }
 
                if (mod != n - 1 && temp % 2 == 0) return false;
            }
            return true;
        }

        public static int MaxPrimeFactor(this int n)
        {
            var i = (ulong) Math.Abs(n);

            var factor = (int)i.MaxPrimeFactor();

            if (n < 0) factor *= -1;

            return factor;
        }
        
        public static ulong MaxPrimeFactor (this ulong n)
        {
            unchecked
            {
                // We only care about the highest negative number. 
                while (n > 3 && 0 == (n & 1)) n >>= 1;

                uint k = 3;
                ulong k2 = 9;
                ulong delta = 16;
                while (k2 <= n)
                {
                    if (n % k == 0)
                    {
                        n /= k;
                    }
                    else
                    {
                        k += 2;
                        // This is to check if the number has wrapped around
                        if (k2 + delta < delta) return n;
                        k2 += delta;
                        delta += 8;
                    }
                }
            }
            
            return n;
        }
        
        public static int GetNumberOfDigits(this int n)
        {
            if (n >= 0)
            {
                if (n < 10) return 1;
                if (n < 100) return 2;
                if (n < 1000) return 3;
                if (n < 10000) return 4;
                if (n < 100000) return 5;
                if (n < 1000000) return 6;
                if (n < 10000000) return 7;
                if (n < 100000000) return 8;
                if (n < 1000000000) return 9;
                return 10;
            }
            else
            {
                if (n > -10) return 2;
                if (n > -100) return 3;
                if (n > -1000) return 4;
                if (n > -10000) return 5;
                if (n > -100000) return 6;
                if (n > -1000000) return 7;
                if (n > -10000000) return 8;
                if (n > -100000000) return 9;
                if (n > -1000000000) return 10;
                return 11;
            }
        }

        public static bool IsOdd(this int n)
        {
            return n % 2 == 1;
        }
        public static int[] GetDigits(this int n)
        {
            if (n == 0)
                return new[] {0};
            
            var x = Math.Abs(n);

            var numDigits = GetNumberOfDigits(x);

            var res = new int[numDigits];
            var count = 0;

            while (x > 0)
            {
                res[count++] = x % 10;
                
                x /= 10;
            }
            
            Array.Reverse(res);

            return res;
        }
        
        /// <summary>
        /// A palindromic number reads the same both ways. E.g. 9009.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPalindromic(this int n)
        {
            return n == n.Reverse();
        }
        
        /// <summary>
        /// Reverse a number, e.g. 1234 -> 4321
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Reverse(this int n)
        {
            var reversed = 0;

            while (n > 0)
            {
                reversed *= 10;
                reversed += n % 10;
                
                n /= 10;
            }

            return reversed;
        }
        
        
        public static string ToBinaryString(this int number)
        {
            if (number == 0) 
                return "0";
            
            var pool = ArrayPool<char>.Shared.Rent(32);

            var count = 0;


            while (number > 0)
            {
                pool[count++] = (1 & number) == 0 ? '0' : '1';

                number >>= 1;
            }
            
            Array.Reverse(pool);

            var res = new string(pool, pool.Length - count, count);

            ArrayPool<char>.Shared.Return(pool);

            return res;
        }

        public static int GetPreviousPalindrome(this int n)
        {
            if(n < 0)
                throw new NotImplementedException("Not supported for negative numbers");

            if (n == 0)
                return 0;

            if (n <= 10)
                return --n;

            if (n.IsPalindromic())
            {
                // 101
                var digits = n.GetDigits();
                var isOdd = digits.Length.IsOdd();
                
                var digitsEitherSide = digits.Length / 2;

                var changedDigitIndex = digitsEitherSide - 1;

                // If odd try to decrement mid digit index, otherwise up it to 9,
                // as we will decrement and borrow a value from left anyway
                if (isOdd)
                {
                    var midDigitIndex = digitsEitherSide;

                    var midDigit = digits[midDigitIndex];

                    if (midDigit > 0)
                    {
                        digits[midDigitIndex] = midDigit - 1;

                        return digits.ToNumberFromDigits();
                    }
                    
                    digits[midDigitIndex] = 9;
                }

                //
                for (; changedDigitIndex >= 0; changedDigitIndex--)
                {
                    var digit = digits[changedDigitIndex];

                    if (digit > 0)
                    {
                        var newDigit = digit - 1;

                        digits[changedDigitIndex] = newDigit;

                        // If we just rolled the highest digit to zero, set every other digit to 9
                        // and return that as it will be a palindromic number
                        if (newDigit == 0 && changedDigitIndex == 0)
                        {
                            digits[changedDigitIndex] = newDigit;

                            for (var i = 1; i < digits.Length; i++)
                                digits[i] = 9;
                            
                            return digits.ToNumberFromDigits();
                        }

                        digits[^(changedDigitIndex + 1)] = newDigit;
                        
                        break;
                    }
                    // If Digit is 0, change to 9, as we will be decrementing the next value.
                    else
                    {
                        var newDigit = 9;
                        digits[changedDigitIndex] = newDigit;
                        digits[^(changedDigitIndex + 1)] = newDigit;
                    }
                }

                return digits.ToNumberFromDigits();
            }
            else
            {
                throw new NotImplementedException("Starting from non palindromic is not yet supported");
            }
        }

        public static int ToNumberFromDigits(this IReadOnlyList<int> digits)
        {
            var n = 0;
            
            for (int i = 0; i < digits.Count; i++)
            {
                n *= 10;
                n += digits[i];
            }

            return n;
        }
       
        public static int GreatestCommonDivisor(this int a, int b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            
            return a;
        }
       
        public static int LowestCommonMultiple(this int a, int b)
        {
            var value = (long)a * b;
            var multiplied = Math.Abs(value);
            var greatestCommonDivisor = GreatestCommonDivisor(a, b);
            
            return (int)(multiplied / greatestCommonDivisor);
        }
       
        public static int LowestCommonMultiple(this int[] numbers)
        {
            if (numbers.Length == 0)
                return 0;

            if (numbers.Length == 1)
                return numbers[0];

            var currentLcm = LowestCommonMultiple(numbers[0], numbers[1]);
            
            for (int i = 2; i < numbers.Length; i++)
            {
                var number = numbers[i];
                currentLcm = LowestCommonMultiple(currentLcm, number);
            }

            return currentLcm;
        }

        /// <summary>
        /// Returns 1 + 2 + 3 + 4 + ... + n
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static int SumOfNaturalNumbersUpto(this int limit)
        {
            return limit * (limit + 1) / 2;
        }

        /// <summary>
        /// Returns 1² + 2² + 3² + 4² + ... + n².
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static int SumOfNaturalSquaredNumbersUpto(this int limit)
        {
            return (2 * limit + 1) * (limit + 1) * limit / 6;
        }
        
        public static int Parse(this char c)
        {
            const int zero = (int) '0';

            if (c < zero)
                return 0;

            var number = c - zero;

            return number < 10 ? number : 0;
        }
    }
};