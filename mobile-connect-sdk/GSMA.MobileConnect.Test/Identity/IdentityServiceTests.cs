﻿using GSMA.MobileConnect.Exceptions;
using GSMA.MobileConnect.Identity;
using GSMA.MobileConnect.Utils;
using NUnit.Framework;
using System.Collections.Generic;

namespace GSMA.MobileConnect.Test.Identity
{
    [TestFixture, Parallelizable]
    public class IdentityServiceTests
    {
        private static RestResponse _unauthorizedResponse = new RestResponse(System.Net.HttpStatusCode.Unauthorized, "")
        {
            Headers = new List<BasicKeyValuePair> { new BasicKeyValuePair("WWW-Authenticate", "Bearer error=\"invalid_request\", error_description=\"No Access Token\"") }
        };

        private Dictionary<string, RestResponse> _responses = new Dictionary<string, RestResponse>()
        {
            { "user-info", new RestResponse(System.Net.HttpStatusCode.OK, "{\"sub\":\"411421B0-38D6-6568-A53A-DF99691B7EB6\",\"email\":\"test2@example.com\",\"email_verified\":true}") },
            { "unauthorized", _unauthorizedResponse },
        };

        private MockRestClient _restClient;
        private IIdentityService _identityService;

        [SetUp]
        public void Setup()
        {
            _restClient = new MockRestClient();
            _identityService = new IdentityService(_restClient);
        }

        [Test]
        public void RequestUserInfoShouldHandleUserInfoResponse()
        {
            var response = _responses["user-info"];
            _restClient.NextExpectedResponse = response;

            var result = _identityService.RequestUserInfo("user info url", "zmalqpxnskwocbdjeivbfhru").Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.ResponseCode);
            Assert.IsNotNull(result.ResponseJson);
        }

        [Test]
        public void RequestUserInfoShouldHandleUnauthorizedResponse()
        {
            var response = _responses["unauthorized"];
            _restClient.NextExpectedResponse = response;

            var result = _identityService.RequestUserInfo("user info url", "zmalqpxnskwocbdjeivbfhru").Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(401, result.ResponseCode);
            Assert.IsNull(result.ResponseJson);
            Assert.IsNotNull(result.ErrorResponse);
            Assert.IsNotEmpty(result.ErrorResponse.Error);
            Assert.IsNotEmpty(result.ErrorResponse.ErrorDescription);
        }

        [Test]
        public void RequestUserInfoShouldHandleHttpRequestException()
        {
            _restClient.NextException = new System.Net.Http.HttpRequestException("This is the message");

            Assert.ThrowsAsync<MobileConnectEndpointHttpException>(() => _identityService.RequestUserInfo("user info url", "zmalqpxnskwocbdjeivbfhru"));
        }

        [Test]
        public void RequestUserInfoShouldHandleWebRequestException()
        {
            _restClient.NextException = new System.Net.WebException("This is the message");

            Assert.ThrowsAsync<MobileConnectEndpointHttpException>(() => _identityService.RequestUserInfo("user info url", "zmalqpxnskwocbdjeivbfhru"));
        }

        #region Argument Validation

        [Test]
        public void RequestUserInfoShouldThrowWhenUserInfoUrlNull()
        {
            Assert.ThrowsAsync<MobileConnectInvalidArgumentException>(() => _identityService.RequestUserInfo(null, "zmalqpxnskwocndjeivbfhru"));
        }

        [Test]
        public void RequestUserInfoShouldThrowWhenAccessTokenNull()
        {
            Assert.ThrowsAsync<MobileConnectInvalidArgumentException>(() => _identityService.RequestUserInfo("user info url", null));
        }

        #endregion
    }
}