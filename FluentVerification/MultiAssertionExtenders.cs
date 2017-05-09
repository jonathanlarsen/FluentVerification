using System;
using NUnit.Framework;

namespace FluentVerification
{
    public static class MultiAssertionExtenders
    {
        private static MultiAssertion<T> Within<T>(this MultiAssertion<T> assertion, uint? min, uint? max, Action<IAssertion<T>> handler)
        {
            assertion.Handle(items =>
            {
                var matches = 0;

                foreach (var item in items)
                {
                    var itemAssertion = new Assertion<T>(item);

                    try
                    {
                        handler(itemAssertion);

                        matches++;
                    }
                    catch
                    {
                        // ignored
                    }

                    if (max != null && matches > max) throw new AssertionException($"Item matched too many times. Expected {max} times.");
                }

                if (min != null && matches < min) throw new AssertionException($"Item matched too few times. Expected {min} times.");
            });

            return assertion;
        }

        public static MultiAssertion<T> Exactly<T>(this MultiAssertion<T> assertion, uint times, Action<IAssertion<T>> handler)
        {
            return assertion.Within(times, times, handler);
        }

        public static MultiAssertion<T> AtLeast<T>(this MultiAssertion<T> assertion, uint times, Action<IAssertion<T>> handler)
        {
            return assertion.Within(times, null, handler);
        }

        public static MultiAssertion<T> AtMost<T>(this MultiAssertion<T> assertion, uint times, Action<IAssertion<T>> handler)
        {
            return assertion.Within(null, times, handler);
        }

        public static MultiAssertion<T> Between<T>(this MultiAssertion<T> assertion, uint min, uint max, Action<IAssertion<T>> handler)
        {
            return assertion.Within(min, max, handler);
        }

        public static MultiAssertion<T> All<T>(this MultiAssertion<T> assertion, Action<IAssertion<T>> handler)
        {
            assertion.Handle(items =>
            {
                foreach (var item in items)
                {
                    var itemAssertion = new Assertion<T>(item);

                    try
                    {
                        handler(itemAssertion);
                    }
                    catch
                    {
                        throw new AssertionException("Not all items matched condition");
                    }
                }
            });

            return assertion;
        }

        public static MultiAssertion<T> None<T>(this MultiAssertion<T> assertion, Action<IAssertion<T>> handler)
        {
            assertion.Handle(items =>
            {
                foreach (var item in items)
                {
                    var itemAssertion = new Assertion<T>(item);

                    try
                    {
                        handler(itemAssertion);
                    }
                    catch
                    {
                        continue;
                    }

                    throw new AssertionException("An item met a condition it was not supposed to.");
                }
            });

            return assertion;
        }
    }
}