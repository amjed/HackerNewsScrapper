using HackerNewsScrapper.Domain.Validation;
using HackerNewsScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HackerNewsScrapper.Tests.Unit.Validation
{
    public class HackerNewsPostItemValidatorTests
    {
        private readonly IHackerNewsPostItemValidator _hackerNewsPostItemValidator;
        public HackerNewsPostItemValidatorTests()
        {
            _hackerNewsPostItemValidator = new HackerNewsPostItemValidator();
        }

        [Fact]
        public void ShouldReturnTrueIfPostIsValid()
        {
            var post = new HackerNewsPost
            {
                Author = "some one",
                Comments = 1,
                Points = 1,
                Rank = 1,
                Title = "some title",
                Url = "https://somewhere.com"
            };

            Assert.True(_hackerNewsPostItemValidator.IsValid(post));
        }

        [Fact]
        public void ShouldReturnFalseIfPostIsNotValid()
        {
            var post = new HackerNewsPost
            {
                Author = "some one",
                Comments = 1,
                Title = "some title",
                Url = "https://somewhere.tld"
            };

            Assert.False(_hackerNewsPostItemValidator.IsValid(post));
        }
    }
}
