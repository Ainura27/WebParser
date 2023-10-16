using AutoMapper;
using WebParser.Data.Entities;
using WebParser.DTOs;

namespace WebParser.Profiles
{
    public class PostProfile: Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>(); 
        }
    }
}
