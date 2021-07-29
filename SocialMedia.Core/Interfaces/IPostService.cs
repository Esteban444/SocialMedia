using SocialMedia.Core.CustonEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        /*IEnumerable<Post> de GetPosts es cambiado por pagedList<Post> para impllementar la paginacion*/
        PagedList<Post> GetPosts(PostQueryFilter filters);  
        Task<Post> GetPost(int id);
        Task PostCrear(Post post);
        Task<bool> PostUpdate(Post post); 
        Task<bool> PostDelete(int id);
    }
}