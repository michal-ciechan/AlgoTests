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
    public class Problem6_SumOfSquaresVsSquareOfSum : BaseProblemTest
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
            var questionInput = Enumerable.Range(1,100).ToArray();
            
            RunIteration(nameof(SimpleImplementationSquares), SimpleImplementationSquares, exampleInput, 385);
            RunIteration(nameof(SimpleImplementationSquares), SimpleImplementationSquares, questionInput, 338350);
            RunIteration(nameof(SimpleForSumOfSquares), SimpleForSumOfSquares, exampleInput, 385);
            RunIteration(nameof(SimpleForSumOfSquares), SimpleForSumOfSquares, questionInput, 338350);
            RunIteration(nameof(SimpleForSumSquared), SimpleForSumSquared, exampleInput, 3025);
            RunIteration(nameof(SimpleForSumSquared), SimpleForSumSquared, questionInput, 25502500);
            
            RunIteration(nameof(SimpleImplementation), SimpleImplementation, exampleInput, 2640);
            RunIteration(nameof(SimpleImplementation), SimpleImplementation, questionInput, 25164150);
            
            RunIteration(nameof(ProjectEulerImplementation), ProjectEulerImplementation, exampleInput, 2640);
            RunIteration(nameof(ProjectEulerImplementation), ProjectEulerImplementation, questionInput, 25164150);
        }

        /// <summary>
        /// Iterate over the largest multiple checking each number
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public int SimpleImplementationSquares(int[] inputs)
        {
            if (inputs.Length == 0) 
                return 0;

            return inputs.Sum(x => (int)Math.Pow(x, 2));
        }

        /// <summary>
        /// Iterate over the largest multiple checking each number
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public int SimpleForSumOfSquares(int[] inputs)
        {
            var sum = 0;

            for (var i = 0; i < inputs.Length; i++)
            {
                var n = inputs[i];
                
                sum += n * n;
            }

            return sum;
        }

        /// <summary>
        /// Iterate over the largest multiple checking each number
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public int SimpleForSumSquared(int[] inputs)
        {
            var sum = 0;

            for (var i = 0; i < inputs.Length; i++)
            {
                var n = inputs[i];
                
                sum += n;
            }

            return sum * sum;
        }

        /// <summary>
        /// Iterate over the largest multiple checking each number
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public int SimpleImplementation(int[] inputs)
        {
            var sumOfSquares = SimpleForSumOfSquares(inputs);
            var sumOfSquared = SimpleForSumSquared(inputs);

            return sumOfSquared - sumOfSquares;
        }

        /// <summary>
        /// Iterate over the largest multiple checking each number
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public int ProjectEulerImplementation(int[] inputs)
        {
            var sumOfNatural = inputs[^1].SumOfNaturalNumbersUpto();
            var sumOfSquares = inputs[^1].SumOfNaturalSquaredNumbersUpto();

            return (sumOfNatural * sumOfNatural) - sumOfSquares;
        }
    }
}