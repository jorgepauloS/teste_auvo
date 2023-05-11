using FileRead.Domain.Entities;

namespace FileRead.Domain.Interfaces
{
    public interface ILeituraRepository : IBaseRepository<Leitura, Guid>
    {
        /// <summary>
        /// Retorna todas as leituras do departamento, mês e ano desejado
        /// </summary>
        /// <param name="departamentoId"></param>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public Task<IEnumerable<Leitura>> GetByDepartamentoMesAno(int departamentoId, int mes, int ano, CancellationToken cancellationToken);
    }
}
