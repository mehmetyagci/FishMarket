﻿using FishMarket.Core.Repositories;
using FishMarket.Data;
using FishMarket.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FishMarket.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly FishMarketDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(FishMarketDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void Detach(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> condition)
        {
            return await _dbSet.FirstOrDefaultAsync(condition);
        }
    }
}