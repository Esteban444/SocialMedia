using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositories
{
    public class PostRepository1 : BaseRepository<Post>, IPostRepository1 // este se esta utilizando con repositorio base
    {
        public PostRepository1(RedContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Post>> GetPostsByUsers(int UserId) 
        {
            return await _entities.Where(x => x.UserId == UserId).ToListAsync();
        }
    }
}
