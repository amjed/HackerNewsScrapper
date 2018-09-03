using System.Text.RegularExpressions;
using HackerNewsScrapper.Entities;

namespace HackerNewsScrapper.Domain.Validation
{
    public class HackerNewsPostItemValidator : IHackerNewsPostItemValidator
    {
        public bool IsValid(HackerNewsPost post)
        {
            return post != null &&
                IsTitleValid(post.Title) &&
                IsAuthorValid(post.Author) &&
                IsUrlValid(post.Url) &&
                IsCommentValid(post.Comments) &&
                IsRankValid(post.Rank) &&
                IsPointValid(post.Points);
        }

        private bool IsTitleValid(string title)
        {
            return string.IsNullOrEmpty(title) == false && title.Length <= 256;
        }

        private bool IsAuthorValid(string author)
        {
            return string.IsNullOrEmpty(author) == false && author.Length <= 256;
        }

        private bool IsUrlValid(string url)
        {
            var pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }

        private bool IsCommentValid(int? comment)
        {
            return comment.HasValue && comment.Value > 0;
        }

        private bool IsRankValid(int? rank)
        {
            return rank.HasValue && rank.Value > 0;
        }

        private bool IsPointValid(int? point)
        { 
            return point.HasValue && point.Value > 0;
        }
    }
}
