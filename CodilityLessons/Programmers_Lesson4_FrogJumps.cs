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
    public class Programmers_Lesson4_FrogJumps
    {
        [Test]
        [TestCase(10,85,30, 3)]
        [TestCase(1,5,2, 2)]
        public void Test(int start, int target, int distance, int expectedSteps)
        {
            var res = FindNumberOfSteps(start, target, distance);

            res.Should().Be(expectedSteps);
        }

        private int FindNumberOfSteps(in int start, in int target, in int distance)
        {
            var distanceToJump = target - start;

            var min = distanceToJump / distance;

            if (distanceToJump % distance != 0)
                min++;
            
            return min;
        }

        public int FindUnmatchedNumber(int[] array)
        {
            if (array.Length == 1)
                return array[0];
                
            Array.Sort(array);

            for (int i = 0; i < array.Length - 1; i+=2)
            {
                if (array[i] != array[i + 1])
                    return array[i];
            }

            return array[array.Length - 1];
        }
    }
}