using System;
using System.Collections.Generic;

namespace FluentVerification
{
    public class MultiAssertion<T>
    {
        private readonly IAssertion<IEnumerable<T>> _parent;

        public MultiAssertion(IAssertion<IEnumerable<T>> parent)
        {
            _parent = parent;
        }

        public void Handle(Action<IEnumerable<T>> handlerAction)
        {
            _parent.Handle(handlerAction);
        }
    }
}
