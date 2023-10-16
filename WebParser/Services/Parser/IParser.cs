using HtmlParser;
using WebParser.Data.Entities;

namespace WebParser.Services.Parser
{
    public interface IParser<T> where T: IEntity
    {
        T ParseAsync(HtmlDocument htmlDocument);
    }
}
