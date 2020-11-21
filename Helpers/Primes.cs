using System;
using System.Buffers;
using System.Collections.Generic;

namespace Helpers
{
    public class Primes {
        public static Primes Instance = new Primes();

        private const int _blockSize = 3000000;

        private List<ulong> _primes;
        private ulong _next;

        public Primes() {
            _primes = new List<ulong>() { 2, 3, 5, 7, 11, 13, 17, 19 };
            _next = 23;
        }

        private void Expand() {
            var sieve = ArrayPool<bool>.Shared.Rent(_blockSize);

            for (int i = 0; i < sieve.Length; i++)
                sieve[i] = false;
            
            foreach (var prime in _primes) {
                for (var i = ((_next + prime - 1L) / prime) * prime - _next;
                    i < _blockSize; i += prime) {
                    sieve[i] = true;
                }
            }
            for (uint i = 0; i < _blockSize; i++) {
                if (!sieve[i]) {
                    _primes.Add(_next);
                    for (ulong j = i + _next; j < _blockSize; j += _next) {
                        sieve[j] = true;
                    }
                }
                _next++;
            }
            
            ArrayPool<bool>.Shared.Return(sieve);
        }

        public ulong this[int index] {
            get {
                if (index < 0) throw new IndexOutOfRangeException();
                while (index >= _primes.Count) {
                    Expand();
                }
                return _primes[index];
            }
        }

        public bool IsPrime(ulong number) {
            var last = _primes[^1];
            
            while (last < number)
            {
                last = _primes[^1];
                
                Expand();
            }
            
            return _primes.BinarySearch(number) >= 0;
        }
    }
}