using HackerNewsScrapper.Domain.Parsers;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace HackerNewsScrapper.Tests.Unit.Parser
{
    public class PageParserTests
    {
        private readonly IPageParser _pageParser;
        private readonly IPostSectionParser _postSectionParser;

        public PageParserTests()
        {
            _postSectionParser = new PostSectionParser(null);
            _pageParser = new PageParser(_postSectionParser);
        }

        [Fact]
        public void ShouldReturnTrueForValidPage()
        {
            var html = ReadPageTestData(1);
            Assert.True(_pageParser.IsPageValid(html));
        }

        [Fact]
        public void ShouldReturnFalseForEmptyPage()
        {
            var html = "";
            Assert.False(_pageParser.IsPageValid(html));
        }

        [Fact]
        public void ShouldReturnFalseForInvalidPage()
        {
            var html = "<html>not valid</html>";
            Assert.False(_pageParser.IsPageValid(html));
        }

        [Fact]
        public void ShouldReturnPostsFromTestPage()
        {
            var html = ReadPageTestData(1);
            var result = _pageParser.GetHackerNewsPosts(html);
            Assert.Equal(30, result.Count);
        }


        private string ReadPageTestData(int page)
        {
            var filename = $"page{page}.html";
            var dir = "TestData";
            var path = $"{dir}\\{filename}";
            if (File.Exists(path) == false)
                throw new FileNotFoundException($"page test data file {filename}not found");

            using (StreamReader sr = new StreamReader(path))
                return sr.ReadToEnd();
        }
    }
}
