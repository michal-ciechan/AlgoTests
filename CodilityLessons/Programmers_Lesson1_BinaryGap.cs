using System;
using System.Buffers;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace CodilityLessons
{
    /// <summary>
    /// Find longest sequence of zeros in binary representation of an integer.
    /// </summary>
    public class Programmers_Lesson1_BinaryGap
    {
        [Test]
        [TestCase(9, 2)]
        [TestCase(529, 4)]
        [TestCase(20, 1)]
        [TestCase(15, 0)]
        [TestCase(32, 0)]
        [TestCase(1041, 5)]
        [TestCase(328, 2)]
        [TestCase(6, 0)]
        public void Test(int number, int gap)
        {
            Console.WriteLine(number.ToBinaryString());
            FindLargestBinaryGap(number).Should().Be(gap);
        }

        private int FindLargestBinaryGap(int n)
        {
            if (n == 0)
                return 0;
            
            // Trim trailing zeroes
            while ((1 & n) == 0)
                n >>= 1;

            var largestGap = 0;
            
            while (true)
            {
                // Trim the trailing 1
                n >>= 1;

                // Check if any other 1's, if not return
                if (n == 0)
                    return largestGap;

                int count = 0;
                while ((1 & n) == 0)
                {
                    count++;
                    n >>= 1;
                }

                if(count > largestGap)
                    largestGap = count;
            }
        }
    }
}