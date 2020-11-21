using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FluentAssertions;
using NUnit.Framework;

namespace ProjectEuler
{
    public abstract class BaseProblemTest
    {
        private Stopwatch _sw;
        private Exception _lastEx;
        public int IterationCount { get; set; } = 3;
        
        [SetUp]
        public void TestSetup()
        {
            _sw = Stopwatch.StartNew();
            Setup();
        }

        protected void RunIteration<T>(string name, Action<T> assert, Func<int, T> implementation)
        {
            for (int i = 0; i < IterationCount; i++)
            {
                try
                {
                    _sw.Restart();

                    var res = implementation(i);

                    var elapsed = _sw.Elapsed;

                    Console.WriteLine($"{name} #{i}: {elapsed.Ticks:N0}ticks");

                    assert(res);
                }
                catch (Exception e)
                {
                    var elapsedTicks = _sw.Elapsed.Ticks;

                    Console.WriteLine(
                        $"Assertion failed for {name} on try {i + 1}. {e.Message} after {elapsedTicks:N0}ticks");

                    _lastEx = e;
                }
            }
        }

        protected void RunIteration<TInput, TOutput>(string name, Func<TInput, TOutput> implementation, TInput input,
            TOutput expected)
        {
            RunIteration(name, implementation, input, output => output.Should().Be(expected));
        }

        protected void RunIteration<TInput, TOutput>(string name, Func<TInput, TOutput> implementation, TInput input,
            Action<TOutput> assert)
        {
            for (int i = 0; i < IterationCount; i++)
            {
                try
                {
                    _sw.Restart();

                    var res = implementation(input);

                    var elapsed = _sw.Elapsed;

                    Console.WriteLine($"{name} #{i}: {elapsed.Ticks:N0}ticks");

                    assert(res);
                }
                catch (Exception e)
                {
                    var elapsedTicks = _sw.Elapsed.Ticks;

                    Console.WriteLine(
                        $"Assertion failed for {name} on try {i + 1}. {e.Message} after {elapsedTicks:N0}ticks");

                    _lastEx = e;
                }
            }
        }

        [TearDown]
        protected void Cleanup()
        {
            if (_lastEx != null)
            {
                throw new AssertionException("Exception Happened. See inner exception", _lastEx); 
            }
        }
        
        public abstract void Setup();
    }
}