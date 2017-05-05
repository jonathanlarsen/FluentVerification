using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FluentVerification.Tests
{
    [TestFixture]
    public class CheckTests
    {
        [TestCase("String", "String", true)
        ,TestCase(1, 1, true)
        ,TestCase(1.0, 1.0, true)
        ,TestCase("Thing", "OtherThing", false)
        ,TestCase(1,2,false)
        ,TestCase(1.0, 1.1, false)]
        public void EqualTo_Matches_Correctly(object a, object b, bool shouldMatch)
        {
            if (shouldMatch)
            {
                Assert.DoesNotThrow(() =>
                {
                    Check.That(a)
                        .EqualTo(b);
                });
            }
            else
            {
                Assert.Throws<AssertionException>(() =>
                {
                    Check.That(a)
                         .EqualTo(b);
                });
            }
        }

        [TestCase("Test")
        ,TestCase(1)
        ,TestCase(2.0)
        ,TestCase(null)]
        public void NotNull_Matches_Correctly(object obj)
        {
            if (obj == null)
            {
                Assert.Throws<AssertionException>(() =>
                {
                    Check.That(obj)
                         .NotNull();
                });
            }
            else
            {
                Assert.DoesNotThrow(() =>
                {
                    Check.That(obj)
                        .NotNull();
                });
            }
        }

        [TestCase("Test")
         , TestCase(1)
         , TestCase(2.0)
         , TestCase(null)]
        public void IsNull_Matches_Correctly(object obj)
        {
            if (obj != null)
            {
                Assert.Throws<AssertionException>(() =>
                {
                    Check.That(obj)
                         .IsNull();
                });
            }
            else
            {
                Assert.DoesNotThrow(() =>
                {
                    Check.That(obj)
                         .IsNull();
                });
            }
        }

        [Test]
        public void That_Subassertions_Work()
        {
            var testObj = new
            {
                Nested = 10,
                NullField = (string) null
            };

            Assert.DoesNotThrow(() =>
            {
                Check.That(testObj)
                    .That(x => x.Nested)
                    .NotNull();

                Check.That(testObj)
                    .That(x => x.NullField)
                    .IsNull();
            });

            Assert.Throws<AssertionException>(() =>
            {
                Check.That(testObj)
                    .That(x => x.NullField)
                    .NotNull();
            });
        }

        [Test]
        public void That_Inline_Subassertions_Work()
        {
            var testObj = new
            {
                Nested = 10,
                NullField = (string)null
            };

            Assert.DoesNotThrow(() =>
            {
                Check.That(testObj)
                    .That(x => x.Nested, 
                        nested => nested.NotNull())
                    .That(x => x.NullField, 
                        nullField => nullField.IsNull());
            });

            Assert.Throws<AssertionException>(() =>
            {
                Check.That(testObj)
                    .That(x => x.NullField,
                        nullField => nullField.NotNull());
            });
        }

        [Test]
        public void IsEmpty_Detects_Empty_List()
        {
            var empty = Enumerable.Empty<int>();

            Assert.DoesNotThrow(() =>
            {
                Check.That(empty).IsEmpty();
            });
        }

        [Test]
        public void IsEmpty_Errors_On_Populated_List()
        {
            var list = new List<string>
            {
                "Thing"
            };

            Assert.Throws<AssertionException>(() =>
            {
                Check.That(list).IsEmpty();
            });
        }

        [Test]
        public void NotEmpty_Errors_On_Empty_List()
        {
            var empty = Enumerable.Empty<int>();

            
            Assert.Throws<AssertionException>(() =>
            {
                Check.That(empty).NotEmpty();
            });
        }

        [Test]
        public void NotEmpty_Detects_Populated_List()
        {
            var list = new List<string>
            {
                "Thing"
            };

            Assert.DoesNotThrow(() =>
            {
                Check.That(list).NotEmpty();
            });
        }
    }
}
