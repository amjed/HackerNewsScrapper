using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HackerNewsScrapper.Common;
using HackerNewsScrapper.HttpClients;
using Polly;

namespace HackerNewsScrapper.ServiceAgents
{
    public class HackerNewsServiceAgent : IHackerNewsServiceAgent
    {
        private readonly IHackerNewsClient _hackerNewsClient;
        private readonly IAppConfigurationProvider _appConfigurationProvider;
        
        private readonly List<HttpStatusCode> httpStatusCodesWorthRetrying = new List<HttpStatusCode> {                HttpStatusCode.RequestTimeout,                HttpStatusCode.InternalServerError,                HttpStatusCode.BadGateway,                HttpStatusCode.ServiceUnavailable,                HttpStatusCode.GatewayTimeout,        };

        public HackerNewsServiceAgent(IHackerNewsClient hackerNewsClient, IAppConfigurationProvider appConfigurationProvider)
        {
            _hackerNewsClient = hackerNewsClient;
            _appConfigurationProvider = appConfigurationProvider;
        }

        public async Task<string> GetDataFromPage(int pageNumber)
        {
            var retries = _appConfigurationProvider.GetIntValue("HttpClientSettings:Retries");
            var delay = _appConfigurationProvider.GetIntValue("HttpClientSettings:Delay");

            var request = GetRequest(pageNumber);
            var policy = Policy.Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => httpStatusCodesWorthRetrying.Contains(r.StatusCode))                .WaitAndRetryAsync(retries,                                    sleepDurationProvider: (attempt) =>                                        TimeSpan.FromMilliseconds(delay * attempt));

            var policyResult = await policy.ExecuteAndCaptureAsync(async () =>
            {
                return await _hackerNewsClient.GetRawPageData(request);
            }).ConfigureAwait(false);

            if (policyResult.Outcome == OutcomeType.Failure)
                return null;

            return await policyResult.Result?.Content.ReadAsStringAsync();
        }

        private string GetRequest(int pageNumber)
        {
            var parameter = _appConfigurationProvider.GetStringValue("HttpClientSettings:PageParameter");
            return $"{parameter}={pageNumber}";
        }
    }
}
