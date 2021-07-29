using SocialMedia.Core.Entities;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository1 PostRepository { get; }
        IRepository<Users> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        ISecurityRepository1 SecurityRepository { get; }
        void SaveChages();
        Task SaveChagesAsync();

    }
}
