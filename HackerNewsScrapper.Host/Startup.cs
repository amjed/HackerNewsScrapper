using HackerNewsScrapper.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNewsScrapper.Host
{
    public class Startup
    {
        public void Configure()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IHackerNewsPostsService, HackerNewsPostsService>();

        }
    }
}
