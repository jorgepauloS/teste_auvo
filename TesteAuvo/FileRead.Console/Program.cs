using FileRead.Application.Interfaces;
using FileRead.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        DependencyContainer.RegisterServices(services);
    })
    .Build();

IStorageService? storage = host.Services.GetRequiredService<IStorageService>();
IRelatorioService? relatorio = host.Services.GetRequiredService<IRelatorioService>();
CancellationTokenSource tokenSource = new();

do
{
    try
    {
        Console.WriteLine("Digite uma operação a ser realizada:");
        Console.WriteLine("1 - Ler arquivos no diretório");
        Console.WriteLine("2 - Escrever arquivos de relatório com os dados lidos");

        string operacao = Console.ReadLine();
        if (string.IsNullOrEmpty(operacao) || (operacao != "1" && operacao != "2"))
        {
            Console.WriteLine("Operação inválida.");
            continue;
        }

        switch (operacao)
        {
            case "1":
                Console.WriteLine("Digite o diretório que deseja processar:");
                Console.WriteLine("Ex: C:\\Arquivos\\Entrada");
                string repositorio = Console.ReadLine();
                storage.ProcessarDiretorio(repositorio, tokenSource.Token);
                Console.WriteLine("Diretório processado.");
                break;
            case "2":
                Console.WriteLine("Qual período você deseja fazer a busca?");
                Console.WriteLine("Informe o mês:");
                int mes = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Informe o ano:");
                int ano = Int32.Parse(Console.ReadLine());
                var relatorios = await relatorio.GetRelatorioMesAno(mes, ano, tokenSource.Token);
                if (!relatorios.Any())
                {
                    Console.WriteLine("Período sem registros.");
                }
                else
                {
                    storage.EscreverArquivoRelatorio(relatorios, tokenSource.Token);
                }
                break;
        }
    }
    catch (Exception ex)
    {
        tokenSource.Cancel();
        Console.WriteLine($"Erro: {ex.Message}");
    }
}
while (true);