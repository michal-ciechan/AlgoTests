using System.Linq;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace ProjectEuler
{
    public class Problem1SumOfMultiples : BaseProblemTest
    {
        private int _maxNumber;

        public override void Setup()
        {
            // _maxNumber = 15;
            _maxNumber = 1_000_000;
        }

        [Test]
        public void Run()
        {
            void Assert(long res) => res.Should().Be(233334166668L);

            RunIteration(nameof(LinqImplementation), Assert, LinqImplementation);
            RunIteration(nameof(SimpleForImplementation), Assert, SimpleForImplementation);
            RunIteration(nameof(SumOfDivisibleNumbersImplementation), Assert, SumOfDivisibleNumbersImplementation);
        }

        public long LinqImplementation(int iteration)
        {
            return Enumerable
                .Range(1, _maxNumber)
                .Where(x => x % 3 == 0 || x % 5 == 0)
                .Select(x => (long)x)
                .Sum();
        }

        public long SimpleForImplementation(int iteration)
        {
            var sum = 0L;

            for (int i = 1; i <= _maxNumber; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                }
            }

            return sum;
        }

        public long SumOfDivisibleNumbersImplementation(int iteration)
        {
            var n = _maxNumber;

            var sumOf3 = SumOfDivisors(n, 3);
            var sumOf5 = SumOfDivisors(n, 5);
            var sumOf15 = SumOfDivisors(n, 15);
            
            var sum = sumOf3 + sumOf5 - sumOf15;

            return sum;
        }

        public long SumOfDivisors(int maxNumber, int divisor)
        {
            var numberOfMultiples = maxNumber / divisor;

            var triangularNumberSum = numberOfMultiples.TriangularNumberSum();

            return divisor * triangularNumberSum;
        }
    }
}