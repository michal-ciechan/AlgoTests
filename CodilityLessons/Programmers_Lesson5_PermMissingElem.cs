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
    public class Programmers_Lesson5_PermMissingElem
    {
        [Test]
        [TestCase("3,2,4,5",1)]
        [TestCase("2,3,1,5",4)]
        public void Test(string array, int expectedMissing)
        {
            var input = array.Split(",").Select(Int32.Parse).ToArray();
            
            var res = FindMissingElem(input);

            res.Should().Be(expectedMissing);
        }

        private int FindMissingElem(int[] A)
        {
            var limit = A.Length + 1;
            var expectedSum = (limit * ((long)limit + 1)) / 2;
            var actualSum = 0L;
        
            for(int i = 0; i < A.Length; i++)
            {
                actualSum += A[i];
            }
        
            var missingNumber = expectedSum - actualSum;
        
            return (int)missingNumber;
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