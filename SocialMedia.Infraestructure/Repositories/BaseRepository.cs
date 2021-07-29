﻿using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        
        private readonly RedContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(RedContext context)
        {
           _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
           return  _entities.AsEnumerable();
        }
        public async Task<T> GetById(int id) //tiene <T>
        {
            return await _entities.FindAsync(id);

        }
        public async Task Add(T entity) 
        {
            await _entities.AddAsync(entity);
           
        }

        public void  Update(T entity)
        {
            _entities.Update(entity);
            
        }

        public async Task Delete(int id) 
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
            
        }
    }
}
