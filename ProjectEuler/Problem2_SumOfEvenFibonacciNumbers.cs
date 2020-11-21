using System.Linq;
using FluentAssertions;
using Helpers;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ProjectEuler
{
    public class Problem2_SumOfEvenFibonacciNumbers : BaseProblemTest
    {
        private long _maxNumber;

        public override void Setup()
        {
            // _maxNumber = 15;
            _maxNumber = 4_000_000L;
        }

        [Test]
        public void Run()
        {
            void Assert(long res) => res.Should().Be(4613732);

            RunIteration(nameof(SimpleForImplementation), Assert, SimpleForImplementation);
            RunIteration(nameof(EvenOnlyImplementation), Assert, EvenOnlyImplementation);
        }

        public long SimpleForImplementation(int iteration)
        {
            var left = 1L;
            var right = 1L;
            
            var next = left + right;
            var sum = 0L;

            while (next < _maxNumber)
            {
                if (next % 2 == 0)
                    sum += next;

                left = right;
                right = next;
                next = left + right;
            }

            return sum;
        }

        public long EvenOnlyImplementation(int iteration)
        {
            var first = 1L;
            var second = 1L;
            var third = 2L;
            
            var sum = 0L;

            while (third < _maxNumber)
            {
                if (third % 2 == 0)
                    sum += third;

                first = second + third;
                second = third + first;
                third = first + second;
            }

            return sum;
        }
    }
}