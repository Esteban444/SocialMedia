using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository 
    { 

        Task<IEnumerable<Post>> GetPosts(); // comentado para usar Posrepository1
        Task<Post> GetPost(int id);
        Task PostCrear(Post post);
        Task<bool> PostUddate(Post post);
        Task<bool> PostDelete(int id);
    }
}
