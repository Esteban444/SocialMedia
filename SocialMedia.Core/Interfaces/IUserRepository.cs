using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> GetUser(int id);
        Task<IEnumerable<Users>> GetUsers();
    }
}