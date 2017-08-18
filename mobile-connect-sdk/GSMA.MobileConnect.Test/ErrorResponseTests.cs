﻿using Newtonsoft.Json;
using NUnit.Framework;

namespace GSMA.MobileConnect.Test
{
    [TestFixture, Parallelizable]
    public class ErrorResponseTests
    {
        [Test]
        public void ShouldSerializeDeserializeJson()
        {
            var json = "{\"error\":\"Not_Found_Entity\",\"error_description\":\"Operator Not Found\",\"error_uri\":\"http://error.com/\"}";

            var deserialized = JsonConvert.DeserializeObject<ErrorResponse>(json);
            var serialized = JsonConvert.SerializeObject(deserialized);

            Assert.AreEqual("Not_Found_Entity", deserialized.Error);
            Assert.AreEqual("Operator Not Found", deserialized.ErrorDescription);
            Assert.AreEqual("http://error.com/", deserialized.ErrorUri);
            Assert.AreEqual(json, serialized);
        }

        [Test]
        public void ShouldConstructWithProperties()
        {
            var error = "This Error";
            var description = "This Description";
            var uri = "http://error.com/";

            var actual = new ErrorResponse { Error = error, ErrorDescription = description, ErrorUri = uri };

            Assert.AreEqual(error, actual.Error);
            Assert.AreEqual(description, actual.ErrorDescription);
            Assert.AreEqual(uri, actual.ErrorUri);
        }

        [Test]
        public void CreateErrorResponseFromEmptyUrl()
        {
            Assert.IsNull(ErrorResponse.CreateFromUrl(""));
        }

        [Test]
        public void CreateErrorResponseFromString()
        {
            Assert.IsNull(ErrorResponse.CreateFromUrl("someString"));
        }
    }
}