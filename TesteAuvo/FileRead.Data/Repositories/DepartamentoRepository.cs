using FileRead.Data.Context;
using FileRead.Domain.Entities;
using FileRead.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileRead.Data.Repositories
{
    public class DepartamentoRepository : BaseRepository<Departamento, int>, IDepartamentoRepository
    {
        public DepartamentoRepository(DataContext context) : base(context) { }

        public async Task<Departamento> GetByNome(string nome, CancellationToken cancellationToken)
        {
            return await _context.Departamentos.Where(d => d.Nome.Equals(nome)).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
