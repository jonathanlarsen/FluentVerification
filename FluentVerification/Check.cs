namespace FluentVerification
{
    public static class Check
    {
        public static IAssertion<T> That<T>(T value)
        {
            return new Assertion<T>(value);
        }
    }
}
