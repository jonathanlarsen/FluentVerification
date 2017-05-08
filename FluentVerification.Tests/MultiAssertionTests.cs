using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace FluentVerification.Tests
{
    [TestFixture, Parallelizable]
    public class MultiAssertionTests
    {
        [Test]
        public void Exactly_Errors_On_Fewer_Expected_Matches()
        {
            var list = new List<string>
            {
                "Thing"
            };

            Assert.Throws<AssertionException>(() =>
            {
                Check.That(list)
                    .With(x => x.Exactly(2,
                        once => once.NotNull()));
            });

        }

        [Test]
        public void Exactly_Errors_On_Extra_Matches()
        {
            var list = new List<string>
            {
                "Thing",
                "Other Thing"
            };

            Assert.Throws<AssertionException>(() =>
            {
                Check.That(list)
                    .With(x => x.Exactly(1,
                        once => once.NotNull()));
            });

        }

        [Test]
        public void Exactly_Success_On_Exact_Match()
        {
            var list = new List<string>
            {
                "Thing",
            };

            Assert.DoesNotThrow(() =>
            {
                Check.That(list)
                    .With(x => x.Exactly(1,
                        once => once.NotNull()));
            });

        }

        [Test]
        public void Exactly_Only_Counts_Successful_Assertions()
        {
            var list = new List<int>
            {
                1,
                2,
                3,
                1
            };

            Assert.DoesNotThrow(() =>
            {
                Check.That(list)
                    .With(x => x.Exactly(2,
                        once => once.EqualTo(1)));
            });

        }


        [Test]
        public void Exactly_Works_With_Zero()
        {
            var list = new List<int>();

            Assert.DoesNotThrow(() =>
            {
                Check.That(list)
                    .With(x => x.Exactly(0,
                        once => once.EqualTo(1)));
            });
        }

        [TestCase(1, 1, true)
        ,TestCase(0, 1, false)
        ,TestCase(2, 1, true)]
        public void AtLeast_Handles_Expected_Range(int items, int atLeast, bool shouldSucceed)
        {
            var list = new List<string>();

            for (var i = 0; i < items; i++)
            {
                list.Add(Guid.NewGuid().ToString());
            }

            if (shouldSucceed)
            {
                Assert.DoesNotThrow(() =>
                {
                    Check.That(list)
                         .With(x => x.AtLeast((uint)atLeast,
                            item => item.NotNull()));
                });
            }
            else
            {
                Assert.Throws<AssertionException>(() =>
                {
                    Check.That(list)
                         .With(x => x.AtLeast((uint)atLeast,
                            item => item.NotNull()));
                });
            }
        }
    }
}
