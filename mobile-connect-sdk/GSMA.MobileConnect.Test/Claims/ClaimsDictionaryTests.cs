using System.Collections.Generic;
using GSMA.MobileConnect.Claims;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GSMA.MobileConnect.Test.Claims
{
    [TestFixture, Parallelizable]
    public class ClaimsDictionaryTests
    {
        [Test]
        public void ClaimsDictionaryShouldSerialize()
        {
            var claims = new ClaimsDictionary();
            claims.Add("test");
            claims.AddRequired("test2");
            claims.AddWithValue("test3", false, "1634");
            var expected = "{\"test\":null,\"test2\":{\"essential\":true},\"test3\":{\"value\":\"1634\"}}";

            var serialized = JsonConvert.SerializeObject(claims);

            Assert.AreEqual(expected, serialized);
        }

        [Test]
        public void ClaimsDictionaryShouldDeserialize()
        {
            var serialized = "{\"test\":null,\"test2\":{\"essential\":true},\"test3\":{\"value\":\"1634\"}}";

            var actual = JsonConvert.DeserializeObject<ClaimsDictionary>(serialized);

            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual(null, actual["test"]);
            Assert.IsNotNull(actual["test2"]);
            Assert.IsNotNull(actual["test3"]);
        }

        [Test]
        public void ClaimsDictionaryShoulAddValuesArray()
        {
            var claims = new ClaimsDictionary();
            claims.Add("test");
            object[] objects = { "1234567", "7654321" };
            claims.AddWithValues("test2", true, objects);

            claims.Remove("test1");

            Assert.AreEqual(2, claims.Count);
            Assert.IsNotNull(claims["test2"]);
            Assert.AreEqual(objects, claims["test2"].Values);
        }

        [Test]
        public void ClaimsDictionaryShouldContainsKeyAndValues()
        {
            var claims = new ClaimsDictionary();
            Assert.False(claims.IsReadOnly);
            claims.Add("test");
            object[] objects = { "1234567", "7654321" };
            claims.AddWithValues("test2", true, objects);
            Assert.True(claims.TryGetValue("test2", out var claimsValue));

            Assert.AreEqual(2, claims.Count);
            Assert.True(claims.ContainsKey("test"));
            Assert.True(claims.Contains(new KeyValuePair<string, ClaimsValue>("test2", claimsValue)));
        }

        [Test]
        public void ClaimsDictionaryShouldRemoveAllValues()
        {
            var claims = new ClaimsDictionary();
            claims.Add("test");
            object[] objects = { "1234567", "7654321" };
            claims.AddWithValues("test2", true, objects);

            claims.Clear();

            Assert.AreEqual(0, claims.Count);
        }

        [Test]
        public void ClaimsDictionaryShouldGetValue()
        {
            var claims = new ClaimsDictionary();
            claims.Add("test");
            object[] objects = { "1234567", "7654321" };
            claims.AddWithValues("test2", true, objects);

            Assert.True(claims.TryGetValue("test2", out var values));
            Assert.AreEqual(objects, values.Values);
        }

        [Test]
        public void RemoveShouldRemoveClaimsValue()
        {
            var claims = new ClaimsDictionary();
            claims.Add("test");
            claims.AddWithValue("test2", true, "1234567");
            Assert.True(claims.TryGetValue("test2", out var claimsValue));
            claims.Add(new KeyValuePair<string, ClaimsValue>("test", claimsValue));

            claims.Remove("test2");

            Assert.AreEqual(1, claims.Count);
            Assert.IsNull(claims["test2"]);

            claims.Remove(new KeyValuePair<string, ClaimsValue>("test", claimsValue));

            Assert.AreEqual(0, claims.Count);
            Assert.IsNull(claims["test"]);
        }
    }
}