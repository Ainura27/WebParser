using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Globalization;
using WebParser.DTOs;
using WebParser.Repository;

namespace WebParser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostsController(IPostRepository postRepository,
                               IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByDate([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var posts = await _postRepository.GetByDate(from, to);
            return Ok(_mapper.Map<IEnumerable<PostDto>>(posts));
        }

        [HttpGet("topten")]
        public async Task<ActionResult<IEnumerable<string>>> GetTopTen()
        {
            var topten = await _postRepository.GetTopTen();
            return Ok(topten);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PostDto>>> Search([FromQuery] string text)
        {
            var posts = await _postRepository.Search(text);
            return Ok(_mapper.Map<IEnumerable<PostDto>>(posts));
        }
    }
}
