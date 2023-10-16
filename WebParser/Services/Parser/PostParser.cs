using HtmlParser;
using HtmlParser.Extensions;
using System.Globalization;
using WebParser.Data.Entities;
using WebParser.Extensions;

namespace WebParser.Services.Parser
{
    public class PostParser<T> : IParser<Post> where T : IEntity
    {
        public Post ParseAsync(HtmlDocument htmlDocument)
        {
            var dateString = htmlDocument.DocumentNode.FirstElementByTagname("time").GetValue();

            var post = new Post()
            {
                Title = htmlDocument.DocumentNode.FirstElementByTagname("h1").GetValue(),
                Date = dateString.Replace(" в", "").ToDate(),
                Text = htmlDocument.DocumentNode.FirstElementByTagname("p").GetValue()
            };

            return post;
        }
    }
}
