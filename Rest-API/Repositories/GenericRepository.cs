﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rest_API.Models;
using Rest_API.Repositories.Interfaces;

namespace Rest_API.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public IQueryable<TEntity> GetAll() => _appDbContext.Set<TEntity>()
            .AsNoTracking();

        public async Task<TEntity> GetByIdAsync(int id) => await _appDbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        public async Task CreateAsync(TEntity entity)
        {
            await _appDbContext.Set<TEntity>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            _appDbContext.Set<TEntity>().Remove(await GetByIdAsync(id));
            await _appDbContext.SaveChangesAsync();
        }
    }
}
