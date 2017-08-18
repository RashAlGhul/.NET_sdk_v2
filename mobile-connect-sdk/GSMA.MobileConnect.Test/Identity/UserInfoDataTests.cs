﻿using GSMA.MobileConnect.Identity;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GSMA.MobileConnect.Test.Identity
{
    [TestFixture, Parallelizable]
    public class UserInfoDataTests
    {
        [Test]
        public void UserInfoDataShouldSerializeAndDeserialize()
        {
            string responseJson = "{\"sub\":\"aaaaaaa-bbbb-aaaaa-bbbbbbb\",\"name\":\"David Andrew Smith\",\"family_name\":\"Smith\",\"given_name\":\"David\",\"middle_name\":\"Andrew\",\"nickname\":\"Dave\",\"preferred_username\":\"testname\",\"profile\":\"http://profile.com/profile\",\"picture\":\"http://picture.com/picture\",\"website\":\"http://website.com/\",\"gender\":\"Male\",\"birthdate\":\"1990-11-04\",\"zoneinfo\":\"Europe/London\",\"locale\":\"en-GB\",\"updated_at\":1472136214,\"email\":\"test@test.com\",\"email_verified\":true,\"address\":{\"formatted\":\"123 Fake Street Formatted\",\"street_address\":\"123 Fake Street\",\"locality\":\"Manchester\",\"region\":\"Greater Manchester\",\"postal_code\":\"M1 1AB\",\"country\":\"England\"},\"phone_number\":\"+447700900250\",\"phone_number_verified\":true}";
            var userInfoData = JsonConvert.DeserializeObject<UserInfoData>(responseJson);

            var actual = JsonConvert.SerializeObject(userInfoData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });

            Assert.AreEqual(responseJson, actual);
        }
    }
}