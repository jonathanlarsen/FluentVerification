using System;

namespace FluentVerification
{
    public interface IAssertion<out T>
    {
        void Handle(Action<T> action);
    }
}