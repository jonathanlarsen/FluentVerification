using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;

namespace FluentVerification
{
    public static class AssertionExtenders
    {
        public static IAssertion<T> EqualTo<T>(this IAssertion<T> assertion, T value)
        {
            assertion.Handle(x => Assert.AreEqual(value, x));

            return assertion;
        }

        public static IAssertion<T> NotNull<T>(this IAssertion<T> assertion)
        {
            assertion.Handle(x => Assert.NotNull(x));

            return assertion;
        }

        public static IAssertion<T> IsNull<T>(this IAssertion<T> assertion)
        {
            assertion.Handle(x => Assert.IsNull(x));

            return assertion;
        }

        public static IAssertion<IEnumerable<T>> IsEmpty<T>(this IAssertion<IEnumerable<T>> assertion)
        {
            assertion.Handle(Assert.IsEmpty);

            return assertion;
        }

        public static IAssertion<IEnumerable<T>> NotEmpty<T>(this IAssertion<IEnumerable<T>> assertion)
        {
            assertion.Handle(Assert.IsNotEmpty);

            return assertion;
        }

        public static IAssertion<T2> That<T, T2>(this IAssertion<T> assertion, Expression<Func<T, T2>> selector)
        {
            var member = selector.Body as MemberExpression;

            if(member == null) throw new InvalidOperationException();

            var propertyInfo = member.Member as PropertyInfo;

            if(propertyInfo == null) throw new InvalidOperationException();

            var subAssertion = new ChildAssertion<T2, T>(assertion, x => (T2) propertyInfo.GetMethod.Invoke(x, null));

            return subAssertion;
        }

        public static IAssertion<T> That<T, T2>(this IAssertion<T> assertion, Expression<Func<T, T2>> selector, Action<IAssertion<T2>> handler)
        {
            var subAssertion = assertion.That(selector);

            handler(subAssertion);

            return assertion;
        }

        public static MultiAssertion<T> With<T>(this IAssertion<IEnumerable<T>> assertion)
        {
            return new MultiAssertion<T>(assertion);
        }

        public static IAssertion<IEnumerable<T>> With<T>(this IAssertion<IEnumerable<T>> assertion, Action<MultiAssertion<T>> handler)
        {
            var multiAssertion = assertion.With();

            handler(multiAssertion);

            return assertion;
        }
    }
}