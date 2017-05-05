using System;

namespace FluentVerification
{
    public class Assertion<T> : IAssertion<T>
    {
        private readonly T _value;

        public Assertion(T value)
        {
            _value = value;
        }

        public void Handle(Action<T> action)
        {
            action(_value);
        }
    }
}