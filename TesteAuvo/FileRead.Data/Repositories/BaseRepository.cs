using FileRead.Data.Context;
using FileRead.Domain.Entities;
using FileRead.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FileRead.Data.Repositories
{
    public class BaseRepository<T, K> : IBaseRepository<T, K> where T : BaseEntity<K>
    {
        internal readonly DataContext _context;
        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(T entity, CancellationToken cancellationToken)
        {
            await _context.AddAsync(entity, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<T> Get(K id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }
    }
}
