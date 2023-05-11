namespace FileRead.Application.ViewModels
{
    public class RelatorioViewModel
    {
        public string Departamento { get; set; }
        public string MesVigencia { get; set; }
        public int AnoVigencia { get; set; }

        public double TotalPagar
        {
            get
            {
                return Funcionarios.Sum(f => f.TotalReceber);
            }
        }
        public double TotalDescontos
        {
            get
            {
                return Funcionarios.Sum(f => f.HorasDebito);
            }
        }
        public double TotalExtras
        {
            get
            {
                return Funcionarios.Sum(f => f.HorasExtras);
            }
        }

        public IEnumerable<FuncionarioViewModel> Funcionarios { get; set; }

        public RelatorioViewModel(string departamento, int mesVigencia, int anoVigencia, IEnumerable<FuncionarioViewModel> funcionarios)
        {
            Departamento = departamento;
            AnoVigencia = anoVigencia;
            Funcionarios = funcionarios;
            MesVigencia = GetMesVigenciaExtenso(mesVigencia);
        }

        private string GetMesVigenciaExtenso(int mes)
        {
            //Fazer o switch
            switch (mes)
            {
                case 1:
                    return "Janeiro";
                case 2:
                    return "Fevereiro";
                case 3:
                    return "Março";
                case 4:
                    return "Abril";
                case 5:
                    return "Maio";
                case 6:
                    return "Junho";
                case 7:
                    return "Julho";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setembro";
                case 10:
                    return "Outubro";
                case 11:
                    return "Novembro";
                case 12:
                    return "Dezembro";
            }

            return string.Empty;
        }
    }
}
