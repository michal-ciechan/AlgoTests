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
    public class Programmers_Lesson3_OddOccurencesInArray
    {
        [Test]
        [TestCase("1,2,2,3,3,4,4", 1)]
        [TestCase("9,3,9,3,9,7,9", 7)]
        [TestCase("7", 7)]
        [TestCase("7,1,1", 7)]
        public void Test(string array, int missingNumber)
        {
            var input = array.Split(",").Select(Int32.Parse).ToArray();

            var res = FindUnmatchedNumber(input);

            res.Should().Be(missingNumber);
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