using FileRead.Application.ViewModels;

namespace FileRead.Application.Interfaces
{
    public interface IStorageService
    {
        public void ProcessarDiretorio(string path, CancellationToken cancellationToken);
        public void EscreverArquivoRelatorio(IEnumerable<RelatorioViewModel> relatoriosViewModel, CancellationToken cancellationToken);
    }
}
