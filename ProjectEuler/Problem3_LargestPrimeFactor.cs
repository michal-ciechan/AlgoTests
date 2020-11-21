using System;
using System.Linq;
using FluentAssertions;
using Helpers;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ProjectEuler
{
    public class Problem3_LargestPrimeFactor : BaseProblemTest
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
            void Assert(ulong res) => res.Should().Be(6857);

            // RunIteration(nameof(SimpleForImplementation), Assert, SimpleForImplementation);
            RunIteration(nameof(StackOverflowImplementation), Assert, StackOverflowImplementation);
        }

        public ulong SimpleForImplementation(int iteration)
        {
            var factors = _maxNumber.GetPrimeFactors();
            var max = 0UL;
            
            for (int i = 0; i < factors.Count; i++)
            {
                var factor = factors[i];

                if (factor > max)
                    max = factor;
            }

            return max;
        }

        public ulong StackOverflowImplementation(int iteration)
        {
            return _maxNumber.MaxPrimeFactor();
        }
    }
}