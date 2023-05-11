using FileRead.Domain.Entities;

namespace FileRead.Domain.Interfaces
{
    public interface IBaseRepository<T, K> where T : BaseEntity<K>
    {
        /// <summary>
        /// Retorna todos
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
        /// <summary>
        /// Retorna por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> Get(K id, CancellationToken cancellationToken);
        /// <summary>
        /// Registra
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> Create(T entity, CancellationToken cancellationToken);
    }
}
