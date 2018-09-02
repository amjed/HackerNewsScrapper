using AutoMapper;
using HackerNewsScrapper.Domain.Services;
using HackerNewsScrapper.Host.ViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StructureMap;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNewsScrapper.Host
{
    class Program
    {
        private static Container Container;
        private static IConfiguration Configuration;

        static void Main(string[] args)
        {
            try
            {
                Setup(args);

                var numberOfPosts = GetNumberOfPosts();
                var postsService = Container.GetInstance<IHackerNewsPostsService>();
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
            var strNumOfPosts = Configuration["posts"];
            int numOfPosts = 0;

            if (int.TryParse(Configuration["posts"], out numOfPosts) && numOfPosts > 0 && numOfPosts <= 100)
            {
                return numOfPosts;
            }
            throw new ArgumentException("HackerNewsScrapper.Host --posts n" + Environment.NewLine + "--posts how many posts to print. A positive integer <= 100.");
        }

        static void Setup(string[] args)
        {
            Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddCommandLine(args)
            .Build();

            Container = new Container();
            Container.Configure(config =>
            {
                config.For<IConfiguration>().Use(Configuration);

                config.Scan(_ =>
                {
                    _.AssembliesAndExecutablesFromApplicationBaseDirectory();
                    _.WithDefaultConventions();
                });

            });

            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(Program).Assembly));        }

    }
}
