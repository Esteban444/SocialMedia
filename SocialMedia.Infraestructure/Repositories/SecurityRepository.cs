using SocialMedia.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using System.Threading.Tasks;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Infraestructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository1
    {
        // agregado para el manejo de autentificacion
        public SecurityRepository(RedContext context) : base(context)
        {

        }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Usuario == login.User );
        }
    }
}
