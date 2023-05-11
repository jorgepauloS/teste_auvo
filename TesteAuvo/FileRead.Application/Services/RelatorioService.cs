using FileRead.Application.Interfaces;
using FileRead.Application.ViewModels;
using FileRead.Domain.Entities;
using FileRead.Domain.Interfaces;

namespace FileRead.Application.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly ILeituraRepository _leituraRepository;

        public RelatorioService(IDepartamentoRepository departamentoRepository, ILeituraRepository leituraRepository)
        {
            _departamentoRepository = departamentoRepository;
            _leituraRepository = leituraRepository;
        }

        public async Task<IEnumerable<RelatorioViewModel>> GetRelatorioMesAno(int mes, int ano, CancellationToken cancellationToken)
        {
            var viewModels = new List<RelatorioViewModel>();

            var departamentos = await _departamentoRepository.GetAll(cancellationToken);
            if (departamentos.Any())
            {
                foreach (var departamento in departamentos)
                {
                    var leituras = await _leituraRepository.GetByDepartamentoMesAno(departamento.Id, mes, ano, cancellationToken);
                    var funcionarios = leituras.Select(l => new { l.Codigo, l.Nome }).Distinct();
                    if (funcionarios.Any())
                    {
                        var funcionariosViewModel = new List<FuncionarioViewModel>();

                        foreach (var funcionario in funcionarios)
                        {
                            IEnumerable<Leitura> leiturasFuncionario = leituras.Where(e => e.Codigo == funcionario.Codigo);

                            FuncionarioViewModel funcionarioViewModel = new(codigo: funcionario.Codigo, nome: funcionario.Nome);
                            funcionarioViewModel.AdicionarLeituras(leiturasFuncionario);

                            funcionariosViewModel.Add(funcionarioViewModel);
                        }

                        viewModels.Add(new RelatorioViewModel(departamento.Nome, mes, ano, funcionariosViewModel));
                    }
                }
            }

            return viewModels;
        }
    }
}
