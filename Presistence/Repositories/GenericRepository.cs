using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;


namespace Presistence.Repositories
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
             => await _context.Set<TEntity>().AddAsync(entity);


        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity?>> GetAllAsync(bool isTrackable = false)
        {
           if(isTrackable)
            
              return  await _context.Set<TEntity>().ToListAsync();

             return await _context.Set<TEntity>()
                                   .AsNoTracking()
                                   .ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        
          =>  await _context.Set<TEntity>().FindAsync(id);
        

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

    }
}
