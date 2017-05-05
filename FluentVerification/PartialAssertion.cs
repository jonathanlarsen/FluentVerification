using System;
using System.Collections.Generic;

namespace FluentVerification
{
    public class PartialAssertion<T> : IPartialAssertion<T>
    {
        private readonly Queue<Action<T>> _handlerQueue = new Queue<Action<T>>();

        public void Handle(Action<T> action)
        {
            _handlerQueue.Enqueue(action);
        }

        public void Complete(T val)
        {
            foreach (var handler in _handlerQueue)
            {
                handler(val);
            }
        }
    }
}