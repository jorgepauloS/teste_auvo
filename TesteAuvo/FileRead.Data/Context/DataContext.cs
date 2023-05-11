using FileRead.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileRead.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Leitura> Leituras { get; set; }
    }
}
