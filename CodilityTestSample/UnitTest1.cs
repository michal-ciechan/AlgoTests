using System;
using System.Linq;
using NUnit.Framework;

namespace MavenCodilityTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
    class Solution {
        public int solution(int[] A)
        {
            Array.Sort(A);

            var nextPotentialSmallestValue = 1;
                
            foreach (var v in A)
            {
                if(v < 1)
                    continue;
                
                if(v > nextPotentialSmallestValue)
                    return nextPotentialSmallestValue;

                if (v == nextPotentialSmallestValue)
                {
                    nextPotentialSmallestValue++;
                }
            }

            return nextPotentialSmallestValue;
        } 
    }
}