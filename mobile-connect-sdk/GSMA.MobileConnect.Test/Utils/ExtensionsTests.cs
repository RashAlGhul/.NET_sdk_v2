using NUnit.Framework;
using GSMA.MobileConnect.Utils;
using System;
using System.Collections.Generic;

namespace GSMA.MobileConnect.Test.Utils
{
    [TestFixture, Parallelizable]
    public class ExtensionsTests
    {
        [Test]
        public void RemoveFromDelimitedStringRemovesValues()
        {
            var delimitedString = "test remove test remove value var";
            var expected = "test test value var";
            var toRemove = "remove";

            var actual = delimitedString.RemoveFromDelimitedString(toRemove, StringComparison.Ordinal);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveFromDelimitedRespectsSpecifiedStringComparison()
        {
            var delimitedString = "test remove test REmove value var";
            var expected = "test test value var";
            var toRemove = "reMOve";

            var actual = delimitedString.RemoveFromDelimitedString(toRemove, StringComparison.OrdinalIgnoreCase);
            var anotherActual = delimitedString.RemoveFromDelimitedString(expected, StringComparison.OrdinalIgnoreCase);

            Assert.AreEqual(expected, actual);

            Assert.AreEqual(delimitedString, anotherActual);
        }

        [Test]
        public void RemoveFromDelimitedRespectsSpecifiedSeparator()
        {
            var delimitedString = "test:remove:test:remove:value:var";
            var expected = "test:test:value:var";
            var toRemove = "remove";

            var actual = delimitedString.RemoveFromDelimitedString(toRemove, StringComparison.Ordinal, ':');

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ContainsAllValuesFromString()
        {
            var stringEnumirable = new List<string>
            {
                "some text",
                "another text"
            };
            var delimitedString = "Some text,another Text";
            var separator = ',';

            Assert.True(stringEnumirable.ContainsAllValues(delimitedString, StringComparison.OrdinalIgnoreCase, separator));
        }

        [Test]
        public void ContainsAllValuesFromList()
        {
            var stringEnumirable = new List<string>
            {
                "some text",
                "another text"
            };
            var expectedList = new List<string>()
            {
                "Some text",
                "another Text"
            };

            Assert.True(stringEnumirable.ContainsAllValues(expectedList, StringComparison.OrdinalIgnoreCase));
        }
    }
}