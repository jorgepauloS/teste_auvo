using FileRead.Application.Interfaces;
using FileRead.Application.Services;
using FileRead.Data.Context;
using FileRead.Data.Repositories;
using FileRead.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FileRead.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Application
            services.AddScoped<IRelatorioService, RelatorioService>();
            services.AddScoped<IStorageService, StorageService>();

            //Repositories
            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            services.AddScoped<ILeituraRepository, LeituraRepository>();

            //Context
            services.AddDbContext<DataContext>(e => e.UseInMemoryDatabase("Auvo"));
        }
    }
}