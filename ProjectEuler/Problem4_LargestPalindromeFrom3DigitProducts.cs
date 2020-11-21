using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Helpers;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ProjectEuler
{
    public class Problem4_LargestPalindromeFrom3DigitProducts : BaseProblemTest
    {
        private ulong _maxNumber;

        public override void Setup()
        {
            // _maxNumber = 15;
            _maxNumber = 600851475143;
        }

        [Test]
        public void Run()
        {
            void AssertPalindromic(bool res) => res.Should().Be(true);
            
            void Assert(int res) => res.Should().Be(906609);

            // RunIteration(nameof(SimpleForImplementation), Assert, SimpleForImplementation);
            RunIteration(nameof(IsPalindromicBasicString), AssertPalindromic, IsPalindromicBasicString);
            RunIteration(nameof(IsPalindromicByDigits), AssertPalindromic, IsPalindromicByDigits);
            RunIteration(nameof(IsPalindromicByReverse), AssertPalindromic, IsPalindromicByReverse);
            RunIteration(nameof(SimpleImplementation), Assert, SimpleImplementation);
            RunIteration(nameof(ProjectEulerImplementation), Assert, ProjectEulerImplementation);
        }

        public bool IsPalindromicBasicString(int iteration = 0)
        {
            var n = 9009;

            return IsPalindromicByString(n);
        }

        private static bool IsPalindromicByString(int n)
        {
            var s = n.ToString();

            var reversed = new string(s.Reverse().ToArray());

            return string.Equals(reversed, s, StringComparison.Ordinal);
        }
        

        private static bool IsPalindromicByDigits(int iteration = 0)
        {
            return IsPalindromicByDigitsImplementation(9009);
        }

        private static bool IsPalindromicByReverse(int iteration = 0)
        {
            return 9009.IsPalindromic();
        }
        public static bool IsPalindromicByDigitsImplementation(int n)
        {
            var digits = n.GetDigits();

            var digitsEitherSide = digits.Length / 2;

            for (int i = 0; i < digitsEitherSide; i++)
            {
                var left = digits[i];
                var right = digits[^(i+1)];

                if (left != right) return false;
            }

            return true;
        }

        public int SimpleImplementation(int iteration)
        {
            for (var palindrome = 999999; palindrome >= 0; palindrome = palindrome.GetPreviousPalindrome())
            {
                var factors = palindrome.GetFactors();

                for (var i = 0; i < factors.Count; i++)
                {
                    var factor = factors[i];

                    var factorNumberOfDigits = factor.GetNumberOfDigits();
                    
                    if(factorNumberOfDigits != 3)
                        continue;

                    var otherFactor = palindrome / factor;

                    var otherFactorNumberOfDigits = otherFactor.GetNumberOfDigits();
                    
                    if(otherFactorNumberOfDigits != 3)
                        continue;

                    return palindrome;
                }
            }

            return 0;
        }

        public int ProjectEulerImplementation(int iteration)
        {
            var largestPalindrome = 0;
            var a = 999;
            int b = 999;
            int db = 1;
            while (a >= 100)
            {
                if (a % 11 == 0)
                {
                    b = 999;
                    db = 1;
                }
                else
                {
                    b = 990; //The largest number less than or equal 999
                    //and divisible by 11
                    db = 11;
                }

                while (b >= a)
                {
                    var x = a * b;
                    
                    if (x <= largestPalindrome)
                        break;

                    if (x.IsPalindromic())
                        largestPalindrome = x;

                    b = b - db;
                }
                
                a = a - 1;
            }

            return largestPalindrome;
        }
    }
}