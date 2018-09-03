using HackerNewsScrapper.Common;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HackerNewsScrapper.HttpClients
{
    public class HackerNewsClientProvider:IHackerNewsClientProvider
    {
        private HttpClient _httpClient;
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public HackerNewsClientProvider(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public HttpClient GetConfiguredClient()        {            if (_httpClient != null)                return _httpClient;
            var client = GetConfiguredHttpClient();            return _httpClient ?? (_httpClient = client);        }


        private HttpClient GetConfiguredHttpClient()        {            var client = new HttpClient            {                BaseAddress = new Uri(_appConfigurationProvider.GetStringValue("HttpClientSettings:BaseUrl"))            };            return client;        }    }
}
