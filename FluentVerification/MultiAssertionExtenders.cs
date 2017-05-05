using System;
using NUnit.Framework;

namespace FluentVerification
{
    public static class MultiAssertionExtenders
    {
        public static MultiAssertion<T> Exactly<T>(this MultiAssertion<T> assertion, uint times, Action<IAssertion<T>> handler)
        {
            var partialAssertion = new PartialAssertion<T>();

            handler(partialAssertion);

            assertion.Handle(items =>
            {
                var matches = 0;

                foreach (var item in items)
                {
                    try
                    {
                        partialAssertion.Complete(item);

                        matches++;
                    }
                    catch
                    {
                        // ignored
                    }

                    if(matches > times) throw new AssertionException($"Item matched too many times. Expected {times} times.");
                }

                if(matches < times) throw new AssertionException($"Item matched too few times. Expected {times} times.");
            });

            return assertion;
        }
    }
}