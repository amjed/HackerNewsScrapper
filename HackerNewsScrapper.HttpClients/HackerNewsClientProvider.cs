﻿using HackerNewsScrapper.Common;
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

        public HttpClient GetConfiguredClient()
            var client = GetConfiguredHttpClient();


        private HttpClient GetConfiguredHttpClient()
}