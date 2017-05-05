using System;

namespace FluentVerification
{
    public class ChildAssertion<T, TParent> : IAssertion<T>
    {
        private readonly IAssertion<TParent> _parentAssertion;
        private readonly Func<TParent, T> _transformation;

        public ChildAssertion(IAssertion<TParent> parentAssertion, Func<TParent, T> transformation)
        {
            _parentAssertion = parentAssertion;
            _transformation = transformation;
        }

        public void Handle(Action<T> action)
        {
            _parentAssertion.Handle(x => action(_transformation(x)));
        }
    }
}