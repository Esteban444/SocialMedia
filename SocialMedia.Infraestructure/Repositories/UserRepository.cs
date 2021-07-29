using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositories // este ya no se utiliza con la implementacion del BaseRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly RedContext _context;

        public UserRepository(RedContext context)
        {
            _context = context;

        }
        // metodo todas las consultas
        public async Task<IEnumerable<Users>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }
        // metodo consulta por un id
        public async Task<Users> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

            return user;
        }
    }
}
