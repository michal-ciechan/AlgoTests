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
    public class Programmers_Lesson2_ArrayRotation
    {
        [Test]
        [TestCase("1,2,3,4", 1, "4,1,2,3")]
        [TestCase("1,2,3,4", 2, "3,4,1,2")]
        [TestCase("1,2,3,4", 3, "2,3,4,1")]
        [TestCase("1,2,3,4", 4, "1,2,3,4")]
        [TestCase("1,-2,3,4", 5, "4,1,-2,3")]
        public void Test(string array, int rotations, string expectation)
        {
            var input = array.Split(",").Select(Int32.Parse).ToArray();

            var res = Rotate(input, rotations);

            var str = string.Join(",", res);

            str.Should().Be(expectation);
        }

        public int[] Rotate(int[] array, int count)
        {
            if (array.Length == 0)
                return array;
            
            var len = array.Length;
            
            // Normalize so we only have up to array.Length
            count = count % len;

            if (count == 0)
                return array;

            var pivotIx = len - count;
            var res = new int[len];
            var resIx = 0;
            
            for (int i = pivotIx; i < len; i++)
            {
                res[resIx++] = array[i];
            }
            
            for (int i = 0; i < pivotIx; i++)
            {
                res[resIx++] = array[i];
            }

            return res;
        }
    }
}