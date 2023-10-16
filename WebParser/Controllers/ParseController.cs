using HtmlParser;
using HtmlParser.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebParser.Services;
using WebParser.Services.HttpClientServices;

namespace WebParser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParseController : Controller
    {
        readonly string url = "https://ura.news/news/";
        private readonly PostCrawler _crawler;

        public ParseController(PostCrawler crawler)
        {
            _crawler = crawler;
        }

        [HttpPost]
        public async Task<ActionResult> Crawle()
        { 
            string ids = @"1052694629,1052694637,1052689162,
                           1052647136,1052692733,1052692716,
                           1052691477,1052690326,1052689236,
                           1052646543,1052640544,1052407686,
                           1052500983,1052570109,1052690225,
                           1052676141,1052632774,1052546938,
                           1052513972,1052592741,1052542900,
                           1052517975,1052691779,1052647136,
                           1052694489,1052693102,1052354904,
                           1052489443,1052673461,1052671784";

            await _crawler.Crawle(ids.Split(',').Select(x => $"{url}{x.Trim()}/").ToArray());
            return Ok();
        }
    }
}
