using FileRead.Application.ViewModels;

namespace FileRead.Application.Interfaces
{
    public interface IRelatorioService
    {
        /// <summary>
        /// Retorna o relatório de acordo com o mês e ano desejado
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<RelatorioViewModel>> GetRelatorioMesAno(int mes, int ano, CancellationToken cancellationToken);
    }
}
