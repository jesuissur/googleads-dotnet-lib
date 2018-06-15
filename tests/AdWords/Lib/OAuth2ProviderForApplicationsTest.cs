using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Lib;
using NUnit.Framework;

namespace Google.Api.Ads.AdWords.Tests.Lib
{
    [TestFixture]
    internal class OAuth2ProviderForApplicationsTest
    {
        [Test]
        public void GetAuthorizationUrl_Should_NotThrow()
        {
            var subject = new OAuth2ProviderForApplications(new AdWordsAppConfig());
            Assert.DoesNotThrow(() => subject.GetAuthorizationUrl());
        }
    }
}