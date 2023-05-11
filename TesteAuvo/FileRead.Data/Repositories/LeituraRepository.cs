using FileRead.Data.Context;
using FileRead.Domain.Entities;
using FileRead.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FileRead.Data.Repositories
{
    public class LeituraRepository : BaseRepository<Leitura, Guid>, ILeituraRepository
    {
        public LeituraRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Leitura>> GetByDepartamentoMesAno(int departamentoId, int mes, int ano, CancellationToken cancellationToken)
        {
            return await _context.Leituras.Where(l => l.DepartamentoId.Equals(departamentoId)
                                              //Data maior ou igual do dia 1 do mês
                                              && l.Data >= new DateTime(ano, mes, 1)
                                              //Data menor que o dia 1 do mês posterior
                                              && l.Data < new DateTime(ano, mes + 1, 1))
                .ToListAsync(cancellationToken);
        }
    }
}
