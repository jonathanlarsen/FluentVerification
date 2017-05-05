namespace FluentVerification
{
    public interface IPartialAssertion<T> : IAssertion<T>
    {
        void Complete(T val);
    }
}