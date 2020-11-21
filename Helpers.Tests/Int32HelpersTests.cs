using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Helpers.Tests
{
    public class Int32HelpersTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(4, "2")]
        [TestCase(5, "")]
        [TestCase(6, "2|3")]
        [TestCase(8, "2|4")]
        [TestCase(9, "3")]
        [TestCase(10, "2|5")]
        [TestCase(12, "2|3|4|6")]
        [TestCase(25, "5")]
        [TestCase(27, "3|9")]
        [TestCase(999, "3|9|27|37|111|333")]
        public void GetFactors(int input, string expected)
        {
            var factors = input.GetFactors();

            var orderedFactors = factors.OrderBy(x => x);

            var formattedFactors = orderedFactors.Select(x => x.ToString("N0"));
            
            var res = string.Join("|", formattedFactors);

            if (expected?.Length > 0)
                expected = "1|" + expected;
            else
                expected = "1";
                
            if (input > 1)
            {
                if (expected.Length > 0)
                    expected += "|";

                expected += input;
            }
            
            res.Should().Be(expected);
        }

        [TestCase(4, "2")]
        [TestCase(5, "")]
        [TestCase(6, "2|3")]
        [TestCase(8, "2")]
        [TestCase(9, "3")]
        [TestCase(10, "2|5")]
        [TestCase(12, "2|3")]
        [TestCase(25, "5")]
        [TestCase(27, "3")]
        [TestCase(999, "3|37")]
        public void GetPrimeFactors(int input, string expected)
        {
            var factors = input.GetPrimeFactors();

            var orderedFactors = factors.OrderBy(x => x);

            var formattedFactors = orderedFactors.Select(x => x.ToString("N0"));
            
            var res = string.Join("|", formattedFactors);
            
            res.Should().Be(expected);
        }

        [TestCase(5, 5)]
        [TestCase(10, 5)]
        [TestCase(12, 3)]
        [TestCase(14, 7)]
        [TestCase(25, 5)]
        public void MaxPrimeFactor(int input, int expected)
        {
            var res = input.MaxPrimeFactor();

            res.Should().Be(expected);
        }

        [TestCase(4897, "4897")]
        [TestCase(77979464, "77979464")]
        [TestCase(6, "6")]
        [TestCase(0, "0")]
        [TestCase(-1, "1")]
        [TestCase(-1231412312, "1231412312")]
        public void GetDigits(int input, string expected)
        {
            var digits = input.GetDigits();
            
            var res = string.Join("", digits);
            
            res.Should().Be(expected);
        }

        [TestCase(4897)]
        [TestCase(77979464)]
        [TestCase(6)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-1231412312)]
        public void ToNumberFromDigits(int input)
        {
            var res = input.GetDigits().ToNumberFromDigits();
            
            res.Should().Be(Math.Abs(input));
        }

        [TestCase(5, true)]
        [TestCase(10, false)]
        [TestCase(101, true)]
        [TestCase(9009, true)]
        [TestCase(90009, true)]
        [TestCase(110, false)]
        [TestCase(91009, false)]
        [TestCase(9008, false)]
        public void IsPalindromic(int input, bool expected)
        {
            var res = input.IsPalindromic();

            res.Should().Be(expected);
        }

        [TestCase(5, 4)]
        [TestCase(10, 9)]
        [TestCase(101, 99)]
        [TestCase(99, 88)]
        [TestCase(909, 898)]
        [TestCase(919, 909)]
        [TestCase(11, 9)]
        [TestCase(9009, 8998)]
        [TestCase(90009, 89998)]
        [TestCase(110, 101)]
        [TestCase(91009, 90909)]
        [TestCase(9008, 8998)]
        public void GetPreviousPalindrome(int input, int expected)
        {
            var res = input.GetPreviousPalindrome();

            res.Should().Be(expected);
        }

        [TestCase(5, 10, 10)]
        [TestCase(5, 15, 15)]
        [TestCase(5, 9, 45)]
        [TestCase(232792560, 20, 232792560)]
        public void LowestCommonMultiple(int a, int b, int expected)
        {
            a.LowestCommonMultiple(b).Should().Be(expected);
        }

        [TestCase('0', 0)]
        [TestCase('1', 1)]
        [TestCase('2', 2)]
        [TestCase('3', 3)]
        [TestCase('4', 4)]
        [TestCase('5', 5)]
        [TestCase('6', 6)]
        [TestCase('7', 7)]
        [TestCase('8', 8)]
        [TestCase('9', 9)]
        [TestCase('a', 0)]
        [TestCase('Z', 0)]
        [TestCase('-', 0)]
        public void Parse(char input, int expected)
        {
            input.Parse().Should().Be(expected);
        }
        [TestCase(0, "0")]
        [TestCase(9, "1001")]
        [TestCase(529, "1000010001")]
        [TestCase(20, "10100")]
        [TestCase(15, "1111")]
        [TestCase(32, "100000")]
        public void ToBinaryString(int input, string expected)
        {
            input.ToBinaryString().Should().Be(expected);
        }
    }
}