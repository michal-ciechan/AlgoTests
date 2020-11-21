using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FluentAssertions;
using Helpers;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ProjectEuler
{
    public class Problem5_SmallestMultipleOfASetOfNumbers : BaseProblemTest
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
            var exampleInput = Enumerable.Range(1,10).ToArray();
            var questionInput = Enumerable.Range(1,20).ToArray();
            
            RunIteration(nameof(SimpleImplementation), SimpleImplementation, exampleInput, 2520);
            RunIteration(nameof(SimpleImplementation), SimpleImplementation, questionInput, 232792560);
            RunIteration(nameof(StackOverflowImplementation), StackOverflowImplementation, exampleInput, 2520);
            RunIteration(nameof(StackOverflowImplementation), StackOverflowImplementation, questionInput, 232792560);
        }

        public int StackOverflowImplementation(int[] inputs)
        {
            return inputs.LowestCommonMultiple();
        }

        /// <summary>
        /// Iterate over the largest multiple checking each number
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public int SimpleImplementation(int[] inputs)
        {
            if (inputs.Length == 0) 
                return 0;

            if (inputs.Length == 1)
                return inputs[0];

            Array.Sort(inputs);
            
            var multiplier = inputs[^1] * inputs[^2];
            var lowestCommonMultiple = multiplier;

            while (true)
            {
                var found = true;
                for (var i = 1; i < inputs.Length - 2; i++)
                {
                    var x = inputs[i];

                    if (x == 0)
                        continue;
                    
                    if(lowestCommonMultiple % x == 0)
                        continue;

                    lowestCommonMultiple += multiplier;

                    found = false;
                }

                if (found)
                    break;
            }

            return lowestCommonMultiple;
        }
    }
}