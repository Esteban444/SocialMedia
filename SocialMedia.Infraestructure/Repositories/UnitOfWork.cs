using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RedContext _context;
        private readonly IPostRepository1 _postRepository;
        private readonly IRepository<Users> _UserRepository;
        private readonly IRepository<Comment> _CommentRepository;
        private readonly ISecurityRepository1  _SecurityRepository; 


        public UnitOfWork(RedContext redContext)
        {
            _context = redContext;
        }
        public IPostRepository1 PostRepository => _postRepository ?? new PostRepository1(_context);

        public IRepository<Users> UserRepository => _UserRepository ?? new BaseRepository<Users>(_context);

        public IRepository<Comment> CommentRepository => _CommentRepository ?? new BaseRepository<Comment>(_context);

        public ISecurityRepository1 SecurityRepository => _SecurityRepository ?? new SecurityRepository (_context);


        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChages()
        {
            _context.SaveChanges();
        }

        public async Task SaveChagesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
