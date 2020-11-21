using System;
using System.Buffers;
using System.Linq;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace CodilityLessons
{
    /// <summary>
    /// Find longest sequence of zeros in binary representation of an integer.
    /// </summary>
    public class Programmers_Lesson7
    {
        [Test]
        [TestCase("3,1,2,4,3",1)]
        [TestCase("-1000,1000",2000)]
        public void Test(string array, int expectedMissing)
        {
            var input = array.Split(",").Select(Int32.Parse).ToArray();
            
            var res = FindMinimumAbsoluteDifferenceWhenBalanced(input);

            res.Should().Be(expectedMissing);
        }

        private int FindMinimumAbsoluteDifferenceWhenBalanced(int[] A)
        {
            int sum = 0;

            for (var i = 0; i < A.Length; i++)
                sum += A[i];

            var leftSum = A[0];
            var rightSum = sum - leftSum;

            var maxDiff = Math.Abs(leftSum - rightSum);

            for (int i = 1; i < A.Length - 1; i++)
            {
                leftSum += A[i];
                rightSum -= A[i];
                
                var diff = Math.Abs(leftSum - rightSum);

                if (diff < maxDiff)
                    maxDiff = diff;
            }

            return maxDiff;
        }
    }
}