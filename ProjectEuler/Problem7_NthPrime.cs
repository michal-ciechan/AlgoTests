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
    public class Problem7_NthPrime : BaseProblemTest
    {

        public override void Setup()
        {
        }

        [Test]
        public void Run()
        {
            RunIteration(nameof(PrimesCollectionCold), PrimesCollectionCold, 6, 13);
            RunIteration(nameof(PrimesCollectionCold), PrimesCollectionCold, 10_001, 104743);
            RunIteration(nameof(PrimesCollection), PrimesCollection, 6, 13);
            RunIteration(nameof(PrimesCollection), PrimesCollection, 10_001, 104743);
            RunIteration(nameof(ProjectEuler), ProjectEuler, 6, 13);
            RunIteration(nameof(ProjectEuler), ProjectEuler, 10_001, 104743);
        }
        
        public int PrimesCollectionCold(int n)
        {
            Primes.Instance = new Primes();
            
            return (int)Primes.Instance[n - 1];
        }
        public int PrimesCollection(int n)
        {
            return (int)Primes.Instance[n - 1];
        }
        public int ProjectEuler(int n)
        {
            if (n == 1) return 2;
            
            var count = 1;
            var candidate = 1;

            while (count < n)
            {
                candidate += 2;
                
                if (IsPrime6K(candidate))
                    count++;
            }

            return candidate;
        }

        public bool IsPrime6K(int n)
        {
            if (n == 1) return false; // 1 != Prime
            if (n < 4) return true; // 2 & 3 = Prime
            if (n % 2 == 0) return false; // Even numbers are not prime
            if (n < 9) return true; // We have excluded 2,6,8 so left with 5,7
            if (n % 3 == 0) return false;

            var limit = (int)Math.Floor(Math.Sqrt(n));

            var f = 5;
            while (f <= limit)
            {
                if (n % f == 0) return false;
                if (n % (f + 2) == 0) return false;
                f += 6;
            }

            return true;
        }
    }
}