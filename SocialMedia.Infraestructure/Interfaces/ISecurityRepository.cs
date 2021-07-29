using SocialMedia.Core.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositories
{
    public interface ISecurityRepository
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}