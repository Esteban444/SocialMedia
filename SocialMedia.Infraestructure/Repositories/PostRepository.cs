using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*namespace SocialMedia.Core.Repositories // este ya no se utiliza con la implementacion del baserepository 
{
    public class PostRepository : IPostRepository
    {
        private readonly RedContext _context;

        public PostRepository(RedContext context)
        {
            _context = context;
        }
        // metodo todas las consultas
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var post = await _context.Post.ToListAsync();

            return post;
        }
        // metodo consulta por un id
        public async Task<Post> GetPost(int id)
        {
            var post = await _context.Post.FirstOrDefaultAsync(p => p.Id == id);

            return post;
        }

        // metodo Crear
        public async Task PostCrear(Post post)
        {
            _context.Post.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PostUddate(Post post)
        {
            var currentpost = await GetPost(post.Id);
            currentpost.Dates = post.Dates;
            currentpost.Descriptions = post.Descriptions;
            currentpost.Images = post.Images;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> PostDelete(int id)
        {
            var currentpost = await GetPost(id);
            _context.Post.Remove(currentpost);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
*/