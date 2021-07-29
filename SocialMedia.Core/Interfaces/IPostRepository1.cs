using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository1 : IRepository<Post> //herenciaimplementada para usar el nuevo repositorio PostRepository1
    {
        Task<IEnumerable<Post>> GetPostsByUsers(int UserId); 
    }
}
