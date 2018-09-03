using AutoMapper;
using HackerNewsScrapper.Domain.Services;
using Microsoft.Extensions.Configuration;
using StructureMap;
using System;

namespace HackerNewsScrapper.Host
{
    class Program
    {
        private static Container _container;
        private static IConfiguration _configuration;

        static void Main(string[] args)
        {
            try
            {
                Setup(args);

                var numberOfPosts = GetNumberOfPosts();
                var postsService = _container.GetInstance<IHackerNewsPostsService>();
                var component = new HackerNewsComponent(postsService, Mapper.Instance);
                var json = component.GetPosts(numberOfPosts).GetAwaiter().GetResult();
                Console.WriteLine(json);
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static int GetNumberOfPosts()
        {
            if (int.TryParse(_configuration["posts"], out var numOfPosts) && numOfPosts > 0 && numOfPosts <= 100)
            {
                return numOfPosts;
            }
            throw new ArgumentException("hackernews --posts n" + Environment.NewLine + "--posts how many posts to print. A positive integer <= 100.");
        }

        static void Setup(string[] args)
        {
            //setup configuration
            _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddCommandLine(args)
            .Build();

            //setup container
            _container = new Container();
            _container.Configure(config =>
            {
                config.For<IConfiguration>().Use(_configuration);

                config.Scan(_ =>
                {
                    _.AssembliesAndExecutablesFromApplicationBaseDirectory();
                    _.WithDefaultConventions();
                });

            });

            //load automapper profiles
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(Program).Assembly));        }

    }
}
