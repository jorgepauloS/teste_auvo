using FileRead.Domain.Entities;

namespace FileRead.Domain.Interfaces
{
    public interface IDepartamentoRepository : IBaseRepository<Departamento, int>
    {
        /// <summary>
        /// Retorna o departamento pelo nome exato
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public Task<Departamento> GetByNome(string nome, CancellationToken cancellationToken);
    }
}
