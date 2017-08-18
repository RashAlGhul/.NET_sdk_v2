﻿using GSMA.MobileConnect.Utils;
using NUnit.Framework;
using System.Collections.Generic;

namespace GSMA.MobileConnect.Test.Utils
{
    [TestFixture, Parallelizable]
    public class SecurityTests
    {
        [Test]
        public void GenerateNonceShouldGenerateRandomString()
        {
            var actual = Security.GenerateSecureNonce();

            Assert.IsNotEmpty(actual);
        }

        [Test]
        public void GenerateNonceShouldGenerateDifferentEveryCall()
        {
            var numTests = 100;

            var nonces = new List<string>();

            for (int i = 0; i < numTests; i++)
            {
                var nonce = Security.GenerateSecureNonce();
                Assert.That(() => !nonces.Contains(nonce));
                nonces.Add(nonce);
            }

            Assert.AreEqual(numTests, nonces.Count);
        }
    }
}