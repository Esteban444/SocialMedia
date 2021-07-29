using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id); // se lo agregue <T> para quitar error
        Task Add(T entity);
        void Update(T  entity);
        Task Delete(int id); 
    }
}
