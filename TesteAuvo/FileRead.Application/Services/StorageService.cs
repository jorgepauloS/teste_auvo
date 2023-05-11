using FileRead.Application.Interfaces;
using FileRead.Application.ViewModels;
using FileRead.Domain.Entities;
using FileRead.Domain.Interfaces;
using System.Globalization;
using System.IO;

namespace FileRead.Application.Services
{
    public class StorageService : IStorageService
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly ILeituraRepository _leituraRepository;

        public StorageService(IDepartamentoRepository departamentoRepository, ILeituraRepository leituraRepository)
        {
            _departamentoRepository = departamentoRepository;
            _leituraRepository = leituraRepository;
        }

        public void EscreverArquivoRelatorio(IEnumerable<RelatorioViewModel> relatoriosViewModel, CancellationToken cancellationToken)
        {
            string diretorioSaida = @"C:\\Arquivos\\Saída";
            if (!Directory.Exists(diretorioSaida))
                Directory.CreateDirectory(diretorioSaida);

            string arquivoSaida = Path.Combine(diretorioSaida, $"{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.json");
            File.AppendAllText(arquivoSaida, System.Text.Json.JsonSerializer.Serialize(relatoriosViewModel, options: new System.Text.Json.JsonSerializerOptions() { WriteIndented = true }));
        }

        public void ProcessarDiretorio(string path, CancellationToken cancellationToken)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("Diretório inválido");

            IEnumerable<string> files = Directory.EnumerateFiles(path);

            if (files.Any())
            {
                Task[] tasks = files.Select(file => Task.Run(async () =>
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    var nomeDepartamento = fileName.Split("-")[0];

                    var departamento = await _departamentoRepository.GetByNome(nomeDepartamento, cancellationToken);
                    if (departamento is null)
                    {
                        departamento = new Departamento() { Nome = nomeDepartamento };
                        await _departamentoRepository.Create(departamento, cancellationToken);
                    }

                    var linhas = (await File.ReadAllLinesAsync(file, cancellationToken)).ToList();
                    linhas.RemoveAt(0);

                    foreach (var linha in linhas)
                    {
                        List<string> colunas = linha.Split(";").ToList();

                        int codigo = Convert.ToInt32(colunas[0]);
                        string nome = colunas[1];
                        double valorHora = Convert.ToDouble(colunas[2].Replace("R$ ", ""));
                        DateTime data = DateTime.ParseExact(colunas[3], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime dataHoraEntrada = data.AddHours(Convert.ToInt32(colunas[4].Split(":")[0]))
                                                       .AddMinutes(Convert.ToInt32(colunas[4].Split(":")[1]))
                                                       .AddSeconds(Convert.ToInt32(colunas[4].Split(":")[2]));
                        DateTime dataHoraSaida = data.AddHours(Convert.ToInt32(colunas[5].Split(":")[0]))
                                                     .AddMinutes(Convert.ToInt32(colunas[5].Split(":")[1]))
                                                     .AddSeconds(Convert.ToInt32(colunas[5].Split(":")[2]));
                        DateTime dataHoraSaidaAlmoco = data.AddHours(Convert.ToInt32(colunas[6].Split(" - ")[0].Split(":")[0]))
                                                           .AddMinutes(Convert.ToInt32(colunas[6].Split(" - ")[0].Split(":")[1]));
                        DateTime dataHoraVoltaAlmoco = data.AddHours(Convert.ToInt32(colunas[6].Split(" - ")[1].Split(":")[0]))
                                                           .AddMinutes(Convert.ToInt32(colunas[6].Split(" - ")[1].Split(":")[1]));

                        Leitura leitura = new(departamento.Id, codigo, nome, valorHora, data, dataHoraEntrada, dataHoraSaida, dataHoraSaidaAlmoco, dataHoraVoltaAlmoco);
                        await _leituraRepository.Create(leitura, cancellationToken);
                    }
                }, cancellationToken)).ToArray();

                Task.WaitAll(tasks: tasks, cancellationToken: cancellationToken);
            }
        }
    }
}
